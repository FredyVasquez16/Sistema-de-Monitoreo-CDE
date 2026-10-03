// src/components/pages/VerAsesoria.js

import React, { useEffect, useState } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, List, ListItem, ListItemText, CircularProgress
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Edit, Delete } from '@material-ui/icons';
import { useParams, useHistory } from 'react-router-dom';
import { useStateValue } from '../../Context/store';
import { obtenerAsesoriaPorId } from '../../actions/AsesoriaAction';

// Estilos consistentes con las vistas anteriores
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
    headerBar: {
        padding: theme.spacing(1.5, 2),
        backgroundColor: '#EAEAEA',
        borderRadius: '12px',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: theme.spacing(3),
    },
    headerTitle: { fontWeight: 'bold', fontSize: '1.5rem' },
    newButton: { backgroundColor: '#42A5F5', color: 'white', '&:hover': { backgroundColor: '#1E88E5' } },
    editButton: { backgroundColor: '#66BB6A', color: 'white', '&:hover': { backgroundColor: '#43A047' } },
    deleteButton: { backgroundColor: '#EF5350', color: 'white', '&:hover': { backgroundColor: '#E53935' } },
    mainPaper: { padding: theme.spacing(2), marginBottom: theme.spacing(3) },
    sideCard: { marginBottom: theme.spacing(2) },
    cardHeader: {
        padding: theme.spacing(1, 2),
        backgroundColor: '#f5f5f5',
        borderBottom: '1px solid #ddd',
    },
    cardTitle: { fontWeight: 'bold' },
    cardContent: { padding: theme.spacing(2) },
    infoText: { marginBottom: theme.spacing(1.5), fontSize: '1rem' },
    infoLabel: { fontWeight: 'bold', marginRight: theme.spacing(1) },
    notesSection: {
        padding: theme.spacing(2),
        whiteSpace: 'pre-wrap', // Respeta los saltos de línea en el texto
        wordBreak: 'break-word',
    },
    listItem: { padding: theme.spacing(0.5, 0) },
}));

// Formatea la fecha de sesión en un formato legible (es-HN)
const formatearFecha = (fecha) => {
    if (!fecha) return '—';
    const d = new Date(fecha);
    return isNaN(d.getTime()) ? fecha : d.toLocaleDateString('es-HN', { year: 'numeric', month: 'long', day: 'numeric' });
};

// El tiempo de contacto llega como "HH:mm:ss"; se muestran los primeros 5 caracteres (HH:mm)
const formatearTiempo = (tiempo) => {
    if (!tiempo || typeof tiempo !== 'string') return '—';
    return tiempo.slice(0, 5);
};

const InfoRow = ({ label, value }) => {
    const classes = useStyles();
    return (
        <Typography className={classes.infoText}>
            <span className={classes.infoLabel}>{label}:</span>
            {value}
        </Typography>
    );
};

const InfoCard = ({ title, children }) => {
    const classes = useStyles();
    return (
        <Paper className={classes.sideCard}>
            <Box className={classes.cardHeader}><Typography className={classes.cardTitle}>{title}</Typography></Box>
            <Box className={classes.cardContent}>{children}</Box>
        </Paper>
    );
};

