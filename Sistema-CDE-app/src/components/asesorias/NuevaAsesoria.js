import React, { useState } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    Select, MenuItem, FormControl, InputAdornment, IconButton, FormHelperText
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { Search } from '@material-ui/icons';
import { validarAsesoria } from '../../validators/AsesoriaValidator';

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
    content: { padding: theme.spacing(3) },
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
    placeholder: { color: "#aaa" },
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
    }
}));

const FormField = ({ label, children, required = true, ...props }) => {
    const classes = useStyles();
    return (
        <Grid item xs={12} md={4} {...props}>
            <Typography className={classes.inputLabel}>{label} {required && '*'}</Typography>
            {children}
        </Grid>
    );
};

const NuevaAsesoria = () => {
    const classes = useStyles();
    const history = useHistory();

    const [formState, setFormState] = useState({
        clienteId: '', fechaSesion: '', tiempoContacto: '', tipoContactoId: '', areaAsesoriaId: '',
        ayudaAdicional: '', asunto: '', fuenteFinanciamientoId: '', centro: 'CDE MIPYME ROC', numeroParticipantes: '',
        notas: '', referidoA: '', descripcionReferido: '', descripcionDerivado: '',
        descripcionAsesoriaEspecializada: '', listaAsesores: '', listaContactos: ''
    });

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setFormState((anterior) => ({
            ...anterior,
            [name]: value
        }));
    }

    const [errors, setErrors] = useState({});

    const handleCancel = () => { history.goBack(); };

    const handleSubmit = (e) => {
        e.preventDefault();
        const validationErrors = validarAsesoria(formState);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            console.log("Formulario válido, enviando datos:", formState);
            alert("Asesoría guardada con éxito (simulación).");
            history.push('/asesorias');
        } else {
            console.log("Errores de validación:", validationErrors);
        }
    };

    const renderSelect = (name, placeholder, items) => (
        <FormControl variant="outlined" fullWidth className={classes.textField} error={!!errors[name]}>
            <Select name={name} value={formState[name]} onChange={ingresarValoresMemoria} displayEmpty>
                <MenuItem value="" disabled><em className={classes.placeholder}>{placeholder}</em></MenuItem>
                {items.map(item => <MenuItem key={item.value} value={item.value}>{item.label}</MenuItem>)}
            </Select>
            {errors[name] && <FormHelperText>{errors[name]}</FormHelperText>}
        </FormControl>
    );

    const renderTextFieldWithSearch = (name) => (
        <TextField name={name} value={formState[name]} onChange={ingresarValoresMemoria} fullWidth variant="outlined" className={classes.textField}
            InputProps={{ startAdornment: (<InputAdornment position="start"><IconButton size="small"><Search /></IconButton></InputAdornment>), }}
            error={!!errors[name]} helperText={errors[name]}
        />
    );

    return (
        <div className={classes.root}>
            <main className={classes.content}>
                <form onSubmit={handleSubmit}>
                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Cliente/Pre-Cliente">{renderTextFieldWithSearch('clienteId')}</FormField>
                            <FormField label="Contactos">{renderTextFieldWithSearch('listaContactos')}</FormField>
                            <FormField label="Asesores">{renderTextFieldWithSearch('listaAsesores')}</FormField>
                            <FormField label="Fecha de Sesión"><TextField name="fechaSesion" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.fechaSesion} onChange={ingresarValoresMemoria} InputLabelProps={{ shrink: true }} error={!!errors.fechaSesion} helperText={errors.fechaSesion} /></FormField>
                            <FormField label="Tiempo de Contacto (h:mm)" required={false}><TextField name="tiempoContacto" fullWidth variant="outlined" className={classes.textField} value={formState.tiempoContacto} onChange={ingresarValoresMemoria} placeholder="0:00" /></FormField>
                            <FormField label="Tipo de Contacto">{renderSelect('tipoContactoId', 'Seleccione un tipo', [{ value: 1, label: 'Presencial' }, { value: 2, label: 'En Línea' }, { value: 3, label: 'Telefónico' }])}</FormField>
                            <FormField label="Área de Asesoría">{renderSelect('areaAsesoriaId', 'Seleccione un área', [{ value: 1, label: 'Finanzas' }, { value: 2, label: 'Marketing' }])}</FormField>
                            <FormField label="Ayuda Adicional" required={false}>{renderSelect('ayudaAdicional', 'Seleccione una ayuda', [{ value: 'Especializada', label: 'Asesoría Especializada' }, { value: 'Estudio', label: 'Estudio de Mercado' }])}</FormField>
                            <FormField label="Asunto" required={false}><TextField name="asunto" fullWidth variant="outlined" className={classes.textField} value={formState.asunto} onChange={ingresarValoresMemoria} /></FormField>
                            <FormField label="Fuente de Financiamiento">{renderSelect('fuenteFinanciamientoId', 'Seleccione una fuente', [{ value: 1, label: 'Otra' }])}</FormField>
                            <FormField label="Centro" required={false}><TextField name="centro" disabled fullWidth variant="outlined" className={classes.textField} value={formState.centro} /></FormField>
                            <FormField label="Número de Asistencias" required={false}><TextField name="numeroParticipantes" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.numeroParticipantes} onChange={ingresarValoresMemoria} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Referido a" required={false}>{renderSelect('referidoA', 'Seleccione una institución', [])}</FormField>
                            <FormField label="Descripción del Referido" md={8} required={false}><TextField name="descripcionReferido" fullWidth variant="outlined" className={classes.textField} value={formState.descripcionReferido} onChange={ingresarValoresMemoria} /></FormField>
                            <FormField label="Descripción de Derivado" md={12} required={false}><TextField name="descripcionDerivado" multiline rows={3} fullWidth variant="outlined" className={classes.textField} value={formState.descripcionDerivado} onChange={ingresarValoresMemoria} /></FormField>
                            <FormField label="Descripción de Asesoría Especializada" md={12} required={false}><TextField name="descripcionAsesoriaEspecializada" multiline rows={3} fullWidth variant="outlined" className={classes.textField} value={formState.descripcionAsesoriaEspecializada} onChange={ingresarValoresMemoria} /></FormField>
                            <FormField label="Notas" md={12} required={false}><TextField name="notas" multiline rows={5} fullWidth variant="outlined" className={classes.textField} value={formState.notas} onChange={ingresarValoresMemoria} /></FormField>
                        </Grid>
                    </Paper>

                    <Grid container spacing={2} justify="flex-end" className={classes.buttonContainer}>
                        <Grid item><Button type="submit" variant="contained" className={classes.buttonGuardar}>Guardar</Button></Grid>
                        <Grid item><Button variant="contained" className={classes.buttonCancelar} onClick={handleCancel}>Cancelar</Button></Grid>
                    </Grid>
                </form>
            </main>
        </div>
    );
};

export default NuevaAsesoria;