import React, { useState, useEffect } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, Tabs, Tab, Link,
    Chip
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Edit, Delete, Business, School, Phone, Email, LocationOn, Person, People, VerifiedUser } from '@material-ui/icons';
import { useParams, useHistory } from 'react-router-dom';
import { obtenerContactoPorId, obtenerAsesoriasPorContacto } from '../../actions/ContactoAction';
import { useStateValue } from '../../Context/store';
import HttpCliente from '../../services/HttpCliente';

// Estilos del componente
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565',
        minHeight: '100vh',
    },
    appBar: {
        backgroundColor: '#D5A408',
        color: '#000000',
    },
    logo: {
        height: '50px',
        marginRight: theme.spacing(2),
    },
    navLinks: {
        flexGrow: 1,
    },
    navButton: {
        fontWeight: 'bold',
        marginLeft: theme.spacing(1),
        marginRight: theme.spacing(1),
    },
    userInfo: {
        textAlign: 'right',
    },
    content: {
        padding: theme.spacing(3),
    },
    headerBar: {
        padding: theme.spacing(2, 3),
        backgroundColor: '#EAEAEA',
        borderRadius: '12px',
        display: 'flex',
        justifyContent: 'space-between',
        alignItems: 'center',
    },
    headerTitle: {
        fontWeight: 'bold',
        fontSize: '1.5rem',
    },
    newButton: { backgroundColor: '#42A5F5', color: 'white', '&:hover': { backgroundColor: '#1E88E5' } },
    editButton: { backgroundColor: '#66BB6A', color: 'white', '&:hover': { backgroundColor: '#43A047' } },
    deleteButton: { backgroundColor: '#EF5350', color: 'white', '&:hover': { backgroundColor: '#E53935' } },
    mainPaper: {
        padding: theme.spacing(2),
    },
    sideCard: {
        padding: theme.spacing(2),
        marginBottom: theme.spacing(2),
    },
    infoText: {
        marginBottom: theme.spacing(1),
        fontSize: '1rem',
    },
    infoLabel: {
        fontWeight: 'bold',
        marginRight: theme.spacing(1),
    },
    tabPanel: {
        paddingTop: theme.spacing(2),
    },
    tabsRoot: {
        // La línea de base delgada que abarca todo el ancho
        borderBottom: '1px solid #e8e8e8',
        minHeight: 'auto', // Ajustamos la altura
        padding: 0,
    },
    tabIndicator: {
        // El indicador grueso para la pestaña activa
        backgroundColor: '#1f1f1f', // Color oscuro
        height: '4px',
    },
    customTab: {
        // Estilo para cada etiqueta de pestaña individual
        textTransform: 'none', // Evita que el texto se ponga en mayúsculas
        fontWeight: 400,       // Fuente normal, no negrita
        minWidth: 'auto',      // Permitimos que la pestaña sea más angosta
        padding: theme.spacing(1, 2),
    }
}));

// Componente para mostrar una fila de información
const InfoRow = ({ label, value }) => {
    const classes = useStyles();
    return (
        <Typography className={classes.infoText}>
            <span className={classes.infoLabel}>{label}:</span>
            {value || <span className={classes.noDataText}>No disponible</span>}
        </Typography>
    );
};



