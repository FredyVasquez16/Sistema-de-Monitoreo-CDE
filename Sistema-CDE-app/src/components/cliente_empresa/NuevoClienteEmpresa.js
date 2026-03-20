// src/components/cliente_empresa/NuevoClienteEmpresa.js

import React, { useEffect, useState, useCallback } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    Select, MenuItem, FormControl, Switch, FormControlLabel, InputAdornment, IconButton, FormHelperText
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { Search } from '@material-ui/icons';
import { validarClienteEmpresa } from '../../validators/ClienteEmpresaValidator'; // Importa el validador
import { guardarClienteEmpresa } from '../../actions/ClienteEmpresaAction';
import { useStateValue } from '../../Context/store';
import CircularProgress from '@material-ui/core/CircularProgress';
import { obtenerTiposClientesNivel, obtenerServiciosSolicitados, obtenerTiposClientesEstado, buscarAsesoresPorTermino, obtenerTiposOrganizacion, obtenerTiposEmpresa, obtenerTamanosEmpresa } from '../../actions/ClienteEmpresaAction';
import Autocomplete from '@material-ui/lab/Autocomplete';
import { buscarContactosPorTermino } from '../../actions/ContactoAction';
import debounce from 'lodash.debounce';
import { obtenerTipoContabilidades } from '../../actions/ClienteEmpresaAction';
import { obtenerNivelesFormalizacion } from '../../actions/ClienteEmpresaAction';
import { obtenerTiposComercioInternacional } from '../../actions/ClienteEmpresaAction';
import { obtenerFuentesFinanciamiento } from '../../actions/ClienteEmpresaAction';

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

