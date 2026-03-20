// src/components/pages/VerAsesoria.js

import React from 'react';
import { 
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, List, ListItem, ListItemText
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Add, Edit, Delete } from '@material-ui/icons';

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

// Datos de ejemplo para poblar la vista
const asesoriaData = {
    cliente: 'Café Copán',
    fechaSesion: '15 de Agosto, 2025',
    tiempoContacto: '1:30',
    tipoContacto: 'Presencial',
    areaAsesoria: 'Finanzas',
    ayudaAdicional: 'Asesoría Especializada',
    asunto: 'Análisis de Flujo de Caja Q3',
    fuenteFinanciamiento: 'Otra',
    centro: 'SERCOTEC',
    numeroParticipantes: 3,
    notas: 'Se revisó el flujo de caja del tercer trimestre. Se identificaron oportunidades de ahorro en costos de materia prima. El cliente preparará un nuevo presupuesto para la próxima sesión.',
    referidoA: 'Banco de Occidente',
    descripcionReferido: 'Se refiere al cliente para explorar opciones de crédito pyme.',
    descripcionDerivado: 'N/A',
    descripcionAsesoriaEspecializada: 'Se requiere un experto en contabilidad agrícola para optimizar la declaración de impuestos.',
    asesores: ['Kristel Padilla', 'Jose Manuel (Especialista)'],
    contactos: ['Ana Méndez (Propietaria)', 'Carlos Pineda (Contador)'],
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
                        <Button className={classes.newButton} startIcon={<Add />} variant="contained">Nueva Asesoría</Button>
                        <Button className={classes.editButton} startIcon={<Edit />} variant="contained" style={{ marginLeft: 8 }}>Editar Asesoría</Button>
                        <Button className={classes.deleteButton} startIcon={<Delete />} variant="contained" style={{ marginLeft: 8 }}>Eliminar Asesoría</Button>
                    </Box>
                </Paper>

                <Grid container spacing={3}>
                    {/* --- Columna Izquierda --- */}
                    <Grid item xs={12} md={8}>
                        <Paper className={classes.mainPaper}>
                            <InfoRow label="Cliente/Empresa" value={asesoriaData.cliente} />
                            <InfoRow label="Fecha de Sesión" value={asesoriaData.fechaSesion} />
                            <InfoRow label="Tiempo de Contacto" value={`${asesoriaData.tiempoContacto} (h:mm)`} />
                            <InfoRow label="Tipo de Contacto" value={asesoriaData.tipoContacto} />
                            <InfoRow label="Área de Asesoría" value={asesoriaData.areaAsesoria} />
                            <InfoRow label="Ayuda Adicional" value={asesoriaData.ayudaAdicional} />
                            <InfoRow label="Asunto" value={asesoriaData.asunto} />
                            <InfoRow label="Número de Asistencias" value={asesoriaData.numeroParticipantes} />
                        </Paper>
                        
                        <InfoCard title="Notas">
                            <Typography className={classes.notesSection}>{asesoriaData.notas}</Typography>
                        </InfoCard>

                        <InfoCard title="Información de Referidos">
                            <InfoRow label="Referido a" value={asesoriaData.referidoA} />
                            <InfoRow label="Descripción del Referido" value={asesoriaData.descripcionReferido} />
                        </InfoCard>

                        <InfoCard title="Información Adicional">
                             <InfoRow label="Descripción de Derivado" value={asesoriaData.descripcionDerivado} />
                             <InfoRow label="Asesoría Especializada" value={asesoriaData.descripcionAsesoriaEspecializada} />
                        </InfoCard>
                    </Grid>

                    {/* --- Columna Derecha --- */}
                    <Grid item xs={12} md={4}>
                         <InfoCard title="Asesores Involucrados">
                             <List dense>
                                {asesoriaData.asesores.map((nombre, index) => (
                                    <ListItem key={index} className={classes.listItem}>
                                        <ListItemText primary={nombre} />
                                    </ListItem>
                                ))}
                            </List>
                        </InfoCard>

                         <InfoCard title="Contactos Participantes">
                             <List dense>
                                {asesoriaData.contactos.map((nombre, index) => (
                                    <ListItem key={index} className={classes.listItem}>
                                        <ListItemText primary={nombre} />
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