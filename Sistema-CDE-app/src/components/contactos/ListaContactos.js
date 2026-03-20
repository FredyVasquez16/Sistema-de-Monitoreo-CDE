import React, { useState, useEffect, useCallback } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    InputAdornment, IconButton, Table, TableBody, TableCell, TableContainer,
    TableHead, TableRow, Link, FormControl, InputLabel, Select, MenuItem,
    Collapse, Chip
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Search, FilterList, Add, Clear, ExpandMore, ExpandLess } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';
import { obtenerContactos, buscarContactosPorTermino, obtenerContactosFiltrados } from '../../actions/ContactoAction';
import { useStateValue } from '../../Context/store';
import debounce from 'lodash.debounce';

// Estilos del componente, inspirados en la imagen
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565', // Color de fondo principal (azul/verde oscuro)
        minHeight: '100vh',
    },
    appBar: {
        backgroundColor: '#D5A408', // Color dorado/mostaza de la barra
        color: '#000000', // Texto negro para contraste
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
    paper: {
        padding: theme.spacing(2),
        backgroundColor: '#EAEAEA', // Gris claro para el contenedor del contenido
    },
    tableHeader: {
        backgroundColor: '#D0D0D0', // Un gris un poco más oscuro para la cabecera
    },
    headerCell: {
        fontWeight: 'bold',
        color: '#000000',
    },
    newContactButton: {
        backgroundColor: '#42A5F5',
        color: 'white',
        '&:hover': {
            backgroundColor: '#1E88E5',
        }
    },
    searchBar: {
        backgroundColor: 'white',
        borderRadius: theme.shape.borderRadius,
    },
    filterButton: {
        marginLeft: theme.spacing(1),
    },
    filterPanel: {
        padding: theme.spacing(2),
        marginTop: theme.spacing(2),
        backgroundColor: '#F5F5F5',
        borderRadius: theme.shape.borderRadius,
    },
    filterChip: {
        marginRight: theme.spacing(0.5),
        marginBottom: theme.spacing(0.5),
    },
    clearButton: {
        marginLeft: theme.spacing(1),
    }
}));

// Datos de ejemplo para la tabla
const createData = (nombres, apellidos, telefono, correo, empresa, rtn) => {
    return { nombres, apellidos, telefono, correo, empresa, rtn };
};