const NuevoClienteEmpresa = () => {
    const classes = useStyles();
    const history = useHistory();
    const [loading, setLoading] = useState(false);
    const [, dispatch] = useStateValue();
    const [tiposClienteNivel, setTiposClienteNivel] = useState([]);
    const [serviciosSolicitados, setServiciosSolicitados] = useState([]);
    const [tiposClienteEstado, setTiposClienteEstado] = useState([]);
    const [tiposOrganizacion, setTiposOrganizacion] = useState([]);
    const [tiposEmpresa, setTiposEmpresa] = useState([]);
    const [tamanosEmpresa, setTamanosEmpresa] = useState([]);
    const [tiposContabilidad, setTiposContabilidad] = useState([]);
    const [nivelesFormalizacion, setNivelesFormalizacion] = useState([]);
    const [tiposComercioInternacional, setTiposComercioInternacional] = useState([]);
    const [fuentesFinanciamiento, setFuentesFinanciamiento] = useState([]);

    const [opcionesContacto, setOpcionesContacto] = useState([]);
    const [loadingContacto, setLoadingContacto] = useState(false);
    const [opcionesPropietario, setOpcionesPropietario] = useState([]);
    const [loadingPropietario, setLoadingPropietario] = useState(false);
    const [opcionesAsesor, setOpcionesAsesor] = useState([]);
    const [loadingAsesor, setLoadingAsesor] = useState(false);

    /*const [formState, setFormState] = useState({
        nombreCliente: '', nivelCliente: '', contactoPrimario: '', estadoCliente: '', asesorPrincipal: '', nombrePropietario: '', generoPropietario: '',
        razonSocial: '', serviciosSolicitados: '', direccionFisica: '', Municipio: '', departamento: '', numeroTelefono: '', paginaWeb: '',
        correoElectronico: '', fechaInicio: '', tipoOrganizacion: '', tipoEmpresa: '', tamanoEmpresa: '', tipoContabilidad: '', nivelFormalizacion: '',
        tipoCasos: '', participaAsociacion: false, beneficiadoCDE: false, empleadosCompleto: '', empleadosMedio: '', trabajadoresInformales: '',
        negocioEnLinea: false, negocioEnCasa: false, tipoComercio: '', paisesExporta: '', potencialContratoGob: false, localizadoZonaIndigena: false,
        fuenteFinanciamiento: '', subFuente: '', ingresosAnuales: '', fechaIngresos: '', ingresosExportaciones: '', gananciasPerdidas: '',
        fechaGanancias: '', adjudicadoFondo: false, instruccionFondo: '', estatusInicial: '', estatusActual: '', fechaEstablecimiento: '',
        impideCrecimiento: '', areasNecesita: '', dondeDesarrolla: '', porqueComenzarPotenciar1: '', porqueComenzarPotenciar2: '', descripcionProducto: '',
        solicitadoCredito: false, comoSolicito: '', porqueNoSolicito: '', utilizaMediosPago: false, queMediosPago: '', notas: '',
        contactoPrimarioId: null,
        nombrePropietarioId: null,
        asesorPrincipalId: null,
    });*/

    const [formState, setFormState] = useState({
        nombreCliente: '', nivelCliente: '', estadoCliente: '', generoPropietario: '',
        razonSocial: '', serviciosSolicitados: '', direccionFisica: '', municipio: '', departamento: '', numeroTelefono: '', paginaWeb: '',
        correoElectronico: '', fechaInicio: '', tipoOrganizacion: '', tipoEmpresa: '', tamanoEmpresa: '', tipoContabilidad: '', nivelFormalizacion: '',
        tipoCasos: '', participaAsociacion: false, beneficiadoCDE: false, empleadosCompleto: '', empleadosMedio: '', trabajadoresInformales: '',
        negocioEnLinea: false, negocioEnCasa: false, tipoComercio: '', paisesExporta: '', potencialContratoGob: false, localizadoZonaIndigena: false,
        fuenteFinanciamiento: '', subFuente: '', ingresosAnuales: '', fechaIngresos: '', ingresosExportaciones: '', gananciasPerdidas: '',
        fechaGanancias: '', adjudicadoFondo: false, instruccionFondo: '', estatusInicial: '', estatusActual: '', fechaEstablecimiento: '',
        impideCrecimiento: '', areasNecesita: '', dondeDesarrolla: '', porqueComenzarPotenciar1: '', porqueComenzarPotenciar2: '', descripcionProducto: '',
        solicitadoCredito: false, comoSolicito: '', porqueNoSolicito: '', utilizaMediosPago: false, queMediosPago: '', notas: '',
        contactoPrimarioId: null,
        nombrePropietarioId: null,
        asesorPrincipalId: null,
    });

    const [errors, setErrors] = useState({});

    const handleChange = (event) => {
        const { name, value, checked, type } = event.target;
        setFormState(prevState => ({ ...prevState, [name]: type === 'checkbox' ? checked : value }));
    };

    const handleCancel = () => { history.goBack(); };

    const handleSubmit = async (e) => {
        e.preventDefault();
        const validationErrors = validarClienteEmpresa(formState);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            setLoading(true);

            const safeParseInt = (value) => (value === '' || value === null || isNaN(parseInt(value))) ? null : parseInt(value, 10);
            const safeParseFloat = (value) => (value === '' || value === null || isNaN(parseFloat(value))) ? null : parseFloat(value, 10);

            const safeFormatDate = (dateString) => (dateString === '' ? null : dateString);

            const payload = {
                nombre: formState.nombreCliente,
                tipoClienteNivelId: safeParseInt(formState.nivelCliente),
                contactoPrimarioId: formState.contactoPrimarioId,
                tipoClienteEstadoId: safeParseInt(formState.estadoCliente),
                usuarioId: formState.asesorPrincipalId,
                servicioSolicitadoId: safeParseInt(formState.serviciosSolicitados),
                razonSocial: formState.razonSocial || null,
                telefono: safeParseInt(formState.numeroTelefono),
                correo: formState.correoElectronico,
                paginaWeb: formState.paginaWeb || null,
                fechaInicio: formState.fechaInicio,
                direccionFisica: formState.direccionFisica,
                municipio: formState.municipio,
                departamento: formState.departamento,
                tipoOrganizacionId: safeParseInt(formState.tipoOrganizacion),
                tipoEmpresaId: safeParseInt(formState.tipoEmpresa),
                tamanoEmpresaId: safeParseInt(formState.tamanoEmpresa),
                tipoContabilidadId: safeParseInt(formState.tipoContabilidad),
                nivelFormalizacionId: safeParseInt(formState.nivelFormalizacion),
                participaGremio: formState.participaAsociacion,
                beneficiadoCde: formState.beneficiadoCDE,
                tipoCasoEnProceso: formState.tipoCasos || null,
                empleadosTiempoCompleto: safeParseInt(formState.empleadosCompleto),
                empleadosMedioTiempo: safeParseInt(formState.empleadosMedio),
                trabajadoresInformales: safeParseInt(formState.trabajadoresInformales),
                negocioEnLinea: formState.negocioEnLinea,
                negocioEnCasa: formState.negocioEnCasa,
                comercioInternacionalId: safeParseInt(formState.tipoComercio),
                paisExporta: formState.paisesExporta || null,
                contratoGobierno: formState.potencialContratoGob,
                zonaIndigena: formState.localizadoZonaIndigena,
                fuenteFinanciamientoId: safeParseInt(formState.fuenteFinanciamiento),
                subFuenteFinanciamientoId: safeParseInt(formState.subFuente),
                ingresosBrutosAnuales: safeParseFloat(formState.ingresosAnuales),
                fechaIngresosBrutos: formState.fechaIngresos,
                ingresosExportaciones: safeParseFloat(formState.ingresosExportaciones),
                gananciasPerdidasBrutas: safeParseFloat(formState.gananciasPerdidas),
                fechaGananciasPerdidasBrutas: formState.fechaGanancias,
                descripcionProductoServicio: formState.descripcionProducto,
                areasADominar: formState.areasNecesita || null,
                instrucciones: formState.instruccionFondo || null,
                motivacion: formState.porqueComenzarPotenciar1 || null,
                lugarDesarrolloEmprendimiento: formState.dondeDesarrolla || null,
                obstaculos: formState.impideCrecimiento || null,
                fondoConcursable: formState.adjudicadoFondo,
                estatusInicial: formState.estatusInicial,
                estatusActual: formState.estatusActual,
                fechaEstablecimiento: formState.fechaEstablecimiento,
                nombrePropietarioId: formState.nombrePropietarioId,
                generoPropietario: formState.generoPropietario,
                haSolicitadoCredito: formState.solicitadoCredito,
                comoSolicitoCredito: formState.comoSolicito || null,
                porQueNoCredito: formState.porqueNoSolicito || null,
                usaPagoElectronico: formState.utilizaMediosPago,
                mediosPago: formState.queMediosPago || null,
                notas: formState.notas || null
            };

            try {
                const response = await guardarClienteEmpresa(payload);

                if (response.status === 201) {
                    dispatch({
                        type: 'OPEN_SNACKBAR',
                        payload: { open: true, mensaje: 'Cliente/Empresa guardado con éxito.', severity: 'success' },
                    });
                    history.push('/cliente_empresa');
                }
            } catch (error) {
                console.error("Error al guardar cliente/empresa:", error.response || error);

                // ===== LA SOLUCIÓN ESTÁ AQUÍ =====
                const errorData = error.response?.data;
                let mensajeError = "No se pudo guardar. Verifique los datos."; // Mensaje por defecto

                if (typeof errorData === 'string') {
                    // Si la respuesta es un string simple
                    mensajeError = errorData;
                } else if (errorData && errorData.errors) {
                    // Si es un objeto de FluentValidation
                    const errorMessages = Object.values(errorData.errors).flat();
                    mensajeError = errorMessages.join(' ');
                } else if (errorData && errorData.mensaje) {
                    // Si es un objeto con una propiedad 'mensaje'
                    mensajeError = errorData.mensaje;
                } else if (errorData && errorData.errores) {
                    // Si es un objeto con una propiedad 'errores' (tu caso actual)
                    mensajeError = errorData.errores;
                }

                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: mensajeError, severity: 'error' },
                });
            } finally {
                setLoading(false);
            }
        } else {
            console.log("Errores de validación encontrados:", validationErrors);
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

    const renderTextFieldWithSearch = (name) => (
        <TextField name={name} value={formState[name]} onChange={handleChange} fullWidth variant="outlined" className={classes.textField}
            InputProps={{ startAdornment: (<InputAdornment position="start"><IconButton size="small"><Search /></IconButton></InputAdornment>), }}
            error={!!errors[name]} helperText={errors[name]}
        />
    );

    /*const handleBusquedaContacto = async (event, newValue) => {
        setLoadingContacto(true);
        const resultados = await buscarContactosPorTermino(newValue);

        // El backend debe devolver objetos con { id, nombre, apellido }
        // Los mapeamos a { value: id, label: 'Nombre Apellido' }
        setOpcionesContacto(resultados.map(c => ({
            ...c, // Mantenemos el objeto completo
            label: `${c.nombre} ${c.apellido}`
        })));
        setLoadingContacto(false);
    };*/

    /*const debouncedBusqueda = useCallback(
        debounce(async (inputValue, setOpciones, setLoadingState) => {
            if (inputValue.length < 3) { // No buscar si hay menos de 3 caracteres
                setOpciones([]);
                setLoadingState(false);
                return;
            }
            const resultados = await buscarContactosPorTermino(inputValue);
            const opcionesMapeadas = resultados.map(c => ({
                ...c,
                label: `${c.nombre} ${c.apellido}`
            }));
            setOpciones(opcionesMapeadas);
            setLoadingState(false);
        }, 500), // 500ms de espera antes de lanzar la búsqueda
        []
    );*/

    const debouncedBusqueda = useCallback(
        debounce(async (inputValue, buscarAction, setOpciones, setLoadingState) => {
            if (inputValue.length < 2) { // Buscar con 2 o más caracteres
                setOpciones([]);
                setLoadingState(false);
                return;
            }
            const resultados = await buscarAction(inputValue);
            // El backend devuelve objetos con id y nombre/apellido o nombreCompleto
            const opcionesMapeadas = resultados.map(item => ({
                ...item,
                label: item.nombreCompleto || `${item.nombre} ${item.apellido}`
            }));
            setOpciones(opcionesMapeadas);
            setLoadingState(false);
        }, 500),
        []
    );

    /*const handleBusqueda = (inputValue, setOpciones, setLoadingState) => {
        setLoadingState(true);
        debouncedBusqueda(inputValue, setOpciones, setLoadingState);
    };*/

    const handleBusqueda = (inputValue, buscarAction, setOpciones, setLoadingState) => {
        setLoadingState(true);
        debouncedBusqueda(inputValue, buscarAction, setOpciones, setLoadingState);
    };


    useEffect(() => {
        const cargarTiposClienteNivel = async () => {
            try {
                const response = await obtenerTiposClientesNivel();
                // Ajusta según la estructura de tu backend
                setTiposClienteNivel(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de cliente nivel',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposClienteNivel();
    }, [dispatch]);

    useEffect(() => {
        const cargarServiciosSolicitados = async () => {
            try {
                const response = await obtenerServiciosSolicitados();
                setServiciosSolicitados(
                    (response.data.data || []).map(s => ({
                        value: s.id,
                        label: s.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los servicios solicitados',
                        severity: 'error'
                    }
                });
            }
        };
        cargarServiciosSolicitados();
    }, [dispatch]);

    useEffect(() => {
        const cargarTiposClienteEstado = async () => {
            try {
                const response = await obtenerTiposClientesEstado();
                setTiposClienteEstado(
                    (response.data.data || []).map(e => ({
                        value: e.id,
                        label: e.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de estado',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposClienteEstado();
    }, [dispatch]);

    useEffect(() => {
        const cargarTiposOrganizacion = async () => {
            try {
                const response = await obtenerTiposOrganizacion();
                setTiposOrganizacion(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de organización',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposOrganizacion();
    }, [dispatch]);

    useEffect(() => {
        const cargarTiposEmpresa = async () => {
            try {
                const response = await obtenerTiposEmpresa();
                setTiposEmpresa(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de empresa',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposEmpresa();
    }, [dispatch]);

    useEffect(() => {
        const cargarTamanosEmpresa = async () => {
            try {
                const response = await obtenerTamanosEmpresa();
                setTamanosEmpresa(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tamaños de empresa',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTamanosEmpresa();
    }, [dispatch]);

    useEffect(() => {
        const cargarTiposContabilidad = async () => {
            try {
                const response = await obtenerTipoContabilidades();
                setTiposContabilidad(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de contabilidad',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposContabilidad();
    }, [dispatch]);

    useEffect(() => {
        const cargarNivelesFormalizacion = async () => {
            try {
                const response = await obtenerNivelesFormalizacion();
                setNivelesFormalizacion(
                    (response.data.data || []).map(n => ({
                        value: n.id,
                        label: n.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los niveles de formalización',
                        severity: 'error'
                    }
                });
            }
        };
        cargarNivelesFormalizacion();
    }, [dispatch]);

    useEffect(() => {
        const cargarTiposComercioInternacional = async () => {
            try {
                const response = await obtenerTiposComercioInternacional();
                setTiposComercioInternacional(
                    (response.data.data || []).map(t => ({
                        value: t.id,
                        label: t.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los tipos de comercio internacional',
                        severity: 'error'
                    }
                });
            }
        };
        cargarTiposComercioInternacional();
    }, [dispatch]);

    useEffect(() => {
        const cargarFuentesFinanciamiento = async () => {
            try {
                const response = await obtenerFuentesFinanciamiento();
                setFuentesFinanciamiento(
                    (response.data.data || []).map(f => ({
                        value: f.id,
                        label: f.descripcion
                    }))
                );
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar las fuentes de financiamiento',
                        severity: 'error'
                    }
                });
            }
        };
        cargarFuentesFinanciamiento();
    }, [dispatch]);

    return (
        <div className={classes.root}>
            <main className={classes.content}>
                <form onSubmit={handleSubmit}>
                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Nombre del Cliente/Empresa"><TextField name="nombreCliente" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.nombreCliente} error={!!errors.nombreCliente} helperText={errors.nombreCliente} /></FormField>
                            <FormField label="Nivel del Cliente/Empresa">
                                {renderSelect('nivelCliente', 'Seleccione un nivel', tiposClienteNivel)}
                            </FormField>
                            <FormField label="Contacto Primario">
                                <Autocomplete
                                    id="contacto-primario-autocomplete"
                                    options={opcionesContacto}
                                    getOptionLabel={(option) => option.label || ""}
                                    loading={loadingContacto}
                                    noOptionsText="No se encontraron contactos"
                                    loadingText="Buscando..."
                                    onInputChange={(event, newInputValue) => {
                                        handleBusqueda(newInputValue, buscarContactosPorTermino, setOpcionesContacto, setLoadingContacto);
                                    }}
                                    onChange={(event, newValue) => {
                                        setFormState(prevState => ({ ...prevState, contactoPrimarioId: newValue ? newValue.id : null }));
                                    }}
                                    renderInput={(params) => (
                                        <TextField {...params} placeholder="Buscar por nombre..." variant="outlined" className={classes.textField}
                                            error={!!errors.contactoPrimarioId}
                                            helperText={errors.contactoPrimarioId}
                                            InputProps={{
                                                ...params.InputProps,
                                                endAdornment: (
                                                    <React.Fragment>
                                                        {loadingContacto ? <CircularProgress color="inherit" size={20} /> : null}
                                                        {React.cloneElement(params.InputProps.endAdornment, { style: { display: 'none' } })}
                                                    </React.Fragment>
                                                ),
                                                startAdornment: (<InputAdornment position="start"><Search /></InputAdornment>),
                                            }}
                                        />
                                    )}
                                />
                            </FormField>
                            <FormField label="Estado del Cliente/Empresa">
                                {renderSelect('estadoCliente', 'Seleccione un estado', tiposClienteEstado)}
                            </FormField>
                            <FormField label="Asesor Principal">
                                <Autocomplete
                                    id="asesor-principal-autocomplete"
                                    options={opcionesAsesor}
                                    getOptionLabel={(option) => option.label || ""}
                                    loading={loadingAsesor}
                                    noOptionsText="No se encontraron asesores"
                                    loadingText="Buscando..."
                                    onInputChange={(event, newInputValue) => {
                                        handleBusqueda(newInputValue, buscarAsesoresPorTermino, setOpcionesAsesor, setLoadingAsesor);
                                    }}
                                    onChange={(event, newValue) => {
                                        setFormState(prevState => ({ ...prevState, asesorPrincipalId: newValue ? newValue.id : null }));
                                    }}
                                    renderInput={(params) => (
                                        <TextField {...params} placeholder="Buscar por nombre..." variant="outlined" className={classes.textField}
                                            error={!!errors.asesorPrincipalId}
                                            helperText={errors.asesorPrincipalId}
                                            InputProps={{
                                                ...params.InputProps,
                                                endAdornment: (
                                                    <React.Fragment>
                                                        {loadingAsesor ? <CircularProgress color="inherit" size={20} /> : null}
                                                        {React.cloneElement(params.InputProps.endAdornment, { style: { display: 'none' } })}
                                                    </React.Fragment>
                                                ),
                                                startAdornment: (<InputAdornment position="start"><Search /></InputAdornment>),
                                            }}
                                        />
                                    )}
                                />
                            </FormField>
                            <FormField label="Nombre del Propietario">
                                <Autocomplete
                                    id="propietario-autocomplete"
                                    options={opcionesPropietario}
                                    getOptionLabel={(option) => option.label || ""}
                                    loading={loadingPropietario}
                                    noOptionsText="No se encontraron contactos"
                                    loadingText="Buscando..."
                                    onInputChange={(event, newInputValue) => {
                                        handleBusqueda(newInputValue, buscarContactosPorTermino, setOpcionesPropietario, setLoadingPropietario);
                                    }}
                                    onChange={(event, newValue) => {
                                        setFormState(prevState => ({ ...prevState, nombrePropietarioId: newValue ? newValue.id : null }));
                                    }}
                                    renderInput={(params) => (
                                        <TextField {...params} placeholder="Buscar por nombre..." variant="outlined" className={classes.textField}
                                            error={!!errors.nombrePropietarioId}
                                            helperText={errors.nombrePropietarioId}
                                            InputProps={{
                                                ...params.InputProps,
                                                endAdornment: (
                                                    <React.Fragment>
                                                        {loadingPropietario ? <CircularProgress color="inherit" size={20} /> : null}
                                                        {React.cloneElement(params.InputProps.endAdornment, { style: { display: 'none' } })}
                                                    </React.Fragment>
                                                ),
                                                startAdornment: (<InputAdornment position="start"><Search /></InputAdornment>),
                                            }}
                                        />
                                    )}
                                />
                            </FormField>
                            <FormField label="Genero del Propietario">
                                {renderSelect('generoPropietario', 'Seleccione un género', [
                                    { value: 'Masculino', label: 'Masculino' },
                                    { value: 'Femenino', label: 'Femenino' }
                                ])}
                            </FormField>
                            <FormField label="Servicios Solicitados">
                                {renderSelect('serviciosSolicitados', 'Seleccione servicios', serviciosSolicitados)}
                            </FormField>
                            <FormField label="Razón social de la empresa, si fuese el caso" md={12} required={false}><TextField name="razonSocial" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.razonSocial} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Direccion Fisica" md={12}><TextField name="direccionFisica" multiline rows={3} fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.direccionFisica} error={!!errors.direccionFisica} helperText={errors.direccionFisica} /></FormField>
                            <FormField label="Municipio" md={6}><TextField name="municipio" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.municipio} error={!!errors.municipio} helperText={errors.municipio} /></FormField>
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
                            <FormField label="Numero de Telefono" md={3}><TextField name="numeroTelefono" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.numeroTelefono} error={!!errors.numeroTelefono} helperText={errors.numeroTelefono} /></FormField>
                            <FormField label="Pagina Web" md={3} required={false}><TextField name="paginaWeb" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.paginaWeb} /></FormField>
                            <FormField label="Correo Electronico" md={3}><TextField name="correoElectronico" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.correoElectronico} error={!!errors.correoElectronico} helperText={errors.correoElectronico} /></FormField>
                            <FormField label="Fecha de Inicio" md={3}><TextField name="fechaInicio" type="date" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.fechaInicio} InputLabelProps={{ shrink: true }} error={!!errors.fechaInicio} helperText={errors.fechaInicio} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3} alignItems="center">
                            <FormField label="Tipo de Organización">{renderSelect('tipoOrganizacion', 'Seleccione un tipo', tiposOrganizacion)}</FormField>
                            <FormField label="Tipo de Empresa">{renderSelect('tipoEmpresa', 'Seleccione un tipo', tiposEmpresa)}</FormField>
                            <FormField label="Tamaño de la Empresa">{renderSelect('tamanoEmpresa', 'Seleccione un tamaño', tamanosEmpresa)}</FormField>
                            <FormField label="Tipo de Contabilidad">{renderSelect('tipoContabilidad', 'Seleccione un tipo', tiposContabilidad)}</FormField>
                            <FormField label="Nivel de Formalizacion">{renderSelect('nivelFormalizacion', 'Seleccione un nivel', nivelesFormalizacion)}</FormField>
                            <FormField label="Tipo de Casos en Proceso" required={false}><TextField name="tipoCasos" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.tipoCasos} /></FormField>
                            <Grid item xs={12} md={6}><FormControlLabel control={<Switch name="participaAsociacion" checked={formState.participaAsociacion} onChange={handleChange} />} label="¿Participa en Alguna Asociacion Gremial?" /></Grid>
                            <Grid item xs={12} md={6}><FormControlLabel control={<Switch name="beneficiadoCDE" checked={formState.beneficiadoCDE} onChange={handleChange} />} label="¿Ha sido beneficiado por el CDE MIPYME ROC?" /></Grid>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3} alignItems="center">
                            <FormField label="Empleados de Tiempo Completo"><TextField name="empleadosCompleto" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.empleadosCompleto} error={!!errors.empleadosCompleto} helperText={errors.empleadosCompleto} /></FormField>
                            <FormField label="Empleados de Medio Tiempo" required={false}><TextField name="empleadosMedio" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.empleadosMedio} /></FormField>
                            <FormField label="Numero de Trabajadores Informales" required={false}><TextField name="trabajadoresInformales" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.trabajadoresInformales} /></FormField>
                            <FormField label="Tipo de Comercio Internacional" required={false}>{renderSelect('tipoComercio', 'Seleccione un tipo', tiposComercioInternacional)}</FormField>
                            <FormField label="Paises a los que exporta:" md={8} required={false}><TextField name="paisesExporta" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.paisesExporta} /></FormField>
                            <Grid item xs={12} md={3}><FormControlLabel control={<Switch name="negocioEnLinea" checked={formState.negocioEnLinea} onChange={handleChange} />} label="¿Negocio en Linea?" /></Grid>
                            <Grid item xs={12} md={3}><FormControlLabel control={<Switch name="negocioEnCasa" checked={formState.negocioEnCasa} onChange={handleChange} />} label="¿Negocio Basado en Casa?" /></Grid>
                            <Grid item xs={12} md={3}><FormControlLabel control={<Switch name="potencialContratoGob" checked={formState.potencialContratoGob} onChange={handleChange} />} label="¿Potencial Contrato con el Gobierno?" /></Grid>
                            <Grid item xs={12} md={3}><FormControlLabel control={<Switch name="localizadoZonaIndigena" checked={formState.localizadoZonaIndigena} onChange={handleChange} />} label="¿Localizado en Zona Indigena?" /></Grid>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3} alignItems="center">
                            <FormField label="Fuente de Financiamiento">{renderSelect('fuenteFinanciamiento', 'Seleccione una fuente', fuentesFinanciamiento)}</FormField>
                            <FormField label="Subfuente de Financiamiento" required={false}>{renderSelect('subFuente', 'Seleccione una subfuente', fuentesFinanciamiento)}</FormField>
                            <FormField label="Ingresos Brutos Anuales"><TextField name="ingresosAnuales" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.ingresosAnuales} error={!!errors.ingresosAnuales} helperText={errors.ingresosAnuales} /></FormField>
                            <FormField label="Fecha de ingresos brutos:" required={true}> {/* La hacemos requerida */}
                                <TextField
                                    name="fechaIngresos"
                                    type="date"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.fechaIngresos}
                                    InputLabelProps={{ shrink: true }}
                                    error={!!errors.fechaIngresos} // Conectar el error
                                    helperText={errors.fechaIngresos} // Mostrar el mensaje de error
                                />
                            </FormField>
                            <FormField label="Ingresos brutos anuales por exportaciones" required={false}><TextField name="ingresosExportaciones" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.ingresosExportaciones} /></FormField>
                            <FormField label="Ganancias/Perdidas Brutas Anuales" required={false}><TextField name="gananciasPerdidas" type="number" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.gananciasPerdidas} /></FormField>
                            <FormField label="Fecha de ganancias/perdidas brutas" required={true}>
                                <TextField
                                    name="fechaGanancias"
                                    type="date"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.fechaGanancias}
                                    InputLabelProps={{ shrink: true }}
                                    error={!!errors.fechaGanancias}
                                    helperText={errors.fechaGanancias}
                                />
                            </FormField>
                            <Grid item xs={12} md={4}><FormControlLabel control={<Switch name="adjudicadoFondo" checked={formState.adjudicadoFondo} onChange={handleChange} />} label="¿Se ha adjudicado con este emprendimiento algun fondo concursable?" /></Grid>
                            <Grid item xs={12} md={4}><FormField label="En caso de haber marcado la anterior opcion, indique que instruccion(es):" md={12} required={false}><TextField name="instruccionFondo" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.instruccionFondo} /></FormField></Grid>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Estatus Inicial">
                                {renderSelect('estatusInicial', 'Seleccione un estatus', [
                                    { value: 'Activo', label: 'Activo' },
                                    { value: 'Inactivo', label: 'Inactivo' }
                                ])}
                            </FormField>
                            <FormField label="Estatus Actual">
                                {renderSelect('estatusActual', 'Seleccione un estatus', [
                                    { value: 'Activo', label: 'Activo' },
                                    { value: 'Inactivo', label: 'Inactivo' }
                                ])}
                            </FormField>
                            <FormField label="Fecha de Establecimiento" required={true}>
                                <TextField
                                    name="fechaEstablecimiento"
                                    type="date"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.fechaEstablecimiento}
                                    InputLabelProps={{ shrink: true }}
                                    error={!!errors.fechaEstablecimiento}
                                    helperText={errors.fechaEstablecimiento}
                                />
                            </FormField>
                            <FormField label="¿Que inpide el crecimiento y desarrollo de su negocio?" md={12} required={false}>
                                <TextField
                                    name="impideCrecimiento"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.impideCrecimiento}
                                />
                            </FormField>
                            <FormField label="Indique que areas necesita dominar para el desarollo de su negocio:" md={6} required={false}><TextField name="areasNecesita" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.areasNecesita} /></FormField>
                            <FormField label="¿Donde desarolla o desarrollara este emprendimiento?" md={6} required={false}><TextField name="dondeDesarrolla" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.dondeDesarrolla} /></FormField>
                            <FormField label="¿Porque desea comenzar o potencias este emprendimiento?" md={12} required={false}><TextField name="porqueComenzarPotenciar1" multiline rows={3} fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.porqueComenzarPotenciar1} /></FormField>
                            <FormField label="Descripcion del producto y/o servicio:" md={12}><TextField name="descripcionProducto" multiline rows={4} fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.descripcionProducto} error={!!errors.descripcionProducto} helperText={errors.descripcionProducto} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3} alignItems="center">
                            <Grid item xs={12} md={6}><FormControlLabel control={<Switch name="solicitadoCredito" checked={formState.solicitadoCredito} onChange={handleChange} />} label="¿Ha solicitado algun credito o prestamo bancario para este negocio?" /></Grid>
                            <FormField label="En caso de marcar si, como lo solicito:" md={3} required={false}>
                                <TextField
                                    name="comoSolicito"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.comoSolicito}
                                />
                            </FormField>
                            <FormField label="En caso de marcar no, indique el porque:" md={3} required={false}>
                                <TextField
                                    name="porqueNoSolicito"
                                    fullWidth
                                    variant="outlined"
                                    className={classes.textField}
                                    onChange={handleChange}
                                    value={formState.porqueNoSolicito}
                                />
                            </FormField>
                            <Grid item xs={12} md={6}><FormControlLabel control={<Switch name="utilizaMediosPago" checked={formState.utilizaMediosPago} onChange={handleChange} />} label="¿Utiliza medios de pago electronicos para hacer transacciones comerciales?" /></Grid>
                            <FormField label="¿Que medios de pago utiliza o utilizaria para realizar transacciones comerciales?" md={6} required={false}><TextField name="queMediosPago" fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.queMediosPago} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Notas" md={12} required={false}><TextField name="notas" multiline rows={4} fullWidth variant="outlined" className={classes.textField} onChange={handleChange} value={formState.notas} /></FormField>
                        </Grid>
                    </Paper>

                    <Grid container spacing={2} justify="flex-end" className={classes.buttonContainer}>
                        <Grid item><Button
                            type="submit"
                            variant="contained"
                            className={classes.buttonGuardar}
                            disabled={loading}
                        >
                            Guardar
                            {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
                        </Button></Grid>
                        <Grid item><Button variant="contained" className={classes.buttonCancelar} onClick={handleCancel}>Cancelar</Button></Grid>
                    </Grid>
                </form>
            </main>
        </div>
    );
};

export default NuevoClienteEmpresa;