const VerContacto = () => {
    const classes = useStyles();
    const { id } = useParams();
    const [tabValue, setTabValue] = useState(0);
    const [contacto, setContacto] = useState(null);
    const [asesorias, setAsesorias] = useState([]);
    const [, dispatch] = useStateValue();
    const history = useHistory();

    useEffect(() => {
        const cargarDatos = async () => {
            try {
                // Cargar datos del contacto
                const data = await obtenerContactoPorId(id);
                setContacto(data);
                
                // Cargar asesorías del contacto
                const asesoriasData = await obtenerAsesoriasPorContacto(id);
                setAsesorias(asesoriasData || []);
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los datos del contacto',
                        severity: 'error'
                    }
                });
            }
        };
        cargarDatos();
    }, [id, dispatch]);

    if (!contacto) {
        return <div>Cargando...</div>;
    }

    const handleTabChange = (event, newValue) => {
        setTabValue(newValue);
    };

    // Botón Nuevo Contacto
    const handleNuevoContacto = () => {
        history.push('/contactos/nuevo');
    };

    // Botón Editar Contacto
    const handleEditarContacto = () => {
        history.push(`/contactos/editar/${id}`);
    };

    // Botón Eliminar Contacto
    const handleEliminarContacto = async () => {
        if (window.confirm('¿Estás seguro de que deseas eliminar este contacto?')) {
            try {
                await HttpCliente.delete(`/Contacto/${id}`);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Contacto eliminado exitosamente',
                        severity: 'success'
                    }
                });
                history.push('/contactos');
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al eliminar el contacto',
                        severity: 'error'
                    }
                });
            }
        }
    };

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                {/* ... (Copiamos la AppBar del componente anterior para consistencia) ... */}
                <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}>
                        <Button color="inherit" className={classes.navButton}>Contactos</Button>
                        <Button color="inherit" className={classes.navButton}>Cliente/Empresa</Button>
                        <Button color="inherit" className={classes.navButton}>Capacitaciones y Eventos</Button>
                        <Button color="inherit" className={classes.navButton}>Asesoría</Button>
                        <Button color="inherit" className={classes.navButton}>Indicadores y Reportes</Button>
                    </Box>
                    <Box className={classes.userInfo}>
                        <Typography variant="subtitle1" style={{ fontWeight: 'bold' }}>Freddy Yoel Vasquez</Typography>
                        <Typography variant="body2">CDE MYPIME ROC</Typography>
                    </Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                {/* Barra de título y botones de acción */}
                <Paper className={classes.headerBar}>
                    <Typography className={classes.headerTitle}>Informacion del Contacto</Typography>
                    <Box>
                        <Button className={classes.newButton} startIcon={<Add />} variant="contained" onClick={handleNuevoContacto}>Nuevo Contacto</Button>
                        <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }} onClick={handleEditarContacto}>Editar Contacto</Button>
                        <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }} onClick={handleEliminarContacto}>Eliminar Contacto</Button>
                    </Box>
                </Paper>

                {/* Contenido principal en dos columnas */}
                <Grid container spacing={3} style={{ marginTop: '16px' }}>

                    {/* Columna Izquierda */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <Tabs value={tabValue} onChange={handleTabChange} classes={{ root: classes.tabsRoot, indicator: classes.tabIndicator }}>
                                <Tab label="Informacion Personal" />
                                <Tab label="Informacion del Contacto" />
                                <Tab label="Informacion Familiar" />
                                <Tab label="Notas" />
                            </Tabs>
                            <div className={classes.tabPanel}>
                                {tabValue === 0 && (
                                    <Box>
                                        <InfoRow label="Nombre" value={contacto.nombre} />
                                        <InfoRow label="Apellido" value={contacto.apellido} />
                                        <InfoRow label="Fecha de Nacimiento" value={contacto.fechaNacimiento} />
                                        <InfoRow label="DNI" value={contacto.dni} />
                                        <InfoRow label="Nacionalidad" value={contacto.nacionalidad} />
                                        <InfoRow label="Género" value={contacto.genero} />
                                        <InfoRow label="Teléfono" value={contacto.telefono} />
                                        <InfoRow label="Correo" value={<Link href={`mailto:${contacto.correo}`}>{contacto.correo}</Link>} />
                                        <InfoRow label="RTN" value={contacto.rtn} />
                                    </Box>
                                )}
                                {tabValue === 1 && (
                                    <Box>
                                        <InfoRow label="Dirección" value={contacto.direccion} />
                                        <InfoRow label="Municipio" value={contacto.municipio} />
                                        <InfoRow label="Departamento" value={contacto.departamento} />
                                        <InfoRow label="Cargo" value={contacto.cargo} />
                                        <InfoRow label="Estado Civil" value={contacto.estadoCivil?.descripcion} />
                                        <InfoRow label="Nivel de Estudio" value={contacto.nivelEstudio?.descripcion} />
                                        <InfoRow label="Categoría Laboral" value={contacto.categoriaLaboral?.descripcion} />
                                        <InfoRow label="Etnia" value={contacto.nombreEtnia} />
                                        <InfoRow label="Localidad Etnia" value={contacto.localidadEtnica} />
                                        <InfoRow label="Posee Negocio" value={contacto.poseeNegocio ? 'Sí' : 'No'} />
                                    </Box>
                                )}
                                {tabValue === 2 && (
                                    <Box>
                                        <InfoRow label="Contacto Discapacidad" value={contacto.contactoDiscapacidad} />
                                        <InfoRow label="Rol Familiar" value={contacto.rolContactoFamiliar} />
                                        <InfoRow label="Integrantes Familia" value={contacto.integrantesTotalesFamilia} />
                                        <InfoRow label="Número de Hijos" value={contacto.numeroHijos} />
                                        <InfoRow label="Número de Hijas" value={contacto.numeroHijas} />
                                    </Box>
                                )}
                                {tabValue === 3 && (
                                    <Box>
                                        <InfoRow label="Notas" value={contacto.notas} />
                                    </Box>
                                )}
                            </div>
                        </Paper>
                    </Grid>

                    {/* Columna Derecha */}
                    <Grid item xs={12} md={4}>
                        {/* Card de Cliente/Empresa */}
                        <Paper className={classes.sideCard}>
                            <Typography variant="h6" gutterBottom className={classes.cardTitle}>
                                <Business /> Empresa Vinculada
                            </Typography>
                            {contacto.clienteEmpresa ? (
                                <>
                                    {/* Nombre de la empresa */}
                                    <Typography variant="h6" style={{ fontWeight: 'bold', color: '#033565', marginBottom: 8 }}>
                                        {contacto.clienteEmpresa.nombre}
                                    </Typography>
                                    
                                    {/* Ubicación */}
                                    <Box display="flex" alignItems="center" mb={1}>
                                        <LocationOn fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Typography variant="body2">
                                            {contacto.clienteEmpresa.municipio}, {contacto.clienteEmpresa.departamento}
                                        </Typography>
                                    </Box>
                                    
                                    {/* Teléfono */}
                                    <Box display="flex" alignItems="center" mb={1}>
                                        <Phone fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Typography variant="body2">
                                            {contacto.clienteEmpresa.telefono}
                                        </Typography>
                                    </Box>
                                    
                                    {/* Correo */}
                                    <Box display="flex" alignItems="center" mb={1}>
                                        <Email fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Typography variant="body2">
                                            {contacto.clienteEmpresa.correo}
                                        </Typography>
                                    </Box>
                                    
                                    {/* Asesor Principal */}
                                    <Box display="flex" alignItems="center" mb={1}>
                                        <Person fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Typography variant="body2">
                                            Asesor: {contacto.clienteEmpresa.asesorPrincipalNombre || 'No asignado'}
                                        </Typography>
                                    </Box>
                                    
                                    {/* Número de empleados */}
                                    <Box display="flex" alignItems="center" mb={1}>
                                        <People fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Typography variant="body2">
                                            {contacto.clienteEmpresa.empleadosTiempoCompleto || 0} empleados
                                        </Typography>
                                    </Box>
                                    
                                    {/* Estado */}
                                    <Box display="flex" alignItems="center" mb={2}>
                                        <VerifiedUser fontSize="small" color="action" style={{ marginRight: 8 }} />
                                        <Chip 
                                            label={contacto.clienteEmpresa.estatusActual || 'Sin estado'} 
                                            size="small"
                                            color={contacto.clienteEmpresa.estatusActual === 'Activo' ? 'primary' : 'default'}
                                        />
                                    </Box>
                                    
                                    {/* Botón ver empresa */}
                                    <Button
                                        fullWidth
                                        variant="contained"
                                        color="primary"
                                        startIcon={<Business />}
                                        onClick={() => history.push(`/cliente_empresa/ver/${contacto.clienteEmpresa.id}`)}
                                    >
                                        Ver Detalles de la Empresa
                                    </Button>
                                </>
                            ) : (
                                <Typography className={classes.noDataText}>
                                    Este contacto no está vinculado a ninguna empresa.
                                </Typography>
                            )}
                        </Paper>
                        
                        {/* Card de Asesorías */}
                        <Paper className={classes.sideCard}>
                            <Typography variant="h6" gutterBottom className={classes.cardTitle}>
                                <School /> Asesorías del Contacto
                            </Typography>
                            {asesorias && asesorias.length > 0 ? (
                                <>
                                    {asesorias.map((asesoria, index) => (
                                        <Box key={asesoria.asesoriaId || index} className={classes.listaAsesoriaItem}>
                                            <Typography variant="body2" style={{ fontWeight: 'bold' }}>
                                                {asesoria.asunto || `Asesoría #${asesoria.asesoriaId}`}
                                            </Typography>
                                            <Typography variant="caption" color="textSecondary">
                                                {asesoria.areaAsesoria}
                                            </Typography>
                                            <Typography variant="caption" display="block" color="textSecondary">
                                                Fecha: {new Date(asesoria.fechaSesion).toLocaleDateString()}
                                            </Typography>
                                            <Typography variant="caption" color="textSecondary">
                                                Participantes: {asesoria.numeroParticipantes}
                                            </Typography>
                                        </Box>
                                    ))}
                                </>
                            ) : (
                                <Typography className={classes.noDataText}>
                                    Este contacto no tiene asesorías registradas.
                                </Typography>
                            )}
                        </Paper>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerContacto;