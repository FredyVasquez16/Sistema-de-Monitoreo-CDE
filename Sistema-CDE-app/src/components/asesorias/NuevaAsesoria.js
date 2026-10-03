import React, { useState, useEffect, useCallback } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    Select, MenuItem, FormControl, InputAdornment, IconButton, FormHelperText
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { Search } from '@material-ui/icons';
import { validarAsesoria } from '../../validators/AsesoriaValidator';
import { useStateValue } from '../../Context/store';
import CircularProgress from '@material-ui/core/CircularProgress';
import Autocomplete from '@material-ui/lab/Autocomplete';
import debounce from 'lodash.debounce';
import {
    obtenerTiposContactos,
    obtenerAreasAsesoria,
    obtenerAyudasAdicionales,
    obtenerReferencias,
    obtenerFuentesFinanciamiento,
    buscarClientesEmpresasPorTermino,
    buscarContactosPorTermino,
    buscarAsesoresPorTermino,
    guardarAsesoria
} from '../../actions/AsesoriaAction';

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
    const [, dispatch] = useStateValue();

    const [tiposContacto, setTiposContacto] = useState([]);
    const [areasAsesoria, setAreasAsesoria] = useState([]);
    const [ayudasAdicionales, setAyudasAdicionales] = useState([]);
    const [referencias, setReferencias] = useState([]);
    const [fuentesFinanciamiento, setFuentesFinanciamiento] = useState([]);

    const [opcionesCliente, setOpcionesCliente] = useState([]);
    const [loadingCliente, setLoadingCliente] = useState(false);
    const [opcionesContactoLista, setOpcionesContactoLista] = useState([]);
    const [loadingContactoLista, setLoadingContactoLista] = useState(false);
    const [opcionesAsesor, setOpcionesAsesor] = useState([]);
    const [loadingAsesor, setLoadingAsesor] = useState(false);

    const [selectedClientes, setSelectedClientes] = useState([]);
    const [selectedContactos, setSelectedContactos] = useState([]);
    const [selectedAsesores, setSelectedAsesores] = useState([]);

    const [formState, setFormState] = useState({
        clienteId: '',
        clienteNombre: '',
        fechaSesion: '',
        tiempoContacto: '',
        tipoContactoId: '',
        areaAsesoriaId: '',
        ayudaAdicional: '',
        asunto: '',
        fuenteFinanciamientoId: '',
        centro: 'CDE MIPYME ROC',
        numeroParticipantes: '',
        notas: '',
        referidoA: '',
        descripcionReferido: '',
        descripcionDerivado: '',
        descripcionAsesoriaEspecializada: '',
        listaAsesores: '',
        listaContactos: ''
    });

    const [errors, setErrors] = useState({});
    const [saving, setSaving] = useState(false);

    const debouncedBusqueda = useCallback(
        debounce(async (inputValue, buscarAction, setOpciones, setLoadingState) => {
            if (inputValue.length < 2) {
                setOpciones([]);
                setLoadingState(false);
                return;
            }
            const resultados = await buscarAction(inputValue);
            const opcionesMapeadas = resultados.map(item => ({
                ...item,
                label: item.nombreCompleto || item.nombre || item.razonSocial || `${item.nombre} ${item.apellido}`
            }));
            setOpciones(opcionesMapeadas);
            setLoadingState(false);
        }, 500),
        []
    );

    const handleBusqueda = (inputValue, buscarAction, setOpciones, setLoadingState) => {
        setLoadingState(true);
        debouncedBusqueda(inputValue, buscarAction, setOpciones, setLoadingState);
    };

    const cargarCatalogos = async () => {
        try {
            const [
                tiposContactoRes,
                areasAsesoriaRes,
                ayudasAdicionalesRes,
                referenciasRes,
                fuentesFinanciamientoRes
            ] = await Promise.all([
                obtenerTiposContactos(),
                obtenerAreasAsesoria(),
                obtenerAyudasAdicionales(),
                obtenerReferencias(),
                obtenerFuentesFinanciamiento()
            ]);

            setTiposContacto((tiposContactoRes || []).map(t => ({ value: t.id, label: t.descripcion })));
            setAreasAsesoria((areasAsesoriaRes || []).map(a => ({ value: a.id, label: a.descripcion })));
            setAyudasAdicionales((ayudasAdicionalesRes || []).map(a => ({ value: a.id, label: a.descripcion })));
            setReferencias((referenciasRes || []).map(r => ({ value: r.id, label: r.descripcion })));
            setFuentesFinanciamiento((fuentesFinanciamientoRes || []).map(f => ({ value: f.id, label: f.descripcion })));
        } catch (error) {
            console.error('Error cargando catálogos:', error);
            dispatch({
                type: 'OPEN_SNACKBAR',
                payload: { open: true, mensaje: 'Error al cargar catálogos', severity: 'error' }
            });
        }
    };

    useEffect(() => {
        cargarCatalogos();
    }, [dispatch]);

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setFormState((anterior) => ({
            ...anterior,
            [name]: value
        }));
    }

    const handleCancel = () => { history.goBack(); };

    const handleSubmit = async (e) => {
        e.preventDefault();

        const formDataToValidate = {
            ...formState,
            listaAsesores: selectedAsesores.length > 0 ? 'selected' : '',
            listaContactos: selectedContactos.length > 0 ? 'selected' : '',
            clienteId: selectedClientes.length > 0 ? selectedClientes[0].id : ''
        };

        const validationErrors = validarAsesoria(formDataToValidate);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            setSaving(true);
            try {
                const objetoAsesoria = {
                    ...formState,
                    clienteId: selectedClientes.length > 0 ? selectedClientes[0].id : 0,
                    listaClientes: selectedClientes.map(c => c.id),
                    listaContactos: selectedContactos.map(c => c.id),
                    listaAsesores: selectedAsesores.map(a => a.id)
                };
                await guardarAsesoria(objetoAsesoria);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Asesoría guardada con éxito.', severity: 'success' }
                });
                history.push('/asesorias');
            } catch (error) {
                let mensajeError = 'Error al guardar la asesoría';
                if (error.response && error.response.data) {
                    const errorData = error.response.data;
                    if (typeof errorData === 'string') {
                        mensajeError = errorData;
                    } else if (errorData.mensaje) {
                        mensajeError = errorData.mensaje;
                    } else if (errorData.errores && Array.isArray(errorData.errores)) {
                        mensajeError = errorData.errores.join(' ');
                    }
                } else if (error.message) {
                    mensajeError = error.message;
                }
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: mensajeError, severity: 'error' }
                });
            } finally {
                setSaving(false);
            }
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

    const renderAutocomplete = (label, opciones, loading, onInputChange, onChange, selectedValues, error, helperText, multiple = false) => (
        <Autocomplete
            id={`${label.toLowerCase().replace(/\s/g, '-')}-autocomplete`}
            options={opciones}
            getOptionLabel={(option) => option.label || ""}
            loading={loading}
            multiple={multiple}
            value={selectedValues}
            noOptionsText="No se encontraron resultados"
            loadingText="Buscando..."
            onInputChange={onInputChange}
            onChange={onChange}
            renderInput={(params) => (
                <TextField {...params} placeholder={`Buscar ${label.toLowerCase()}...`} variant="outlined" className={classes.textField}
                    error={!!error}
                    helperText={helperText}
                    InputProps={{
                        ...params.InputProps,
                        endAdornment: (
                            <React.Fragment>
                                {loading ? <CircularProgress color="inherit" size={20} /> : null}
                                {React.cloneElement(params.InputProps.endAdornment, { style: { display: 'none' } })}
                            </React.Fragment>
                        ),
                        startAdornment: (<InputAdornment position="start"><Search /></InputAdornment>),
                    }}
                />
            )}
        />
    );

    return (
        <div className={classes.root}>
            <main className={classes.content}>
                <form onSubmit={handleSubmit}>
                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Cliente/Pre-Cliente">
                                {renderAutocomplete(
                                    'Cliente',
                                    opcionesCliente,
                                    loadingCliente,
                                    (event, newInputValue) => handleBusqueda(newInputValue, buscarClientesEmpresasPorTermino, setOpcionesCliente, setLoadingCliente),
                                    (event, newValue) => {
                                        setSelectedClientes(newValue || []);
                                        setFormState(prev => ({ ...prev, clienteId: newValue && newValue.length > 0 ? newValue.map(c => c.id) : [] }));
                                    },
                                    selectedClientes,
                                    errors.clienteId,
                                    errors.clienteId,
                                    true
                                )}
                            </FormField>
                            <FormField label="Contactos">
                                {renderAutocomplete(
                                    'Contactos',
                                    opcionesContactoLista,
                                    loadingContactoLista,
                                    (event, newInputValue) => handleBusqueda(newInputValue, buscarContactosPorTermino, setOpcionesContactoLista, setLoadingContactoLista),
                                    (event, newValue) => {
                                        setSelectedContactos(newValue || []);
                                        setFormState(prev => ({ ...prev, listaContactos: newValue?.length > 0 ? 'selected' : '' }));
                                    },
                                    selectedContactos,
                                    errors.listaContactos,
                                    errors.listaContactos,
                                    true
                                )}
                            </FormField>
                            <FormField label="Asesores">
                                {renderAutocomplete(
                                    'Asesores',
                                    opcionesAsesor,
                                    loadingAsesor,
                                    (event, newInputValue) => handleBusqueda(newInputValue, buscarAsesoresPorTermino, setOpcionesAsesor, setLoadingAsesor),
                                    (event, newValue) => {
                                        setSelectedAsesores(newValue || []);
                                        setFormState(prev => ({ ...prev, listaAsesores: newValue?.length > 0 ? 'selected' : '' }));
                                    },
                                    selectedAsesores,
                                    errors.listaAsesores,
                                    errors.listaAsesores,
                                    true
                                )}
                            </FormField>
                            <FormField label="Fecha de Sesión">
                                <TextField name="fechaSesion" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.fechaSesion} onChange={ingresarValoresMemoria} InputLabelProps={{ shrink: true }} error={!!errors.fechaSesion} helperText={errors.fechaSesion} />
                            </FormField>
                            <FormField label="Tiempo de Contacto (h:mm)" required={false}>
                                <TextField name="tiempoContacto" fullWidth variant="outlined" className={classes.textField} value={formState.tiempoContacto} onChange={ingresarValoresMemoria} placeholder="0:00" />
                            </FormField>
                            <FormField label="Tipo de Contacto">
                                {renderSelect('tipoContactoId', 'Seleccione un tipo', tiposContacto)}
                            </FormField>
                            <FormField label="Área de Asesoría">
                                {renderSelect('areaAsesoriaId', 'Seleccione un área', areasAsesoria)}
                            </FormField>
                            <FormField label="Ayuda Adicional" required={false}>
                                {renderSelect('ayudaAdicional', 'Seleccione una ayuda', ayudasAdicionales)}
                            </FormField>
                            <FormField label="Asunto" required={false}>
                                <TextField name="asunto" fullWidth variant="outlined" className={classes.textField} value={formState.asunto} onChange={ingresarValoresMemoria} />
                            </FormField>
                            <FormField label="Fuente de Financiamiento">
                                {renderSelect('fuenteFinanciamientoId', 'Seleccione una fuente', fuentesFinanciamiento)}
                            </FormField>
                            <FormField label="Centro" required={false}>
                                <TextField name="centro" disabled fullWidth variant="outlined" className={classes.textField} value={formState.centro} />
                            </FormField>
                            <FormField label="Número de Asistencias" required={false}>
                                <TextField name="numeroParticipantes" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.numeroParticipantes} onChange={ingresarValoresMemoria} />
                            </FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Referido a" required={false}>
                                {renderSelect('referidoA', 'Seleccione una institución', referencias)}
                            </FormField>
                            <FormField label="Descripción del Referido" md={8} required={false}>
                                <TextField name="descripcionReferido" fullWidth variant="outlined" className={classes.textField} value={formState.descripcionReferido} onChange={ingresarValoresMemoria} />
                            </FormField>
                            <FormField label="Descripción de Derivado" md={12} required={false}>
                                <TextField name="descripcionDerivado" multiline rows={3} fullWidth variant="outlined" className={classes.textField} value={formState.descripcionDerivado} onChange={ingresarValoresMemoria} />
                            </FormField>
                            <FormField label="Descripción de Asesoría Especializada" md={12} required={false}>
                                <TextField name="descripcionAsesoriaEspecializada" multiline rows={3} fullWidth variant="outlined" className={classes.textField} value={formState.descripcionAsesoriaEspecializada} onChange={ingresarValoresMemoria} />
                            </FormField>
                            <FormField label="Notas" md={12} required={false}>
                                <TextField name="notas" multiline rows={5} fullWidth variant="outlined" className={classes.textField} value={formState.notas} onChange={ingresarValoresMemoria} />
                            </FormField>
                        </Grid>
                    </Paper>

                    <Grid container spacing={2} justify="flex-end" className={classes.buttonContainer}>
                        <Grid item><Button type="submit" variant="contained" className={classes.buttonGuardar} disabled={saving}>
                            {saving ? <CircularProgress size={24} /> : 'Guardar'}
                        </Button></Grid>
                        <Grid item><Button variant="contained" className={classes.buttonCancelar} onClick={handleCancel}>Cancelar</Button></Grid>
                    </Grid>
                </form>
            </main>
        </div>
    );
};

export default NuevaAsesoria;
