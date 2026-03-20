// src/components/pages/ListaClienteEmpresa.js

import React, { useState, useEffect, useCallback } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    InputAdornment, IconButton, Table, TableContainer, TableHead, TableRow,
    TableCell, TableBody, Link, FormControl, InputLabel, Select, MenuItem,
    Collapse, Chip
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Search, FilterList, Add, Clear } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';
import { 
    obtenerClientesEmpresas, 
    obtenerTiposClientesNivel,
    obtenerServiciosSolicitados,
    obtenerTiposClientesEstado,
    obtenerTiposOrganizacion,
    obtenerTiposEmpresa,
    obtenerTamanosEmpresa
} from '../../actions/ClienteEmpresaAction';
import { useStateValue } from '../../Context/store';

// Estilos del componente, consistentes con las vistas anteriores
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565', // Fondo azul oscuro
        minHeight: '100vh',
    },
    appBar: {
        backgroundColor: '#D5A408',
        color: '#000000',
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
        backgroundColor: '#EAEAEA',
        borderRadius: '12px',
    },
    tableHeader: {
        backgroundColor: '#D0D0D0',
    },
    headerCell: {
        fontWeight: 'bold',
        color: '#000000',
    },
    newButton: {
        backgroundColor: '#42A5F5', // Azul claro para el botón
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
    }
}));

