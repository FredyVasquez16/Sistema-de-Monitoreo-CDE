// src/components/pages/VerReporte.js

import React, { useState, useEffect } from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Paper, Grid, CircularProgress } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Bar, Doughnut } from 'react-chartjs-2';
import { useHistory } from 'react-router-dom';
import { useStateValue } from '../../Context/store';
import { obtenerAsesorias } from '../../actions/AsesoriaAction';
import { obtenerClientesEmpresas } from '../../actions/ClienteEmpresaAction';
import { obtenerContactos } from '../../actions/ContactoAction';

const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565',
        minHeight: '100vh',
        paddingBottom: theme.spacing(4),
    },
    appBar: { backgroundColor: '#D5A408', color: '#000000' },
    logo: { height: '50px', marginRight: theme.spacing(2) },
    navLinks: { flexGrow: 1 },
    navButton: { fontWeight: 'bold', margin: theme.spacing(0, 1) },
    userInfo: { textAlign: 'right' },
    content: { padding: theme.spacing(3) },
    reportCard: {
        padding: theme.spacing(2),
        backgroundColor: '#FFFFFF',
    },
    cargando: { display: 'flex', justifyContent: 'center', padding: theme.spacing(6) },
}));

// Colores existentes de los reportes (se reutilizan y se ciclan si hay más categorías)
const COLOR_BARRA = { backgroundColor: 'rgba(66, 165, 245, 0.6)', borderColor: 'rgba(66, 165, 245, 1)' };

const COLORES_DEPARTAMENTO = [
    { fondo: 'rgba(255, 159, 64, 0.7)', borde: 'rgba(255, 159, 64, 1)' },
    { fondo: 'rgba(75, 192, 192, 0.7)', borde: 'rgba(75, 192, 192, 1)' },
    { fondo: 'rgba(153, 102, 255, 0.7)', borde: 'rgba(153, 102, 255, 1)' },
    { fondo: 'rgba(255, 99, 132, 0.7)', borde: 'rgba(255, 99, 132, 1)' },
];

const COLORES_GENERO = [
    { fondo: 'rgba(54, 162, 235, 0.7)', borde: 'rgba(54, 162, 235, 1)' },
    { fondo: 'rgba(255, 99, 132, 0.7)', borde: 'rgba(255, 99, 132, 1)' },
];

// Devuelve backgroundColor/borderColor ciclando la paleta dada
const colorearCategorias = (paleta, cantidad) => ({
    backgroundColor: Array.from({ length: cantidad }, (_, i) => paleta[i % paleta.length].fondo),
    borderColor: Array.from({ length: cantidad }, (_, i) => paleta[i % paleta.length].borde),
});

const MESES = ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'];

// Agrupa las asesorías por mes de fechaSesion (etiquetas 'YYYY-MM' ordenadas → nombre del mes)
const construirAsesoriasPorMes = (asesorias) => {
    const conteos = {};
    asesorias.forEach((asesoria) => {
        if (!asesoria.fechaSesion) return;
        const mes = String(asesoria.fechaSesion).slice(0, 7); // 'YYYY-MM'
        conteos[mes] = (conteos[mes] || 0) + 1;
    });
    const mesesOrdenados = Object.keys(conteos).sort();
    return {
        labels: mesesOrdenados.map((mes) => MESES[parseInt(mes.slice(5), 10) - 1] || mes),
        datasets: [{
            label: 'Número de Asesorías',
            data: mesesOrdenados.map((mes) => conteos[mes]),
            ...COLOR_BARRA,
            borderWidth: 1,
        }],
    };
};

// Agrupa una lista por un campo de texto (omite valores nulos o vacíos)
const construirPorCategoria = (lista, campo, paleta, etiqueta) => {
    const conteos = {};
    lista.forEach((item) => {
        const valor = item ? item[campo] : null;
        if (!valor || !String(valor).trim()) return;
        const clave = String(valor).trim();
        conteos[clave] = (conteos[clave] || 0) + 1;
    });
    const claves = Object.keys(conteos).sort();
    return {
        labels: claves,
        datasets: [{
            label: etiqueta,
            data: claves.map((clave) => conteos[clave]),
            ...colorearCategorias(paleta, claves.length),
            borderWidth: 1,
        }],
    };
};

