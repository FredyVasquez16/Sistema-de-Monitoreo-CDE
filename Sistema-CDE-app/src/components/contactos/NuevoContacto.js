// src/components/contactos/NuevoContacto.js

import React, { useState, useEffect, use } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    Select, MenuItem, FormControl, Switch, FormControlLabel, FormHelperText, CircularProgress
} from '@material-ui/core'; // Agregamos CircularProgress para feedback visual
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { validarContacto } from '../../validators/ContactoValidator';
import { guardarContacto, guardarNuevoContacto } from '../../actions/ContactoAction'; // 1. IMPORTAMOS la nueva acción
import { useStateValue } from '../../Context/store'; // 2. IMPORTAMOS el hook del contexto global
import { obtenerEstadosCiviles, obtenerNivelesEstudio, obtenerCategoriasLaborales } from '../../actions/ContactoAction';

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
        position: 'relative', // Necesario para el spinner
    },
    buttonCancelar: {
        backgroundColor: '#F44336',
        color: 'white',
        fontWeight: 'bold',
        '&:hover': { backgroundColor: '#d32f2f' },
    },
    buttonProgress: { // Estilo para el spinner
        color: '#D5A408',
        position: 'absolute',
        top: '50%',
        left: '50%',
        marginTop: -12,
        marginLeft: -12,
    },
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


