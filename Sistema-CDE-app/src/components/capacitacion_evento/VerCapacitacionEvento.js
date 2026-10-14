import React, { useEffect, useState } from 'react';
import {
    Typography, Button, Box, Paper, Grid, CircularProgress
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Delete, Edit } from '@material-ui/icons';
import { useHistory, useParams } from 'react-router-dom';
import { useStateValue } from '../../Context/store';
import { obtenerCapacitacionPorId, eliminarCapacitacion, obtenerTemasCapacitacion, obtenerTiposCapacitacion, obtenerFormatosPrograma } from '../../actions/CapacitacionEventoAction';

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
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        padding: theme.spacing(1, 2),
        backgroundColor: '#f5f5f5',
        borderBottom: '1px solid #ddd',
    },
    cardTitle: { fontWeight: 'bold' },
    cardContent: { padding: theme.spacing(2) },
    infoText: { marginBottom: theme.spacing(1.5), fontSize: '1rem' },
    infoLabel: { fontWeight: 'bold', marginRight: theme.spacing(1) },
    chipContainer: { display: 'flex', flexWrap: 'wrap', gap: theme.spacing(1), marginTop: theme.spacing(1) },
    actionLink: { color: '#EF5350', cursor: 'pointer', fontWeight: 'bold', fontSize: '0.8rem' },
}));

const InfoRow = ({ label, value }) => {
    const classes = useStyles();
    return (
        <Typography className={classes.infoText}>
            <span className={classes.infoLabel}>{label}:</span>
            {value}
        </Typography>
    );
};

const InfoCard = ({ title, actionButton, children }) => {
    const classes = useStyles();
    return (
        <Paper className={classes.sideCard}>
            <Box className={classes.cardHeader}>
                <Typography className={classes.cardTitle}>{title}</Typography>
                {actionButton}
            </Box>
            <Box className={classes.cardContent}>{children}</Box>
        </Paper>
    );
};

// Formatea una fecha en un formato legible (es-HN)
const formatearFecha = (fecha) => {
    if (!fecha) return '—';
    const d = new Date(fecha);
    return isNaN(d.getTime()) ? fecha : d.toLocaleDateString('es-HN', { year: 'numeric', month: 'long', day: 'numeric' });
};

// La hora programada llega como "HH:mm:ss"; se muestran los primeros 5 caracteres (HH:mm)
const formatearHora = (hora) => {
    if (!hora || typeof hora !== 'string') return '—';
    return hora.slice(0, 5);
};

// Obtiene el valor de un campo de la entidad aceptando PascalCase o camelCase
const obtenerCampo = (entidad, campo) => {
    const camel = campo.charAt(0).toLowerCase() + campo.slice(1);
    const valor = entidad[campo] !== undefined ? entidad[campo] : entidad[camel];
    return valor !== undefined && valor !== null ? valor : null;
};

// Traduce un id de catálogo a su descripción; '—' si no se encuentra
const nombreCatalogo = (lista, id) => {
    if (id === null || id === undefined) return '—';
    const item = (lista || []).find(x => x.id === Number(id));
    return item ? item.descripcion : '—';
};

