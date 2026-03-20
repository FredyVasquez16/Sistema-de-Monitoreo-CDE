import React, { useState, useEffect, useCallback } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, Tabs, Tab, Link, List, ListItem,
    ListItemText, useTheme, useMediaQuery, IconButton, Menu, MenuItem, CircularProgress, Icon
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Edit, Delete, MoreVert } from '@material-ui/icons';
import { useHistory, useParams } from 'react-router-dom';
import { obtenerClienteEmpresaPorId, eliminarClienteEmpresa } from '../../actions/ClienteEmpresaAction';
import { useStateValue } from '../../Context/store';

// Estilos unificados (corregidos y organizados)
const useStyles = makeStyles((theme) => ({
    root: {
        backgroundColor: '#033565',
        minHeight: '100vh',
        padding: theme.spacing(3),
    },
    headerBar: {
        padding: theme.spacing(1.5, 2),
        backgroundColor: '#EAEAEA',
        borderRadius: '12px',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
        marginBottom: theme.spacing(3),
    },
    headerTitle: { fontWeight: 'bold', fontSize: '1.5rem', color: '#033565' },
    newButton: { backgroundColor: '#42A5F5', color: 'white', '&:hover': { backgroundColor: '#1E88E5' } },
    editButton: { backgroundColor: '#66BB6A', color: 'white', '&:hover': { backgroundColor: '#43A047' } },
    deleteButton: { backgroundColor: '#EF5350', color: 'white', '&:hover': { backgroundColor: '#E53935' } },
    mainPaper: { padding: theme.spacing(2) },
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
    infoText: { marginBottom: theme.spacing(1), fontSize: '0.95rem' },
    infoLabel: { fontWeight: 'bold', marginRight: theme.spacing(1) },
    tabPanel: { paddingTop: theme.spacing(2) },
    tabsRoot: { borderBottom: '1px solid #e8e8e8', minHeight: 'auto' },
    tabIndicator: { backgroundColor: '#033565', height: '4px' },
    customTab: { textTransform: 'none', fontWeight: 600, fontSize: '1rem', minWidth: 'auto', padding: theme.spacing(1, 2) },
    actionLink: { color: '#EF5350', cursor: 'pointer', fontWeight: 'bold', fontSize: '0.8rem' },
    listItem: { padding: theme.spacing(0.5, 0) },
    menuItemIcon: { marginRight: theme.spacing(1) },
}));