const NuevoContacto = () => {
    const classes = useStyles();
    const history = useHistory();
    const [, dispatch] = useStateValue(); // 3. OBTENEMOS el dispatch para las notificaciones
    const [estadosCiviles, setEstadosCiviles] = useState([]);
    const [nivelesEstudio, setNivelesEstudio] = useState([]);
    const [categoriasLaborales, setCategoriasLaborales] = useState([]);

    const [loading, setLoading] = useState(false); // Estado para el feedback de carga

    const [formState, setFormState] = useState({
        nombre: '', apellidos: '', fechaNacimiento: '', dni: '', nacionalidad: '', genero: '',
        numeroTelefono: '', correoElectronico: '', rtn: '', direccion: '', municipio: '',
        departamento: '', cargo: '', estadoCivil: '', nivelEstudio: '', categoriaLaboral: '',
        etnia: '', localidadEtnia: '', tieneNegocio: false, contactoDiscapacidad: '',
        rolFamiliar: '', numIntegrantes: '', numHijos: '', numHijas: '', notas: ''
    });

    const [errors, setErrors] = useState({});

    const handleChange = (event) => {
        const { name, value, checked, type } = event.target;
        setFormState(prevState => ({ ...prevState, [name]: type === 'checkbox' ? checked : value }));
    };

    const handleCancel = () => { history.goBack(); };

    useEffect(() => {
        const cargarEstadosCiviles = async () => {
            try {
                const data = await obtenerEstadosCiviles();
                setEstadosCiviles(data.map(e => ({ value: e.id, label: e.descripcion })));
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar estados civiles',
                        severity: 'error'
                    }
                });
            }
        };
        cargarEstadosCiviles();
    }, [dispatch]);

    useEffect(() => {
        const cargarNivelesEstudio = async () => {
            try {
                const data = await obtenerNivelesEstudio();
                setNivelesEstudio(data.map(n => ({ value: n.id, label: n.descripcion })));
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar niveles de estudio',
                        severity: 'error'
                    }
                });
            }
        };
        cargarNivelesEstudio();
    }, [dispatch]);

    useEffect(() => {
        const cargarCategoriasLaborales = async () => {
            try {
                const data = await obtenerCategoriasLaborales();
                setCategoriasLaborales(data.map(c => ({ value: c.id, label: c.descripcion })));
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar categorías laborales',
                        severity: 'error'
                    }
                });
            }
        };
        cargarCategoriasLaborales();
    }, [dispatch]);

    const handleSubmit = async (e) => {
        e.preventDefault();
        const validationErrors = validarContacto(formState);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            setLoading(true);
            try {

                const payload = {
                    nombre: formState.nombre,
                    apellido: formState.apellidos,
                    fechaNacimiento: formState.fechaNacimiento,
                    dni: formState.dni,
                    nacionalidad: formState.nacionalidad,
                    genero: formState.genero,
                    telefono: formState.numeroTelefono ? parseInt(formState.numeroTelefono, 10) : 0,
                    correo: formState.correoElectronico,
                    rtn: formState.rtn,
                    direccion: formState.direccion,
                    municipio: formState.municipio,
                    departamento: formState.departamento,
                    cargo: formState.cargo,
                    estadoCivilId: formState.estadoCivil ? parseInt(formState.estadoCivil, 10) : null,
                    nivelEstudioId: formState.nivelEstudio ? parseInt(formState.nivelEstudio, 10) : 0,
                    categoriaLaboralId: formState.categoriaLaboral ? parseInt(formState.categoriaLaboral, 10) : null,
                    poseeNegocio: formState.tieneNegocio,
                    nombreEtnia: formState.etnia || null,
                    localidadEtnica: formState.localidadEtnia || null,
                    contactoDiscapacidad: formState.contactoDiscapacidad ? parseInt(formState.contactoDiscapacidad, 10) : null,
                    integrantesTotalesFamilia: formState.numIntegrantes ? parseInt(formState.numIntegrantes, 10) : null,
                    numeroHijos: formState.numHijos ? parseInt(formState.numHijos, 10) : null,
                    numeroHijas: formState.numHijas ? parseInt(formState.numHijas, 10) : null,
                    rolContactoFamiliar: formState.rolFamiliar || null,
                    centro: "CDE MIPYME ROC",
                    notas: formState.notas || null,
                };

                const response = await guardarContacto(payload);

                if (response.status === 201 || response.status === 200) {
                    dispatch({
                        type: 'OPEN_SNACKBAR',
                        payload: {
                            open: true,
                            mensaje: 'Contacto registrado exitosamente.',
                            severity: 'success',
                        },
                    });
                    history.push('/contactos');
                }
            } catch (error) {
                console.error("Error al guardar contacto:", error.response || error);

                const errorData = error.response?.data;
                let mensajeError = "No se pudo guardar el contacto.";

                // Manejo de errores de FluentValidation
                if (errorData && errorData.errors) {
                    const primerosErrores = Object.values(errorData.errors).slice(0, 2).join(' ');
                    mensajeError = primerosErrores;
                } else if (errorData && errorData.mensaje) {
                    mensajeError = errorData.mensaje;
                }

                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: mensajeError, severity: 'error' },
                });
            } finally {
                setLoading(false);
            }
        } else {
            dispatch({
                type: 'OPEN_SNACKBAR',
                payload: { open: true, mensaje: "Por favor, corrija los errores en el formulario.", severity: 'warning' },
            });
        }
    };

    const renderSelect = (name, placeholder, items) => (
        <FormControl variant="outlined" fullWidth className={classes.textField} error={!!errors[name]}>
            <Select name={name} value={formState[name]} onChange={handleChange} displayEmpty>
                <MenuItem value="" disabled><em className={classes.placeholder}>{placeholder}</em></MenuItem>
                {items.map(item => <MenuItem key={item.value} value={item.value}>{item.label}</MenuItem>)}
            </Select>
            {errors[name] && <FormHelperText>{errors[name]}</FormHelperText>}
        </FormControl>
    );

    return (
        <div className={classes.root}>

            <main className={classes.content}>
                <form onSubmit={handleSubmit}>
                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Nombre"><TextField name="nombre" value={formState.nombre} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.nombre} helperText={errors.nombre} /></FormField>
                            <FormField label="Apellidos"><TextField name="apellidos" value={formState.apellidos} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.apellidos} helperText={errors.apellidos} /></FormField>
                            <FormField label="Fecha de Nacimiento"><TextField name="fechaNacimiento" type="date" value={formState.fechaNacimiento} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} InputLabelProps={{ shrink: true }} error={!!errors.fechaNacimiento} helperText={errors.fechaNacimiento} /></FormField>
                            <FormField label="DNI"><TextField name="dni" value={formState.dni} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.dni} helperText={errors.dni} /></FormField>
                            <FormField label="Nacionalidad"><TextField name="nacionalidad" value={formState.nacionalidad} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.nacionalidad} helperText={errors.nacionalidad} /></FormField>
                            <FormField label="Genero">{renderSelect('genero', 'Seleccione un género', [{ value: "Masculino", label: "Masculino" }, { value: "Femenino", label: "Femenino" }])}</FormField>
                            <FormField label="Numero de Telefono"><TextField name="numeroTelefono" value={formState.numeroTelefono} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.numeroTelefono} helperText={errors.numeroTelefono} /></FormField>
                            <FormField label="Correo Electronico" md={4}><TextField name="correoElectronico" value={formState.correoElectronico} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.correoElectronico} helperText={errors.correoElectronico} /></FormField>
                            <FormField label="RTN"><TextField name="rtn" value={formState.rtn} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.rtn} helperText={errors.rtn} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Direccion" md={12}><TextField name="direccion" value={formState.direccion} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.direccion} helperText={errors.direccion} /></FormField>
                            <FormField label="Municipio" md={6}><TextField name="municipio" value={formState.municipio} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.municipio} helperText={errors.municipio} /></FormField>
                            <FormField label="Departamento" md={6}>
                                {renderSelect('departamento', 'Seleccione un departamento', [
                                    { value: 'Copan', label: 'Copan' },
                                    { value: 'Lempira', label: 'Lempira' },
                                    { value: 'Ocotepeque', label: 'Ocotepeque' }
                                ])}
                            </FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Cargo"><TextField name="cargo" value={formState.cargo} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} error={!!errors.cargo} helperText={errors.cargo} /></FormField>
                            <FormField label="Estado Civil" required={false}>{renderSelect('estadoCivil', 'Seleccione un estado', estadosCiviles)}</FormField>
                            <FormField label="Nivel de Estudio">{renderSelect('nivelEstudio', 'Seleccione un nivel', nivelesEstudio)}</FormField>
                            <FormField label="Categoria Laboral" required={false}>{renderSelect('categoriaLaboral', 'Seleccione una categoría', categoriasLaborales)}</FormField>
                            <FormField label="Etnia del contacto, si procede" required={false}><TextField name="etnia" value={formState.etnia} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <FormField label="Localidad de la Etnia" required={false}><TextField name="localidadEtnia" value={formState.localidadEtnia} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <Grid item xs={12}><FormControlLabel control={<Switch checked={formState.tieneNegocio} onChange={handleChange} name="tieneNegocio" />} label="¿Posee usted un negocio en funcionamiento o una idea de negocio?" /></Grid>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Contacto en caso de discapacidad" required={false}><TextField name="contactoDiscapacidad" value={formState.contactoDiscapacidad} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <FormField label="Rol del contacto primario en la familia" required={false}><TextField name="rolFamiliar" value={formState.rolFamiliar} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <FormField label="Numero de Integrantes del nucleo familiar" required={false}><TextField name="numIntegrantes" type="number" value={formState.numIntegrantes} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <FormField label="Numero de Hijos" required={false}><TextField name="numHijos" type="number" value={formState.numHijos} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                            <FormField label="Numero de Hijas" required={false}><TextField name="numHijas" type="number" value={formState.numHijas} onChange={handleChange} fullWidth variant="outlined" className={classes.textField} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Notas" md={12} required={false}><TextField name="notas" value={formState.notas} onChange={handleChange} fullWidth variant="outlined" multiline rows={4} className={classes.textField} /></FormField>
                        </Grid>
                    </Paper>

                    <Grid container spacing={2} justify="flex-end" className={classes.buttonContainer}>
                        <Grid item>
                            <Button
                                type="submit"
                                variant="contained"
                                className={classes.buttonGuardar}
                                disabled={loading} // Deshabilita el botón mientras carga
                            >
                                Guardar
                                {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
                            </Button>
                        </Grid>
                        <Grid item>
                            <Button variant="contained" className={classes.buttonCancelar} onClick={handleCancel}>Cancelar</Button>
                        </Grid>
                    </Grid>
                </form>
            </main>
        </div>
    );
};

export default NuevoContacto;