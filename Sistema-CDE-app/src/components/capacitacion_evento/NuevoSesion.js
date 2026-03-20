import React, { useState } from 'react';
import { 
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField, Switch, FormControlLabel
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { validarSesion } from '../../validators/SesionValidator'; // Asegúrate que la ruta sea correcta

const useStyles = makeStyles((theme) => ({
    root: {
        backgroundColor: '#033565',
        minHeight: '100vh',
        paddingBottom: theme.spacing(4),
    },
    appBar: { backgroundColor: '#D5A408', color: '#000000' },
    logo: { height: '50px', marginRight: theme.spacing(2) },
    navLinks: { flexGrow: 1 },
    navButton: { fontWeight: 'bold', margin: theme.spacing(0, 1) },
    userInfo: { textAlign: 'right' },
    content: {
        padding: theme.spacing(3),
        maxWidth: '960px',
        margin: '0 auto',
    },
    mainTitle: {
        color: 'white',
        marginBottom: theme.spacing(2),
        textAlign: 'center',
    },
    formSection: {
        backgroundColor: '#EAEAEA',
        padding: theme.spacing(3),
        borderRadius: '12px',
        marginBottom: theme.spacing(3),
    },
    inputLabel: {
        fontWeight: 'bold',
        marginBottom: theme.spacing(0.5),
        fontSize: '0.9rem',
    },
    textField: {
        '& .MuiOutlinedInput-root': {
            backgroundColor: 'white',
        },
    },
    buttonContainer: { marginTop: theme.spacing(2) },
    buttonGuardar: {
        backgroundColor: '#4CAF50',
        color: 'white',
        fontWeight: 'bold',
        '&:hover': { backgroundColor: '#45a049' },
    },
    buttonCancelar: {
        backgroundColor: '#F44336',
        color: 'white',
        fontWeight: 'bold',
        '&:hover': { backgroundColor: '#d32f2f' },
    },
    switchContainer: {
        display: 'flex',
        alignItems: 'center',
        height: '100%',
    }
}));

const FormField = ({ label, children, required=true, ...props }) => {
    const classes = useStyles();
    return (
        <Grid item xs={12} {...props}>
            <Typography className={classes.inputLabel}>{label} {required && '*'}</Typography>
            {children}
        </Grid>
    );
};

const NuevoSesion = () => {
    const classes = useStyles();
    const history = useHistory();
    const [formState, setFormState] = useState({
        titulo: '', fechaInicio: '', horaInicio: '', fechaFinal: '', horaFinal: '',
        tipo: '', nombre: '', precio: '', descripcion: '', publicado: false
    });
    
    const [errors, setErrors] = useState({});
    
    const handleChange = (event) => {
        const { name, value, checked, type } = event.target;
        setFormState(prevState => ({ 
            ...prevState, 
            [name]: type === 'checkbox' ? checked : value 
        }));
    };

    const handleCancel = () => {
        history.push('/capacitacion_evento/ver');
    };
    
    const handleSubmit = (e) => {
        e.preventDefault();
        const validationErrors = validarSesion(formState);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            console.log("Formulario válido, enviando datos:", formState);
            alert("Sesión guardada con éxito (simulación).");
            history.push('/capacitacion_evento/ver');
        } else {
            console.log("Errores de validación:", validationErrors);
        }
    };

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                 <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}><Button color="inherit" className={classes.navButton}>Capacitaciones y Eventos</Button></Box>
                    <Box className={classes.userInfo}><Typography variant="subtitle1" style={{fontWeight: 'bold'}}>Freddy Yoel Vasquez</Typography><Typography variant="body2">CDE MYPIME ROC</Typography></Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                <Typography variant="h4" className={classes.mainTitle}>Añadir Nueva Sesión</Typography>
                <form onSubmit={handleSubmit}>
                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                           <FormField label="Título de la Sesión" md={12}>
                               <TextField name="titulo" fullWidth variant="outlined" className={classes.textField} value={formState.titulo} onChange={handleChange} error={!!errors.titulo} helperText={errors.titulo} />
                           </FormField>
                           <FormField label="Fecha de Inicio" md={6}>
                               <TextField name="fechaInicio" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.fechaInicio} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.fechaInicio} helperText={errors.fechaInicio} />
                           </FormField>
                           <FormField label="Hora de Inicio" md={6}>
                               <TextField name="horaInicio" type="time" fullWidth variant="outlined" className={classes.textField} value={formState.horaInicio} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.horaInicio} helperText={errors.horaInicio} />
                           </FormField>
                           <FormField label="Fecha de Finalización" md={6}>
                               <TextField name="fechaFinal" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.fechaFinal} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.fechaFinal} helperText={errors.fechaFinal} />
                           </FormField>
                           <FormField label="Hora de Finalización" md={6}>
                               <TextField name="horaFinal" type="time" fullWidth variant="outlined" className={classes.textField} value={formState.horaFinal} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.horaFinal} helperText={errors.horaFinal} />
                           </FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                         <Grid container spacing={3} alignItems="center">
                            <FormField label="Nombre de la Cuota" md={4} required={false}>
                                <TextField name="nombre" fullWidth variant="outlined" className={classes.textField} value={formState.nombre} onChange={handleChange} />
                            </FormField>
                            <FormField label="Tipo" md={3} required={false}>
                                <TextField name="tipo" fullWidth variant="outlined" className={classes.textField} value={formState.tipo} onChange={handleChange} />
                            </FormField>
                            <FormField label="Precio" md={3} required={false}>
                                <TextField name="precio" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.precio} onChange={handleChange} />
                            </FormField>
                            <Grid item xs={12} md={2}>
                                <div className={classes.switchContainer}>
                                     <FormControlLabel control={<Switch checked={formState.publicado} onChange={handleChange} name="publicado" />} label="Publicado" />
                                </div>
                            </Grid>
                            <FormField label="Descripción de la Cuota" md={12} required={false}>
                                <TextField name="descripcion" multiline rows={3} fullWidth variant="outlined" className={classes.textField} value={formState.descripcion} onChange={handleChange} />
                            </FormField>
                        </Grid>
                    </Paper>

                    <Grid container spacing={2} justify="flex-end" className={classes.buttonContainer}>
                        <Grid item><Button type="submit" variant="contained" className={classes.buttonGuardar}>Guardar Sesión</Button></Grid>
                        <Grid item><Button variant="contained" className={classes.buttonCancelar} onClick={handleCancel}>Cancelar</Button></Grid>
                    </Grid>
                </form>
            </main>
        </div>
    );
};

export default NuevoSesion;