const ListaContactos = () => {
    const classes = useStyles();
    const history = useHistory();
    const [contactos, setContactos] = useState([]);
    const [contactosFiltrados, setContactosFiltrados] = useState([]);
    const [loading, setLoading] = useState(false);
    const [, dispatch] = useStateValue();
    
    // Estados para búsqueda y filtros
    const [busqueda, setBusqueda] = useState('');
    const [showFilters, setShowFilters] = useState(false);
    const [filtros, setFiltros] = useState({
        departamento: '',
        municipio: '',
        categoriaLaboral: '',
        tieneNegocio: ''
    });
    const [filtrosActivos, setFiltrosActivos] = useState(0);

    const handleNavigate = (path) => {
        history.push(path);
    };

    // Función para filtrar contactos localmente
    const filtrarContactos = useCallback((termino, contactosOriginales) => {
        if (!termino && Object.values(filtros).every(f => !f)) {
            return contactosOriginales;
        }

        const terminoLower = termino.toLowerCase();
        
        return contactosOriginales.filter(contacto => {
            // Filtrar por término de búsqueda
            const matchBusqueda = !termino || 
                (contacto.nombre && contacto.nombre.toLowerCase().includes(terminoLower)) ||
                (contacto.apellido && contacto.apellido.toLowerCase().includes(terminoLower)) ||
                (contacto.correo && contacto.correo.toLowerCase().includes(terminoLower)) ||
                (contacto.telefono && contacto.telefono.toString().includes(termino)) ||
                (contacto.empresa && contacto.empresa.toLowerCase().includes(terminoLower)) ||
                (contacto.rtn && contacto.rtn.toLowerCase().includes(terminoLower));

            // Filtrar por departamento
            const matchDepartamento = !filtros.departamento || 
                (contacto.departamento && contacto.departamento.toLowerCase().includes(filtros.departamento.toLowerCase()));

            // Filtrar por municipio
            const matchMunicipio = !filtros.municipio || 
                (contacto.municipio && contacto.municipio.toLowerCase().includes(filtros.municipio.toLowerCase()));

            // Filtrar por categoría laboral
            const matchCategoria = !filtros.categoriaLaboral || 
                (contacto.categoriaLaboral && contacto.categoriaLaboral.toLowerCase().includes(filtros.categoriaLaboral.toLowerCase()));

            // Filtrar por tiene negocio
            const matchNegocio = !filtros.tieneNegocio || 
                (filtros.tieneNegocio === 'si' && contacto.poseeNegocio) ||
                (filtros.tieneNegocio === 'no' && !contacto.poseeNegocio);

            return matchBusqueda && matchDepartamento && matchMunicipio && matchCategoria && matchNegocio;
        });
    }, [filtros]);

    // Efecto para actualizar contactos filtrados cuando cambia la búsqueda o los filtros
    useEffect(() => {
        const filtrados = filtrarContactos(busqueda, contactos);
        setContactosFiltrados(filtrados);
    }, [busqueda, contactos, filtros, filtrarContactos]);

    // Contar filtros activos
    useEffect(() => {
        const count = Object.values(filtros).filter(f => f && f !== '').length;
        setFiltrosActivos(count);
    }, [filtros]);

    const handleBusquedaChange = (e) => {
        setBusqueda(e.target.value);
    };

    const handleFiltroChange = (campo, valor) => {
        setFiltros(prev => ({
            ...prev,
            [campo]: valor
        }));
    };

    const limpiarFiltros = () => {
        setFiltros({
            departamento: '',
            municipio: '',
            categoriaLaboral: '',
            tieneNegocio: ''
        });
        setBusqueda('');
    };

    useEffect(() => {
        const cargarContactos = async () => {
            setLoading(true);
            try {
                const data = await obtenerContactos();
                setContactos(data);
                setContactosFiltrados(data);
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los contactos',
                        severity: 'error'
                    }
                });
            } finally {
                setLoading(false);
            }
        };
        cargarContactos();
    }, [dispatch]);

    return (
        <div className={classes.root}>

            <main className={classes.content}>
                <Paper className={classes.paper}>
                    <Grid container justify="space-between" alignItems="center" spacing={2}>
                        <Grid item xs={12} md={6}>
                            <Box display="flex" alignItems="center">
                                <TextField
                                    fullWidth
                                    variant="outlined"
                                    placeholder="Buscar por nombre, correo, teléfono, empresa, RTN..."
                                    className={classes.searchBar}
                                    value={busqueda}
                                    onChange={handleBusquedaChange}
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start">
                                                <Search />
                                            </InputAdornment>
                                        ),
                                        endAdornment: busqueda && (
                                            <InputAdornment position="end">
                                                <IconButton size="small" onClick={() => setBusqueda('')}>
                                                    <Clear />
                                                </IconButton>
                                            </InputAdornment>
                                        )
                                    }}
                                />
                                <IconButton 
                                    className={classes.filterButton}
                                    onClick={() => setShowFilters(!showFilters)}
                                    color={showFilters || filtrosActivos > 0 ? "primary" : "default"}
                                    style={showFilters || filtrosActivos > 0 ? { backgroundColor: '#D5A408' } : {}}
                                >
                                    <FilterList />
                                </IconButton>
                                {filtrosActivos > 0 && (
                                    <Chip 
                                        label={filtrosActivos} 
                                        size="small" 
                                        color="primary" 
                                        className={classes.filterChip}
                                    />
                                )}
                            </Box>
                        </Grid>
                        <Grid item>
                            <Button
                                variant="contained"
                                className={classes.newContactButton}
                                startIcon={<Add />}
                                onClick={() => handleNavigate('/contactos/nuevo')}
                            >
                                Nuevo Contacto
                            </Button>
                        </Grid>
                    </Grid>

                    {/* Panel de Filtros */}
                    <Collapse in={showFilters}>
                        <Box className={classes.filterPanel}>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6} md={3}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        label="Departamento"
                                        size="small"
                                        value={filtros.departamento}
                                        onChange={(e) => handleFiltroChange('departamento', e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        label="Municipio"
                                        size="small"
                                        value={filtros.municipio}
                                        onChange={(e) => handleFiltroChange('municipio', e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <TextField
                                        fullWidth
                                        variant="outlined"
                                        label="Categoría Laboral"
                                        size="small"
                                        value={filtros.categoriaLaboral}
                                        onChange={(e) => handleFiltroChange('categoriaLaboral', e.target.value)}
                                    />
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>¿Tiene Negocio?</InputLabel>
                                        <Select
                                            value={filtros.tieneNegocio}
                                            onChange={(e) => handleFiltroChange('tieneNegocio', e.target.value)}
                                            label="¿Tiene Negocio?"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            <MenuItem value="si">Sí</MenuItem>
                                            <MenuItem value="no">No</MenuItem>
                                        </Select>
                                    </FormControl>
                                </Grid>
                            </Grid>
                            <Box mt={2} display="flex" justifyContent="flex-end">
                                <Button 
                                    variant="outlined" 
                                    size="small" 
                                    startIcon={<Clear />}
                                    onClick={limpiarFiltros}
                                >
                                    Limpiar Filtros
                                </Button>
                            </Box>
                        </Box>
                    </Collapse>

                    {/* Contador de resultados */}
                    <Box mt={2} mb={1}>
                        <Typography variant="body2" color="textSecondary">
                            {contactosFiltrados.length} contacto(s) encontrado(s)
                            {busqueda || filtrosActivos > 0 ? ' - ' : ''}
                            {busqueda && `búsqueda: "${busqueda}"`}
                        </Typography>
                    </Box>

                    <TableContainer style={{ marginTop: '20px' }}>
                        <Table>
                            <TableHead className={classes.tableHeader}>
                                <TableRow>
                                    <TableCell className={classes.headerCell}>Código</TableCell>
                                    <TableCell className={classes.headerCell}>Nombres</TableCell>
                                    <TableCell className={classes.headerCell}>Apellidos</TableCell>
                                    <TableCell className={classes.headerCell}>Teléfono</TableCell>
                                    <TableCell className={classes.headerCell}>Correo Electrónico</TableCell>
                                    <TableCell className={classes.headerCell}>Empresa/Cliente</TableCell>
                                    <TableCell className={classes.headerCell}>RTN</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {contactosFiltrados.length === 0 ? (
                                    <TableRow>
                                        <TableCell colSpan={7} align="center">
                                            <Typography variant="body1" color="textSecondary">
                                                {busqueda || filtrosActivos > 0 
                                                    ? 'No se encontraron contactos con los filtros aplicados' 
                                                    : 'No hay contactos registrados'}
                                            </Typography>
                                        </TableCell>
                                    </TableRow>
                                ) : (
                                    contactosFiltrados.map((row, index) => (
                                        <TableRow key={row.id || index} style={{ backgroundColor: index % 2 === 0 ? 'white' : '#F5F5F5' }}>
                                            <TableCell>{row.codigoUnico}</TableCell>
                                            <TableCell>
                                                <Link
                                                    component="button"
                                                    color="primary"
                                                    onClick={() => history.push(`/contactos/ver/${row.id}`)}
                                                    style={{ cursor: 'pointer', fontWeight: 'bold' }}
                                                >
                                                    {row.nombre}
                                                </Link>
                                            </TableCell>
                                            <TableCell>{row.apellido}</TableCell>
                                            <TableCell>{row.telefono}</TableCell>
                                            <TableCell>
                                                <Link href={`mailto:${row.correo}`} color="primary">
                                                    {row.correo}
                                                </Link>
                                            </TableCell>
                                            <TableCell>
                                                {row.clienteEmpresa ? (
                                                    <Link
                                                        component="button"
                                                        color="primary"
                                                        onClick={() => history.push(`/cliente_empresa/ver/${row.clienteEmpresa.id}`)}
                                                        style={{ cursor: 'pointer' }}
                                                    >
                                                        {row.clienteEmpresa.nombre}
                                                    </Link>
                                                ) : (
                                                    <Typography variant="body2" color="textSecondary">
                                                        Sin empresa
                                                    </Typography>
                                                )}
                                            </TableCell>
                                            <TableCell>{row.rtn}</TableCell>
                                        </TableRow>
                                    ))
                                )}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            </main>
        </div>
    );
};

export default ListaContactos;