const ListaClienteEmpresa = () => {
    const classes = useStyles();
    const history = useHistory();
    const [clientes, setClientes] = useState([]);
    const [clientesFiltrados, setClientesFiltrados] = useState([]);
    const [loading, setLoading] = useState(false);
    const [, dispatch] = useStateValue();
    
    // Estados para catálogos de filtros
    const [catalogos, setCatalogos] = useState({
        niveles: [],
        servicios: [],
        estados: [],
        tiposOrganizacion: [],
        tiposEmpresa: [],
        tamanos: []
    });
    const [catalogosCargados, setCatalogosCargados] = useState(false);
    
    // Estados para búsqueda y filtros
    const [busqueda, setBusqueda] = useState('');
    const [showFilters, setShowFilters] = useState(false);
    const [filtros, setFiltros] = useState({
        nivel: '',
        servicio: '',
        estado: '',
        tipoOrganizacion: '',
        tipoEmpresa: '',
        tamano: '',
        departamento: ''
    });
    const [filtrosActivos, setFiltrosActivos] = useState(0);

    const handleNavigate = (path) => {
        history.push(path);
    };

    // Función para cargar catálogos
    useEffect(() => {
        const cargarCatalogos = async () => {
            try {
                const [nivelesRes, serviciosRes, estadosRes, tiposOrgRes, tiposEmpRes, tamanosRes] = await Promise.all([
                    obtenerTiposClientesNivel(),
                    obtenerServiciosSolicitados(),
                    obtenerTiposClientesEstado(),
                    obtenerTiposOrganizacion(),
                    obtenerTiposEmpresa(),
                    obtenerTamanosEmpresa()
                ]);
                
                // Las funciones devuelven { data: { status, data: [...] } }
                setCatalogos({
                    niveles: nivelesRes.data?.data || [],
                    servicios: serviciosRes.data?.data || [],
                    estados: estadosRes.data?.data || [],
                    tiposOrganizacion: tiposOrgRes.data?.data || [],
                    tiposEmpresa: tiposEmpRes.data?.data || [],
                    tamanos: tamanosRes.data?.data || []
                });
                setCatalogosCargados(true);
            } catch (error) {
                console.error('Error al cargar catálogos:', error);
                // En caso de error, inicializar con arrays vacíos para evitar errores
                setCatalogos({
                    niveles: [],
                    servicios: [],
                    estados: [],
                    tiposOrganizacion: [],
                    tiposEmpresa: [],
                    tamanos: []
                });
                setCatalogosCargados(true); // Marcar como cargado aunque sea vacío
            }
        };
        cargarCatalogos();
    }, []);

    // Función para filtrar clientes localmente
    const filtrarClientes = useCallback((termino, clientesOriginales) => {
        if (!termino && Object.values(filtros).every(f => !f)) {
            return clientesOriginales;
        }

        const terminoLower = termino.toLowerCase();
        
        return clientesOriginales.filter(cliente => {
            // Filtrar por término de búsqueda
            const matchBusqueda = !termino || 
                (cliente.nombre && cliente.nombre.toLowerCase().includes(terminoLower)) ||
                (cliente.nombreCliente && cliente.nombreCliente.toLowerCase().includes(terminoLower)) ||
                (cliente.contactoPrimarioNombre && cliente.contactoPrimarioNombre.toLowerCase().includes(terminoLower)) ||
                (cliente.correo && cliente.correo.toLowerCase().includes(terminoLower)) ||
                (cliente.correoElectronico && cliente.correoElectronico.toLowerCase().includes(terminoLower)) ||
                (cliente.telefono && cliente.telefono.toString().includes(termino)) ||
                (cliente.numeroTelefono && cliente.numeroTelefono.toString().includes(termino)) ||
                (cliente.asesorPrincipalNombre && cliente.asesorPrincipalNombre.toLowerCase().includes(terminoLower)) ||
                (cliente.codigoUnico && cliente.codigoUnico.toLowerCase().includes(terminoLower));

            // Filtrar por nivel
            const matchNivel = !filtros.nivel || 
                (cliente.nivel && cliente.nivel.toString() === filtros.nivel.toString());

            // Filtrar por servicio
            const matchServicio = !filtros.servicio || 
                (cliente.servicio && cliente.servicio.toString() === filtros.servicio.toString());

            // Filtrar por estado
            const matchEstado = !filtros.estado || 
                (cliente.estatusActual && cliente.estatusActual.toString() === filtros.estado.toString());

            // Filtrar por tipo organización
            const matchTipoOrg = !filtros.tipoOrganizacion || 
                (cliente.tipoOrganizacion && cliente.tipoOrganizacion.toString() === filtros.tipoOrganizacion.toString());

            // Filtrar por tipo empresa
            const matchTipoEmp = !filtros.tipoEmpresa || 
                (cliente.tipoEmpresa && cliente.tipoEmpresa.toString() === filtros.tipoEmpresa.toString());

            // Filtrar por tamaño
            const matchTamano = !filtros.tamano || 
                (cliente.tamano && cliente.tamano.toString() === filtros.tamano.toString());

            // Filtrar por departamento
            const matchDepto = !filtros.departamento || 
                (cliente.departamento && cliente.departamento.toLowerCase().includes(filtros.departamento.toLowerCase()));

            return matchBusqueda && matchNivel && matchServicio && matchEstado && 
                   matchTipoOrg && matchTipoEmp && matchTamano && matchDepto;
        });
    }, [filtros]);

    // Efecto para actualizar clientes filtrados cuando cambia la búsqueda o los filtros
    useEffect(() => {
        const filtrados = filtrarClientes(busqueda, clientes);
        setClientesFiltrados(filtrados);
    }, [busqueda, clientes, filtros, filtrarClientes]);

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
            nivel: '',
            servicio: '',
            estado: '',
            tipoOrganizacion: '',
            tipoEmpresa: '',
            tamano: '',
            departamento: ''
        });
        setBusqueda('');
    };

    useEffect(() => {
        const cargarClientes = async () => {
            setLoading(true);
            try {
                const data = await obtenerClientesEmpresas();
                setClientes(data);
                setClientesFiltrados(data);
            } catch (error) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: 'Error al cargar los clientes/empresas',
                        severity: 'error'
                    }
                });
            } finally {
                setLoading(false);
            }
        };
        cargarClientes();
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
                                    placeholder="Buscar por nombre, contacto, asesor, código..."
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
                                className={classes.newButton}
                                startIcon={<Add />}
                                onClick={() => handleNavigate('/cliente_empresa/nuevo')}
                            >
                                Nueva Cliente/Empresa
                            </Button>
                        </Grid>
                    </Grid>

                    {/* Panel de Filtros */}
                    <Collapse in={showFilters && catalogosCargados}>
                        <Box className={classes.filterPanel}>
                            <Grid container spacing={2}>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Nivel</InputLabel>
                                        <Select
                                            value={filtros.nivel}
                                            onChange={(e) => handleFiltroChange('nivel', e.target.value)}
                                            label="Nivel"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.niveles || []).map((nivel) => (
                                                <MenuItem key={nivel.id} value={nivel.id}>{nivel.descripcion || nivel.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Estado</InputLabel>
                                        <Select
                                            value={filtros.estado}
                                            onChange={(e) => handleFiltroChange('estado', e.target.value)}
                                            label="Estado"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.estados || []).map((estado) => (
                                                <MenuItem key={estado.id} value={estado.id}>{estado.descripcion || estado.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Tipo Organización</InputLabel>
                                        <Select
                                            value={filtros.tipoOrganizacion}
                                            onChange={(e) => handleFiltroChange('tipoOrganizacion', e.target.value)}
                                            label="Tipo Organización"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.tiposOrganizacion || []).map((tipo) => (
                                                <MenuItem key={tipo.id} value={tipo.id}>{tipo.descripcion || tipo.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Tipo Empresa</InputLabel>
                                        <Select
                                            value={filtros.tipoEmpresa}
                                            onChange={(e) => handleFiltroChange('tipoEmpresa', e.target.value)}
                                            label="Tipo Empresa"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.tiposEmpresa || []).map((tipo) => (
                                                <MenuItem key={tipo.id} value={tipo.id}>{tipo.descripcion || tipo.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Tamaño</InputLabel>
                                        <Select
                                            value={filtros.tamano}
                                            onChange={(e) => handleFiltroChange('tamano', e.target.value)}
                                            label="Tamaño"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.tamanos || []).map((tamano) => (
                                                <MenuItem key={tamano.id} value={tamano.id}>{tamano.descripcion || tamano.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
                                <Grid item xs={12} sm={6} md={3}>
                                    <FormControl fullWidth variant="outlined" size="small">
                                        <InputLabel>Servicio</InputLabel>
                                        <Select
                                            value={filtros.servicio}
                                            onChange={(e) => handleFiltroChange('servicio', e.target.value)}
                                            label="Servicio"
                                        >
                                            <MenuItem value=""><em>Todos</em></MenuItem>
                                            {(catalogos.servicios || []).map((servicio) => (
                                                <MenuItem key={servicio.id} value={servicio.id}>{servicio.descripcion || servicio.nombre}</MenuItem>
                                            ))}
                                        </Select>
                                    </FormControl>
                                </Grid>
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
                            {clientesFiltrados.length} cliente(s)/empresa(s) encontrado(s)
                            {busqueda || filtrosActivos > 0 ? ' - ' : ''}
                            {busqueda && `búsqueda: "${busqueda}"`}
                        </Typography>
                    </Box>

                    <TableContainer style={{ marginTop: '20px' }}>
                        <Table>
                            <TableHead className={classes.tableHeader}>
                                <TableRow>
                                    <TableCell className={classes.headerCell}>Código</TableCell>
                                    <TableCell className={classes.headerCell}>Nombre de la Empresa</TableCell>
                                    <TableCell className={classes.headerCell}>Contacto Primario</TableCell>
                                    <TableCell className={classes.headerCell}>Teléfono</TableCell>
                                    <TableCell className={classes.headerCell}>Correo Electrónico</TableCell>
                                    <TableCell className={classes.headerCell}>Asesor Principal</TableCell>
                                    <TableCell className={classes.headerCell}>Estatus Actual</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {clientesFiltrados.length === 0 ? (
                                    <TableRow>
                                        <TableCell colSpan={7} align="center">
                                            <Typography variant="body1" color="textSecondary">
                                                {busqueda || filtrosActivos > 0 
                                                    ? 'No se encontraron clientes/empresas con los filtros aplicados' 
                                                    : 'No hay clientes/empresas registradas'}
                                            </Typography>
                                        </TableCell>
                                    </TableRow>
                                ) : (
                                    clientesFiltrados.map((row, index) => (
                                        <TableRow key={row.id || index} style={{ backgroundColor: index % 2 === 0 ? 'white' : '#F5F5F5' }}>
                                            <TableCell>{row.codigoUnico || ''}</TableCell>
                                            <TableCell>
                                                <Link component="button" variant="body2" onClick={() => handleNavigate(`/cliente_empresa/ver/${row.id}`)}>
                                                    {row.nombre || row.nombreCliente || ''}
                                                </Link>
                                            </TableCell>
                                            <TableCell>{row.contactoPrimarioNombre || row.contactoPrimario || ''}</TableCell>
                                            <TableCell>{row.telefono || row.numeroTelefono || ''}</TableCell>
                                            <TableCell>
                                                <Link href={`mailto:${row.correo || row.correoElectronico || ''}`} color="primary">
                                                    {row.correo || row.correoElectronico || ''}
                                                </Link>
                                            </TableCell>
                                            <TableCell>{row.asesorPrincipalNombre || ''}</TableCell>
                                            <TableCell>{row.estatusActual || row.estadoCliente || ''}</TableCell>
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

export default ListaClienteEmpresa;