const VerReporte = () => {
    const classes = useStyles();
    const history = useHistory();
    const [, dispatch] = useStateValue();
    const [cargando, setCargando] = useState(true);
    const [asesoriasMesData, setAsesoriasMesData] = useState({
        labels: [],
        datasets: [{ label: 'Número de Asesorías', data: [], ...COLOR_BARRA, borderWidth: 1 }],
    });
    const [clientesDepartamentoData, setClientesDepartamentoData] = useState({
        labels: [],
        datasets: [{ label: '# de Clientes', data: [], backgroundColor: [], borderColor: [], borderWidth: 1 }],
    });
    const [contactosGeneroData, setContactosGeneroData] = useState({
        labels: [],
        datasets: [{ label: '# de Contactos', data: [], backgroundColor: [], borderColor: [], borderWidth: 1 }],
    });

    useEffect(() => {
        let cancelado = false;
        const cargarReportes = async () => {
            try {
                const [asesorias, clientes, contactos] = await Promise.all([
                    obtenerAsesorias(),
                    obtenerClientesEmpresas(),
                    obtenerContactos(),
                ]);
                if (cancelado) return;
                setAsesoriasMesData(construirAsesoriasPorMes(asesorias || []));
                setClientesDepartamentoData(construirPorCategoria(clientes || [], 'departamento', COLORES_DEPARTAMENTO, '# de Clientes'));
                setContactosGeneroData(construirPorCategoria(contactos || [], 'genero', COLORES_GENERO, '# de Contactos'));
            } catch (error) {
                console.error('Error al cargar los reportes:', error);
                if (error.response && (error.response.status === 401 || error.response.status === 403)) {
                    history.push('/auth/login');
                    return;
                }
                dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: 'Error al cargar los reportes', severity: 'error' } });
            } finally {
                if (!cancelado) setCargando(false);
            }
        };
        cargarReportes();
        return () => { cancelado = true; };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    const chartOptions = (title) => ({
        responsive: true,
        maintainAspectRatio: false, // sin esto, el aspect ratio 2:1 aplasta los gráficos en cards angostas
        legend: { position: 'top' },
        title: { display: true, text: title, fontSize: 16 },
    });

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}><Button color="inherit" className={classes.navButton}>Indicadores y Reportes</Button></Box>
                    <Box className={classes.userInfo}><Typography variant="subtitle1" style={{fontWeight: 'bold'}}>Freddy Yoel Vasquez</Typography><Typography variant="body2">CDE MYPIME ROC</Typography></Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                {cargando ? (
                    <Box className={classes.cargando}>
                        <CircularProgress />
                    </Box>
                ) : (
                    <Grid container spacing={3}>
                        <Grid item xs={12} lg={6}>
                            <Paper className={classes.reportCard}>
                                <Box style={{ height: 300 }}>
                                    <Bar options={chartOptions('Asesorías Realizadas por Mes')} data={asesoriasMesData} />
                                </Box>
                            </Paper>
                        </Grid>
                        <Grid item xs={12} md={6} lg={3}>
                            <Paper className={classes.reportCard}>
                                <Box style={{ height: 300 }}>
                                    <Doughnut options={chartOptions('Distribución de Clientes por Departamento')} data={clientesDepartamentoData} />
                                </Box>
                            </Paper>
                        </Grid>
                        <Grid item xs={12} md={6} lg={3}>
                            <Paper className={classes.reportCard}>
                                <Box style={{ height: 300 }}>
                                    <Doughnut options={chartOptions('Distribución de Contactos por Género')} data={contactosGeneroData} />
                                </Box>
                            </Paper>
                        </Grid>
                    </Grid>
                )}
            </main>
        </div>
    );
};

export default VerReporte;