const VerCapacitacionEvento = () => {
    const classes = useStyles();
    const { id } = useParams();
    const history = useHistory();
    const [, dispatch] = useStateValue();
    const [capacitacion, setCapacitacion] = useState(null);
    const [cargando, setCargando] = useState(true);
    // Catálogos para traducir los ids a nombres descriptivos
    const [temas, setTemas] = useState([]);
    const [tipos, setTipos] = useState([]);
    const [formatos, setFormatos] = useState([]);

    useEffect(() => {
        const cargarCapacitacion = async () => {
            try {
                const [data, listaTemas, listaTipos, listaFormatos] = await Promise.all([
                    obtenerCapacitacionPorId(id),
                    obtenerTemasCapacitacion(),
                    obtenerTiposCapacitacion(),
                    obtenerFormatosPrograma(),
                ]);
                setCapacitacion(data);
                setTemas(listaTemas || []);
                setTipos(listaTipos || []);
                setFormatos(listaFormatos || []);
            } catch (error) {
                console.error('Error al cargar la capacitación:', error);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Error al cargar la capacitación', severity: 'error' },
                });
            } finally {
                setCargando(false);
            }
        };

        cargarCapacitacion();
    }, [id, dispatch]);

    const handleEliminar = async () => {
        if (!window.confirm('¿Eliminar esta capacitación? Esta acción no se puede deshacer.')) return;
        try {
            await eliminarCapacitacion(id);
            dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: 'Capacitación eliminada.', severity: 'success' } });
            history.push('/capacitacion_evento');
        } catch (error) {
            console.error('Error al eliminar la capacitación:', error);
            dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: error.response?.data?.mensaje || 'No se pudo eliminar la capacitación.', severity: 'error' } });
        }
    };

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

    // Estado vacío: capacitación no encontrada
    if (!capacitacion) {
        return (
            <div className={classes.root}>
                <main className={classes.content}>
                    <Paper className={classes.headerBar}>
                        <Typography className={classes.headerTitle}>Capacitación no encontrada</Typography>
                    </Paper>
                </main>
            </div>
        );
    }

    return (
        <div className={classes.root}>

            <main className={classes.content}>
                <Paper className={classes.headerBar}>
                    <Typography className={classes.headerTitle}>Detalle de la Capacitación</Typography>
                    <Box>
                        <Button className={classes.newButton} startIcon={<Add />} variant="contained" onClick={() => history.push('/capacitacion_evento/nuevo')}>Nueva</Button>
                        <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }} onClick={() => history.push(`/capacitacion_evento/editar/${id}`)}>Editar</Button>
                        <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }} onClick={handleEliminar}>Eliminar</Button>
                    </Box>
                </Paper>

                <Grid container spacing={3}>
                    {/* --- Columna Izquierda --- */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <Typography variant="h5" gutterBottom style={{ fontWeight: 'bold' }}>{capacitacion.titulo}</Typography>
                            <InfoRow label="Fecha" value={`${formatearFecha(capacitacion.fechaInicio)} - ${formatearFecha(capacitacion.fechaCierre)}`} />
                            <InfoRow label="Fecha de Informes" value={formatearFecha(capacitacion.fechaInformes)} />
                            <InfoRow label="Hora Programada" value={formatearHora(capacitacion.horaProgramada)} />
                            <InfoRow label="Total de Horas" value={capacitacion.totalHoras != null ? capacitacion.totalHoras : '—'} />
                            <InfoRow label="Tema Principal" value={nombreCatalogo(temas, obtenerCampo(capacitacion, 'TemaPrincipalId'))} />
                            <InfoRow label="Tipo de Capacitación/Evento" value={nombreCatalogo(tipos, obtenerCampo(capacitacion, 'TipoId'))} />
                            <InfoRow label="Formato del Programa" value={nombreCatalogo(formatos, obtenerCampo(capacitacion, 'FormatoProgramaId'))} />
                            <InfoRow label="Estado" value={capacitacion.estado || '—'} />
                            <InfoRow label="Descripción" value={capacitacion.descripcion} />
                        </Paper>

                        {/* --- SECCIÓN DE SESIONES --- */}
                        <InfoCard
                            title="Sesiones Programadas"
                            actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />} onClick={() => history.push('/capacitacion_evento/sesion/nueva')}>Añadir Sesión</Button>}
                        >
                            <InfoRow label="Número de Sesiones" value={capacitacion.numeroSesiones} />
                        </InfoCard>
                    </Grid>

                    {/* --- Columna Derecha --- */}
                    <Grid item xs={12} md={4}>
                         <InfoCard title="Participantes">
                            <InfoRow label="Número Máximo de Participantes" value={capacitacion.numeroMaxParticipantes != null ? capacitacion.numeroMaxParticipantes : '—'} />
                        </InfoCard>

                         <InfoCard title="Ubicación">
                            <InfoRow label="Lugar de Desarrollo" value={capacitacion.lugarDesarrollo} />
                            <InfoRow label="Dirección" value={capacitacion.direccion} />
                            <InfoRow label="Municipio" value={capacitacion.municipio} />
                            <InfoRow label="Departamento" value={capacitacion.departamento} />
                        </InfoCard>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerCapacitacionEvento;
