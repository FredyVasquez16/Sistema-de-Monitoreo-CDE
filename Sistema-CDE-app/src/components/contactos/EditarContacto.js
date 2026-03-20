import React, { useState, useEffect } from 'react';
import {
    Paper, Grid, TextField, Select, MenuItem, FormControl, Switch, FormControlLabel, FormHelperText, Button, CircularProgress, Typography
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory, useParams } from 'react-router-dom';
import { validarContacto } from '../../validators/ContactoValidator';
import { useStateValue } from '../../Context/store';
import { actualizarContacto, obtenerContactoPorId, obtenerEstadosCiviles, obtenerNivelesEstudio, obtenerCategoriasLaborales } from '../../actions/ContactoAction';

const useStyles = makeStyles((theme) => ({
    root: {
        backgroundColor: '#033565',
        minHeight: '100vh',
        paddingBottom: theme.spacing(4),
    },
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
        position: 'relative',
    },
    buttonCancelar: {
        backgroundColor: '#F44336',
        color: 'white',
        fontWeight: 'bold',
        '&:hover': { backgroundColor: '#d32f2f' },
    },
    buttonProgress: {
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

const EditarContacto = () => {
    const classes = useStyles();
    const history = useHistory();
    const { id } = useParams();
    const [, dispatch] = useStateValue();
    const [estadosCiviles, setEstadosCiviles] = useState([]);
    const [nivelesEstudio, setNivelesEstudio] = useState([]);
    const [categoriasLaborales, setCategoriasLaborales] = useState([]);
    const [loading, setLoading] = useState(false);

    const [formState, setFormState] = useState({
        nombre: '', apellidos: '', fechaNacimiento: '', dni: '', nacionalidad: '', genero: '',
        numeroTelefono: '', correoElectronico: '', rtn: '', direccion: '', municipio: '',
        departamento: '', cargo: '', estadoCivil: '', nivelEstudio: '', categoriaLaboral: '',
        etnia: '', localidadEtnia: '', tieneNegocio: false, contactoDiscapacidad: '',
        rolFamiliar: '', numIntegrantes: '', numHijos: '', numHijas: '', notas: ''
    });

    const [errors, setErrors] = useState({});

    useEffect(() => {
        const cargarContacto = async () => {
            try {
                const data = await obtenerContactoPorId(id);
                setFormState({
                    nombre: data.nombre || '',
                    apellidos: data.apellido || '',
                    fechaNacimiento: data.fechaNacimiento ? data.fechaNacimiento.substring(0, 10) : '',
                    dni: data.dni || '',
                    nacionalidad: data.nacionalidad || '',
                    genero: data.genero || '',
                    numeroTelefono: data.telefono ? String(data.telefono) : '',
                    correoElectronico: data.correo || '',
                    rtn: data.rtn || '',
                    direccion: data.direccion || '',
                    municipio: data.municipio || '',
                    departamento: data.departamento || '',
                    cargo: data.cargo || '',
                    estadoCivil: data.estadoCivilId ? String(data.estadoCivilId) : '',
                    nivelEstudio: data.nivelEstudioId ? String(data.nivelEstudioId) : '',
                    categoriaLaboral: data.categoriaLaboralId ? String(data.categoriaLaboralId) : '',
                    etnia: data.nombreEtnia || '',
                    localidadEtnia: data.localidadEtnica || '',
                    tieneNegocio: !!data.poseeNegocio,
                    contactoDiscapacidad: data.contactoDiscapacidad ? String(data.contactoDiscapacidad) : '',
                    rolFamiliar: data.rolContactoFamiliar || '',
                    numIntegrantes: data.integrantesTotalesFamilia ? String(data.integrantesTotalesFamilia) : '',
                    numHijos: data.numeroHijos ? String(data.numeroHijos) : '',
                    numHijas: data.numeroHijas ? String(data.numeroHijas) : '',
                    notas: data.notas || ''
                });
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar el contacto para editar',
                        severity: 'error'
                    }
                });
            }
        };
        cargarContacto();
    }, [id, dispatch]);

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

    const handleChange = (event) => {
        const { name, value, checked, type } = event.target;
        setFormState(prevState => ({ ...prevState, [name]: type === 'checkbox' ? checked : value }));
    };

    const handleCancel = () => { history.goBack(); };

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

                // Llama a la acción para actualizar
                const response = await actualizarContacto(id, payload);
                //console.log("Respuesta completa del servidor:", response);

                if (response.status === 200 || response.status === 204) {
                    dispatch({
                        type: 'OPEN_SNACKBAR',
                        payload: {
                            open: true,
                            mensaje: 'Contacto actualizado exitosamente.',
                            severity: 'success',
                        },
                    });
                    history.push('/contactos');
                }
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: "No se pudo actualizar el contacto.", severity: 'error' },
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
                                disabled={loading}
                            >
                                Guardar Cambios
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

export default EditarContacto;