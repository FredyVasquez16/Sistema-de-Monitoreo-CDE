import React from 'react';
import { 
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, List, ListItem, ListItemText, Chip, Link
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Edit, Delete } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';

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

// Datos de ejemplo
const capacitacionData = {
    titulo: 'Marketing Digital en Redes Sociales',
    descripcion: 'Aprende a crear y gestionar campañas de marketing efectivas en las principales redes sociales.',
    fechaInicio: '15 de Septiembre, 2025',
    fechaCierre: '17 de Septiembre, 2025',
    temaPrincipal: 'Marketing',
    estado: 'Abierto',
    temas: ['Marketing y Ventas', 'Social Media', 'Comercio Electrónico'],
    sesiones: [
        { id: 1, titulo: 'Introducción a Facebook e Instagram Ads', fecha: '2025-09-15' },
        { id: 2, titulo: 'Creación de Contenido Atractivo', fecha: '2025-09-16' },
        { id: 3, titulo: 'Análisis de Métricas y ROI', fecha: '2025-09-17' },
    ],
    participantes: ['Ana Méndez (Café Copán)', 'Juan Pérez (JP Soluciones)'],
    instructores: ['Gabriela Castillo (Experta en Marketing)'],
};

const VerCapacitacionEvento = () => {
    const classes = useStyles();
    const history = useHistory();

    return (
        <div className={classes.root}>
             <AppBar position="static" className={classes.appBar}>
                <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}><Button color="inherit" className={classes.navButton}>Capacitaciones y Eventos</Button>{/* ...otros botones... */}</Box>
                    <Box className={classes.userInfo}><Typography variant="subtitle1" style={{fontWeight: 'bold'}}>Freddy Yoel Vasquez</Typography><Typography variant="body2">CDE MYPIME ROC</Typography></Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                <Paper className={classes.headerBar}>
                    <Typography className={classes.headerTitle}>Detalle de la Capacitación</Typography>
                    <Box>
                        <Button className={classes.newButton} startIcon={<Add />} variant="contained" onClick={() => history.push('/capacitacion_evento/nuevo')}>Nueva</Button>
                        <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }}>Editar</Button>
                        <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }}>Eliminar</Button>
                    </Box>
                </Paper>

                <Grid container spacing={3}>
                    {/* --- Columna Izquierda --- */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <Typography variant="h5" gutterBottom style={{ fontWeight: 'bold' }}>{capacitacionData.titulo}</Typography>
                            <InfoRow label="Fecha" value={`${capacitacionData.fechaInicio} - ${capacitacionData.fechaCierre}`} />
                            <InfoRow label="Tema Principal" value={capacitacionData.temaPrincipal} />
                            <InfoRow label="Estado" value={capacitacionData.estado} />
                            <Typography variant="subtitle1" style={{ fontWeight: 'bold', marginTop: 16 }}>Temas a Tratar:</Typography>
                            <Box className={classes.chipContainer}>
                                {capacitacionData.temas.map(tema => <Chip key={tema} label={tema} />)}
                            </Box>
                        </Paper>
                        
                        {/* --- NUEVA SECCIÓN DE SESIONES --- */}
                        <InfoCard 
                            title="Sesiones Programadas"
                            actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />} onClick={() => history.push('/capacitacion_evento/sesion/nueva')}>Añadir Sesión</Button>}
                        >
                            <List dense>
                                {capacitacionData.sesiones.map(sesion => (
                                     <ListItem key={sesion.id} divider>
                                        <ListItemText 
                                            primary={sesion.titulo} 
                                            secondary={`Fecha: ${sesion.fecha}`} 
                                        />
                                        <Link component="button" variant="body2" style={{marginRight: '16px'}}>Editar</Link>
                                        <Link component="button" variant="body2" className={classes.actionLink}>Eliminar</Link>
                                     </ListItem>
                                ))}
                            </List>
                        </InfoCard>
                    </Grid>

                    {/* --- Columna Derecha --- */}
                    <Grid item xs={12} md={4}>
                         <InfoCard 
                            title="Participantes Inscritos"
                            actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />}>Añadir</Button>}
                        >
                            <List dense>
                                {capacitacionData.participantes.map(nombre => <ListItem key={nombre}><ListItemText primary={nombre} /></ListItem>)}
                            </List>
                        </InfoCard>

                         <InfoCard 
                            title="Instructores"
                            actionButton={<Button size="small" variant="contained" className={classes.newButton} startIcon={<Add />}>Añadir</Button>}
                        >
                            <List dense>
                               {capacitacionData.instructores.map(nombre => <ListItem key={nombre}><ListItemText primary={nombre} /></ListItem>)}
                            </List>
                        </InfoCard>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerCapacitacionEvento;