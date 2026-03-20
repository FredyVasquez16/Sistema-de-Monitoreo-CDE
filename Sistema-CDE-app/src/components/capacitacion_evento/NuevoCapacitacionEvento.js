import React, { useState } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    Select, MenuItem, FormControl, Input, Chip, FormHelperText
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { useHistory } from 'react-router-dom';
import { validarCapacitacion } from '../../validators/CapacitacionValidator';

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
    chips: { display: 'flex', flexWrap: 'wrap' },
    chip: { margin: 2 },
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

const temasDisponibles = [
    'Contabilidad y Presupuestos', 'Iniciar un negocio', 'Relaciones Públicas',
    'Franquicias', 'Temas Legales', 'Tecnología', 'Financiación de Empresas',
    'Plan de Negocios', 'Comercio Electrónico', 'Administración de Empresas',
    'Mercadotecnia y Ventas', 'Empresa liderada por una mujer'
];

const ubicaciones = {
    "Copán": ["Santa Rosa de Copán", "La Entrada", "Copán Ruinas", "Florida"],
    "Lempira": ["Gracias", "La Iguala", "Erandique", "Belén"],
    "Ocotepeque": ["Ocotepeque", "San Marcos", "Sinuapa", "La Labor"],
};
const departamentos = Object.keys(ubicaciones); // ["Copán", "Lempira", "Ocotepeque"]

const NuevoCapacitacionEvento = () => {
    const classes = useStyles();
    const history = useHistory();
    const [errors, setErrors] = useState({});
    const [formState, setFormState] = useState({
        Temas: [], Centro: 'CDE MIPYME ROC', TipoId: '', Titulo: '', FechaInicio: '', FechaCierre: '', FechaInformes: '',
        HoraProgramada: '', TotalHoras: '', Descripcion: '', TemaPrincipalId: '', FormatoProgramaId: '', Estado: '', NumeroMaxParticipantes: '',
        Direccion: '', Ciudad: '', Departamento: '', LugarDesarrollo: '', PatrociniosCentro: '', CoPatrocinios: '', Recursos: '', Contacto: '',
        CorreoContacto: '', TelefonoContacto: '', Idioma: '', UnidadHistorica: '', FuenteFinanciamientoId: '', InstruccionesAsistente: '',
        Notas: '', NumeroSesiones: '',
    });

    const tipoDeRegistro = formState.TipoId === 2 ? 'del Evento' : 'de la Capacitación';

    const handleChange = (event) => {
        const { name, value } = event.target;
        setFormState(prevState => {
            const newState = { ...prevState, [name]: value };
            // Si el campo que cambió es 'Departamento', reiniciamos 'Ciudad'
            if (name === 'Departamento') {
                newState.Ciudad = '';
            }
            return newState;
        });
    };

    const handleCancel = () => { history.goBack(); };

    const handleSubmit = (event) => {
        event.preventDefault();
        const validationErrors = validarCapacitacion(formState);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            console.log("Formulario válido, enviando datos:", formState);
            alert(`'${tipoDeRegistro}' guardado con éxito (simulación).`);
            history.push('/capacitaciones');
        } else {
            console.log("Errores de validación encontrados:", validationErrors);
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
                            <FormField label="Tipo">
                                {renderSelect('TipoId', 'Seleccione un tipo', [
                                    { value: 1, label: 'Capacitación' },
                                    { value: 2, label: 'Evento' }
                                ])}
                            </FormField>
                            <FormField label={`Título ${tipoDeRegistro}`} md={8}>
                                <TextField name="Titulo" fullWidth variant="outlined" className={classes.textField} value={formState.Titulo} onChange={handleChange} error={!!errors.Titulo} helperText={errors.Titulo} />
                            </FormField>
                            <FormField label="Fecha de Inicio"><TextField name="FechaInicio" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.FechaInicio} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.FechaInicio} helperText={errors.FechaInicio} /></FormField>
                            <FormField label="Fecha de Cierre"><TextField name="FechaCierre" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.FechaCierre} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.FechaCierre} helperText={errors.FechaCierre} /></FormField>
                            <FormField label="Fecha de Informes"><TextField name="FechaInformes" type="date" fullWidth variant="outlined" className={classes.textField} value={formState.FechaInformes} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.FechaInformes} helperText={errors.FechaInformes} /></FormField>
                            <FormField label="Hora Programada"><TextField name="HoraProgramada" type="time" fullWidth variant="outlined" className={classes.textField} value={formState.HoraProgramada} onChange={handleChange} InputLabelProps={{ shrink: true }} error={!!errors.HoraProgramada} helperText={errors.HoraProgramada} /></FormField>
                            <FormField label="Total de Horas" required={false}><TextField name="TotalHoras" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.TotalHoras} onChange={handleChange} /></FormField>
                            <FormField label="Formato del Programa">{renderSelect('FormatoProgramaId', 'Seleccione un formato', [{ value: 1, label: 'Curso' }, { value: 2, label: 'Seminario' }])}</FormField>

                            <FormField label={`Temas ${tipoDeRegistro}`} md={8} required={false}>
                                <FormControl variant="outlined" fullWidth className={classes.textField}>
                                    <Select multiple name="Temas" value={formState.Temas} onChange={handleChange} input={<Input />}
                                        renderValue={(selected) => (
                                            <div className={classes.chips}>
                                                {selected.map((value) => (<Chip key={value} label={value} className={classes.chip} />))}
                                            </div>
                                        )}>
                                        <MenuItem value="" disabled>Seleccione los temas</MenuItem>
                                        {temasDisponibles.map((tema) => (<MenuItem key={tema} value={tema}>{tema}</MenuItem>))}
                                    </Select>
                                </FormControl>
                            </FormField>
                            <FormField label="Tema Principal">
                                {renderSelect('TemaPrincipalId', 'Seleccione un tema', [{ value: 1, label: 'Contabilidad' }, { value: 2, label: 'Marketing' }])}
                            </FormField>

                            <FormField label="Descripción" md={12} required={false}><TextField name="Descripcion" multiline rows={4} fullWidth variant="outlined" className={classes.textField} value={formState.Descripcion} onChange={handleChange} /></FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Estado" required={false}>{renderSelect('Estado', 'Seleccione un estado', [{ value: 'Abierto', label: 'Abierto' }, { value: 'Completo', label: 'Completo' }])}</FormField>
                            <FormField label="Número Máximo de Participantes" required={false}><TextField name="NumeroMaxParticipantes" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.NumeroMaxParticipantes} onChange={handleChange} /></FormField>
                            <FormField label="Dirección">
                                <TextField name="Direccion" fullWidth variant="outlined" className={classes.textField} value={formState.Direccion} onChange={handleChange} error={!!errors.Direccion} helperText={errors.Direccion} />
                            </FormField>
                            <FormField label="Departamento">
                                <FormControl variant="outlined" fullWidth className={classes.textField} error={!!errors.Departamento}>
                                    <Select name="Departamento" value={formState.Departamento} onChange={handleChange} displayEmpty>
                                        <MenuItem value="" disabled><em className={classes.placeholder}>Seleccione un departamento</em></MenuItem>
                                        {departamentos.map(dep => <MenuItem key={dep} value={dep}>{dep}</MenuItem>)}
                                    </Select>
                                    {errors.Departamento && <FormHelperText>{errors.Departamento}</FormHelperText>}
                                </FormControl>
                            </FormField>

                            {/* 4. Reemplazamos TextField por Select para Ciudad (dependiente) */}
                            <FormField label="Municipio">
                                <FormControl variant="outlined" fullWidth className={classes.textField} disabled={!formState.Departamento} error={!!errors.Ciudad}>
                                    <Select name="Ciudad" value={formState.Ciudad} onChange={handleChange} displayEmpty>
                                        <MenuItem value="" disabled><em className={classes.placeholder}>Seleccione un municipio</em></MenuItem>
                                        {/* Las opciones se cargan dinámicamente según el departamento seleccionado */}
                                        {formState.Departamento && ubicaciones[formState.Departamento].map(ciudad => (
                                            <MenuItem key={ciudad} value={ciudad}>{ciudad}</MenuItem>
                                        ))}
                                    </Select>
                                    {errors.Ciudad && <FormHelperText>{errors.Ciudad}</FormHelperText>}
                                </FormControl>
                            </FormField>
                            <FormField label="Lugar donde se desarrollará">
                                <TextField name="LugarDesarrollo" fullWidth variant="outlined" className={classes.textField} value={formState.LugarDesarrollo} onChange={handleChange} error={!!errors.LugarDesarrollo} helperText={errors.LugarDesarrollo} />
                            </FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Centro" required={false}><TextField name="Centro" disabled fullWidth variant="outlined" className={classes.textField} value={formState.Centro} /></FormField>
                            <FormField label="Patrocinio del Centro" required={false}><TextField name="PatrociniosCentro" fullWidth variant="outlined" className={classes.textField} value={formState.PatrociniosCentro} onChange={handleChange} /></FormField>
                            <FormField label="Co-Patrocinios" required={false}><TextField name="CoPatrocinios" fullWidth variant="outlined" className={classes.textField} value={formState.CoPatrocinios} onChange={handleChange} /></FormField>
                            <FormField label="Recursos" required={false}><TextField name="Recursos" fullWidth variant="outlined" className={classes.textField} value={formState.Recursos} onChange={handleChange} /></FormField>
                            <FormField label="Contacto" required={false}><TextField name="Contacto" fullWidth variant="outlined" className={classes.textField} value={formState.Contacto} onChange={handleChange} /></FormField>
                            <FormField label="Correo del Contacto" required={false}><TextField name="CorreoContacto" type="email" fullWidth variant="outlined" className={classes.textField} value={formState.CorreoContacto} onChange={handleChange} /></FormField>
                            <FormField label="Teléfono del Contacto" required={false}><TextField name="TelefonoContacto" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.TelefonoContacto} onChange={handleChange} /></FormField>
                            <FormField label="Idioma" required={false}>{renderSelect('Idioma', 'Seleccione un idioma', [{ value: 'Español', label: 'Español' }, { value: 'Inglés', label: 'Inglés' }])}</FormField>
                            <FormField label="Unidad Histórica" required={false}>{renderSelect('UnidadHistorica', 'Seleccione una unidad', [{ value: 'Nuevo', label: 'Nuevo' }, { value: 'Continuación', label: 'Continuación' }])}</FormField>
                            <FormField label="Fuente de Financiamiento" required={false}>{renderSelect('FuenteFinanciamientoId', 'Seleccione una fuente', [{ value: 1, label: 'Otra' }])}</FormField>
                            <FormField label="Número de Sesiones">
                                <TextField name="NumeroSesiones" type="number" fullWidth variant="outlined" className={classes.textField} value={formState.NumeroSesiones} onChange={handleChange} error={!!errors.NumeroSesiones} helperText={errors.NumeroSesiones} />
                            </FormField>
                        </Grid>
                    </Paper>

                    <Paper className={classes.formSection}>
                        <Grid container spacing={3}>
                            <FormField label="Instrucciones para Asistentes" md={12} required={false}><TextField name="InstruccionesAsistente" multiline rows={4} fullWidth variant="outlined" className={classes.textField} value={formState.InstruccionesAsistente} onChange={handleChange} /></FormField>
                            <FormField label="Notas" md={12} required={false}><TextField name="Notas" multiline rows={4} fullWidth variant="outlined" className={classes.textField} value={formState.Notas} onChange={handleChange} /></FormField>
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

export default NuevoCapacitacionEvento;