const VerAsesoria = () => {
    const classes = useStyles();
    const { id } = useParams();
    const history = useHistory();
    const [, dispatch] = useStateValue();
    const [asesoria, setAsesoria] = useState(null);
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        const cargarAsesoria = async () => {
            try {
                const data = await obtenerAsesoriaPorId(id);
                setAsesoria(data);
            } catch (error) {
                console.error('Error al cargar la asesoría:', error);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Error al cargar la asesoría', severity: 'error' },
                });
            } finally {
                setCargando(false);
            }
        };

        cargarAsesoria();
    }, [id, dispatch]);

    // Indicador de carga
    if (cargando) {
        return (
            <div className={classes.root}>
                <Box display="flex" justifyContent="center" alignItems="center" minHeight="60vh">
                    <CircularProgress style={{ color: '#D5A408' }} />
                </Box>
            </div>
        );
    }

    // Estado vacío: asesoría no encontrada
    if (!asesoria) {
        return (
            <div className={classes.root}>
                <main className={classes.content}>
                    <Paper className={classes.headerBar}>
                        <Typography className={classes.headerTitle}>Asesoría no encontrada</Typography>
                    </Paper>
                </main>
            </div>
        );
    }

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                 <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}><Button color="inherit" className={classes.navButton}>Asesoría</Button>{/* ...otros botones... */}</Box>
                    <Box className={classes.userInfo}><Typography variant="subtitle1" style={{fontWeight: 'bold'}}>Freddy Yoel Vasquez</Typography><Typography variant="body2">CDE MYPIME ROC</Typography></Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                <Paper className={classes.headerBar}>
                    <Typography className={classes.headerTitle}>Información de la Asesoría</Typography>
                    <Box>
                        <Button className={classes.newButton} startIcon={<Add />} variant="contained" onClick={() => history.push('/asesoria/nuevo')}>Nueva Asesoría</Button>
                        <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }} onClick={() => history.push(`/asesoria/editar/${id}`)}>Editar Asesoría</Button>
                        <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }}>Eliminar Asesoría</Button>
                    </Box>
                </Paper>

                <Grid container spacing={3}>
                    {/* --- Columna Izquierda --- */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <InfoRow label="Cliente/Empresa" value={asesoria.clienteNombre} />
                            <InfoRow label="Fecha de Sesión" value={formatearFecha(asesoria.fechaSesion)} />
                            <InfoRow label="Tiempo de Contacto" value={asesoria.tiempoContacto ? `${formatearTiempo(asesoria.tiempoContacto)} (h:mm)` : '—'} />
                            <InfoRow label="Tipo de Contacto" value={asesoria.tipoContactoNombre} />
                            <InfoRow label="Área de Asesoría" value={asesoria.areaAsesoriaNombre} />
                            <InfoRow label="Ayuda Adicional" value={asesoria.ayudaAdicional} />
                            <InfoRow label="Asunto" value={asesoria.asunto} />
                            <InfoRow label="Número de Asistencias" value={asesoria.numeroParticipantes} />
                            <InfoRow label="Fuente de Financiamiento" value={asesoria.fuenteFinanciamientoNombre} />
                        </Paper>

                        <InfoCard title="Notas">
                            <Typography className={classes.notesSection}>{asesoria.notas}</Typography>
                        </InfoCard>

                        <InfoCard title="Información de Referidos">
                            <InfoRow label="Referido a" value={asesoria.referidoA} />
                            <InfoRow label="Descripción del Referido" value={asesoria.descripcionReferido} />
                        </InfoCard>

                        <InfoCard title="Información Adicional">
                             <InfoRow label="Descripción de Derivado" value={asesoria.descripcionDerivado} />
                             <InfoRow label="Asesoría Especializada" value={asesoria.descripcionAsesoriaEspecializada} />
                        </InfoCard>
                    </Grid>

                    {/* --- Columna Derecha --- */}
                    <Grid item xs={12} md={4}>
                         <InfoCard title="Asesores Involucrados">
                             <List dense>
                                {(asesoria.asesores || []).map((asesor, index) => (
                                    <ListItem key={index} className={classes.listItem}>
                                        <ListItemText primary={asesor.nombreCompleto} />
                                    </ListItem>
                                ))}
                            </List>
                        </InfoCard>

                         <InfoCard title="Contactos Participantes">
                             <List dense>
                                {(asesoria.contactos || []).map((contacto, index) => (
                                    <ListItem key={index} className={classes.listItem}>
                                        <ListItemText primary={contacto.contactoNombre} />
                                    </ListItem>
                                ))}
                            </List>
                        </InfoCard>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerAsesoria;