// Componentes de UI reutilizables
const InfoRow = ({ label, value }) => {
    const classes = useStyles();
    if (!value) return null; // No renderizar si no hay valor
    return (
        <Typography component="div" className={classes.infoText}>
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

const VerClienteEmpresa = () => {
    const classes = useStyles();
    const { id } = useParams();
    const history = useHistory();
    const [, dispatch] = useStateValue();

    const [cliente, setCliente] = useState(null);
    const [loading, setLoading] = useState(true);
    const [tabValue, setTabValue] = useState(0);
    const [anchorEl, setAnchorEl] = useState(null);

    const theme = useTheme();
    const isMobile = useMediaQuery(theme.breakpoints.down('sm'));

    // Carga de datos con manejo de componente desmontado
    useEffect(() => {
        let isMounted = true;
        const cargarCliente = async () => {
            try {
                const data = await obtenerClienteEmpresaPorId(id);
                if (isMounted) {
                    setCliente(data);
                }
            } catch (error) {
                console.error("Error al cargar el cliente/empresa:", error);
                if (isMounted) {
                    dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: 'Error al cargar el cliente/empresa.', severity: 'error' } });
                }
            } finally {
                if (isMounted) {
                    setLoading(false);
                }
            }
        };

        cargarCliente();
        return () => { isMounted = false; };
    }, [id, dispatch]);

    // Handlers
    const handleTabChange = (event, newValue) => setTabValue(newValue);
    const handleMenuClick = (event) => setAnchorEl(event.currentTarget);
    const handleMenuClose = () => setAnchorEl(null);

    const handleNavigate = (path) => {
        history.push(path);
    };

    const handleEliminarCliente = async () => {
        if (window.confirm('¿Estás seguro de que deseas eliminar este cliente/empresa?')) {
            try {
                await eliminarClienteEmpresa(id);
                dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: 'Cliente/Empresa eliminado exitosamente.', severity: 'success' } });
                history.push('/cliente_empresa');
            } catch (error) {
                dispatch({ type: 'OPEN_SNACKBAR', payload: { open: true, mensaje: 'Error al eliminar el cliente/empresa.', severity: 'error' } });
            }
        }
    };

    // Componentes JSX para los botones de acción
    const actionButtons = (
        <Box>
            <Button className={classes.newButton} startIcon={<Add />} variant="contained" onClick={() => handleNavigate('/cliente_empresa/nuevo')}>Nuevo Cliente</Button>
            <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }} onClick={() => handleNavigate(`/cliente_empresa/editar/${id}`)}>Editar</Button>
            <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }} onClick={handleEliminarCliente}>Eliminar</Button>
        </Box>
    );

    const mobileMenu = (
        <div>
            <IconButton color="inherit" onClick={handleMenuClick}><MoreVert /></IconButton>
            <Menu anchorEl={anchorEl} keepMounted open={Boolean(anchorEl)} onClose={handleMenuClose}>
                <MenuItem onClick={() => { handleMenuClose(); handleNavigate('/cliente_empresa/nuevo'); }}><Icon className={classes.menuItemIcon}>add</Icon> Nuevo Cliente</MenuItem>
                <MenuItem onClick={() => { handleMenuClose(); handleNavigate(`/cliente_empresa/editar?id=${id}`); }}><Icon className={classes.menuItemIcon}>edit</Icon> Editar Cliente</MenuItem>
                <MenuItem onClick={() => { handleMenuClose(); handleEliminarCliente(); }}><Icon className={classes.menuItemIcon}>delete</Icon> Eliminar Cliente</MenuItem>
            </Menu>
        </div>
    );

    // Estados de Carga y No Encontrado
    if (loading) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh" className={classes.root}>
                <CircularProgress style={{ color: '#D5A408' }} />
            </Box>
        );
    }

    if (!cliente) {
        return <div className={classes.root}><Typography style={{ color: 'white', padding: 32 }}>No se encontró la información del cliente/empresa.</Typography></div>;
    }

    return (
        <div className={classes.root}>
            <main>
                <Paper className={classes.headerBar}>
                    <Typography className={classes.headerTitle}>Información del Cliente / Empresa</Typography>
                    {isMobile ? mobileMenu : actionButtons}
                </Paper>

                <Grid container spacing={3}>
                    {/* --- Columna Izquierda --- */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <Tabs
                                value={tabValue}
                                onChange={handleTabChange}
                                classes={{ root: classes.tabsRoot, indicator: classes.tabIndicator }}
                                variant="scrollable"
                                scrollButtons="auto"
                            >
                                <Tab label="Información General" className={classes.customTab} />
                                <Tab label="Perfil Detallado" className={classes.customTab} />
                                <Tab label="Actividad Comercial" className={classes.customTab} />
                                <Tab label="Finanzas" className={classes.customTab} />
                                <Tab label="Notas" className={classes.customTab} />
                            </Tabs>

                            <div className={classes.tabPanel}>
                                {/* Pestaña 1: Información General */}
                                {tabValue === 0 && (
                                    <Box>
                                        <InfoRow label="Código Único" value={cliente.codigoUnico} />
                                        <InfoRow label="Nombre de la Empresa" value={cliente.nombre} />
                                        <InfoRow label="Razón Social" value={cliente.razonSocial} />
                                        <InfoRow label="Estatus Actual" value={cliente.estatusActual} />
                                        <InfoRow label="Municipio" value={cliente.municipio} />
                                        <InfoRow label="Departamento" value={cliente.departamento} />
                                        <InfoRow label="Descripción" value={cliente.descripcionProductoServicio} />
                                    </Box>
                                )}

                                {/* Pestaña 2: Perfil Detallado */}
                                {tabValue === 1 && (
                                    <Box>
                                        <InfoRow label="Nivel de Cliente" value={cliente.tipoClienteNivel?.descripcion} />
                                        <InfoRow label="Tipo de Organización" value={cliente.tipoOrganizacion?.descripcion} />
                                        <InfoRow label="Tipo de Empresa" value={cliente.tipoEmpresa?.descripcion} />
                                        <InfoRow label="Tamaño de Empresa" value={cliente.tamanoEmpresa?.descripcion} />
                                        <InfoRow label="Nivel de Formalización" value={cliente.nivelFormalizacion?.descripcion} />
                                        <InfoRow label="Fecha de Inicio" value={new Date(cliente.fechaInicio).toLocaleDateString()} />
                                        <InfoRow label="Fecha de Establecimiento" value={new Date(cliente.fechaEstablecimiento).toLocaleDateString()} />
                                    </Box>
                                )}

                                {/* Pestaña 3: Actividad Comercial */}
                                {tabValue === 2 && (
                                    <Box>
                                        <InfoRow label="Negocio en Línea" value={cliente.negocioEnLinea ? 'Sí' : 'No'} />
                                        <InfoRow label="Negocio en Casa" value={cliente.negocioEnCasa ? 'Sí' : 'No'} />
                                        <InfoRow label="Comercio Internacional" value={cliente.comercioInternacional?.descripcion} />
                                        <InfoRow label="Países a los que exporta" value={cliente.paisExporta} />
                                        <InfoRow label="Participa en Gremio" value={cliente.participaGremio ? 'Sí' : 'No'} />
                                        <InfoRow label="Potencial Contrato con Gobierno" value={cliente.contratoGobierno ? 'Sí' : 'No'} />
                                    </Box>
                                )}

                                {/* Pestaña 4: Finanzas */}
                                {tabValue === 3 && (
                                    <Box>
                                        <InfoRow label="Ingresos Brutos Anuales" value={cliente.ingresosBrutosAnuales?.toLocaleString('es-HN', { style: 'currency', currency: 'HNL' })} />
                                        <InfoRow label="Fecha de Ingresos" value={new Date(cliente.fechaIngresosBrutos).toLocaleDateString()} />
                                        <InfoRow label="Fuente de Financiamiento" value={cliente.fuenteFinanciamiento?.descripcion} />
                                        <InfoRow label="Ha solicitado crédito" value={cliente.haSolicitadoCredito ? 'Sí' : 'No'} />
                                        <InfoRow label="Utiliza Medios de Pago Electrónicos" value={cliente.usaPagoElectronico ? 'Sí' : 'No'} />
                                    </Box>
                                )}

                                {/* Pestaña 5: Notas */}
                                {tabValue === 4 && (
                                    <Box>
                                        <InfoRow label="Notas" value={cliente.notas} />
                                    </Box>
                                )}
                            </div>
                        </Paper>
                    </Grid>

                    {/* --- Columna Derecha --- */}
                    <Grid item xs={12} md={4}>
                        <InfoCard title="Contactos Clave">
                            <List dense>
                                <ListItem className={classes.listItem}>
                                    <ListItemText primary="Contacto Primario" secondary={cliente.contactoPrimarioNombre || 'No asignado'} />
                                </ListItem>
                                <ListItem className={classes.listItem}>
                                    <ListItemText primary="Propietario" secondary={cliente.nombrePropietarioNombre || 'No asignado'} />
                                </ListItem>
                                <ListItem className={classes.listItem}>
                                    <ListItemText primary="Género del Propietario" secondary={cliente.generoPropietario} />
                                </ListItem>
                            </List>
                        </InfoCard>

                        <InfoCard title="Asesoría y Seguimiento">
                            <List dense>
                                <ListItem className={classes.listItem}>
                                    <ListItemText primary="Asesor Principal" secondary={cliente.asesorPrincipalNombre || 'No asignado'} />
                                </ListItem>
                                <ListItem className={classes.listItem}>
                                    <ListItemText primary="Servicio Solicitado" secondary={cliente.servicioSolicitado?.descripcion || 'No especificado'} />
                                </ListItem>
                            </List>
                        </InfoCard>

                        <InfoCard title="Capacitaciones Recibidas" actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />}>Asignar</Button>}>
                            <List dense>
                                {/* Aquí iría un mapeo de las capacitaciones del cliente */}
                                <ListItem className={classes.listItem}><ListItemText primary="Finanzas para Emprendedores" /></ListItem>
                                <ListItem className={classes.listItem}><ListItemText primary="Marketing Digital Básico" /></ListItem>
                            </List>
                        </InfoCard>

                        <InfoCard title="Asesorías Recibidas" actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />}>Registrar</Button>}>
                            <List dense>
                                {/* Aquí iría un mapeo de las asesorías del cliente */}
                                <ListItem className={classes.listItem}><ListItemText primary="Asistencia Técnica en Plan de Negocios" /></ListItem>
                            </List>
                        </InfoCard>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerClienteEmpresa;