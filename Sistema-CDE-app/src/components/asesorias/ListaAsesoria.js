// src/components/pages/ListaAsesoria.js

import React, { useEffect, useState } from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    InputAdornment, IconButton, Table, TableContainer, TableHead, TableRow,
    TableCell, TableBody, Link, CircularProgress
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Search, FilterList, Add } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';
import { useStateValue } from '../../Context/store';
import { obtenerAsesorias } from '../../actions/AsesoriaAction';

// Estilos consistentes con las vistas anteriores
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565', // Fondo azul oscuro consistente
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
        backgroundColor: '#42A5F5',
        color: 'white',
        '&:hover': {
            backgroundColor: '#1E88E5',
        }
    },
    searchBar: {
        backgroundColor: 'white',
        borderRadius: theme.shape.borderRadius,
    }
}));

// Formatea la fecha de sesión en un formato legible (es-HN)
const formatearFecha = (fecha) => {
    if (!fecha) return '—';
    const d = new Date(fecha);
    return isNaN(d.getTime()) ? fecha : d.toLocaleDateString('es-HN', { year: 'numeric', month: 'long', day: 'numeric' });
};

const ListaAsesoria = () => {
    const classes = useStyles();
    const history = useHistory();
    const [, dispatch] = useStateValue();
    const [rows, setRows] = useState([]);
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        const cargarAsesorias = async () => {
            try {
                const data = await obtenerAsesorias();
                setRows(data || []);
            } catch (error) {
                console.error('Error al cargar las asesorías:', error);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Error al cargar las asesorías', severity: 'error' },
                });
            } finally {
                setCargando(false);
            }
        };

        cargarAsesorias();
    }, [dispatch]);

    const handleNavigate = (path) => {
        history.push(path);
    };

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
                                    placeholder="Buscar por cliente, asesor..."
                                    className={classes.searchBar}
                                    InputProps={{
                                        startAdornment: (
                                            <InputAdornment position="start"><Search /></InputAdornment>
                                        ),
                                    }}
                                />
                                <IconButton><FilterList /></IconButton>
                            </Box>
                        </Grid>
                        <Grid item>
                            <Button
                                variant="contained"
                                className={classes.newButton}
                                startIcon={<Add />}
                                onClick={() => handleNavigate('/asesoria/nuevo')}
                            >
                                Nueva Asesoría
                            </Button>
                        </Grid>
                    </Grid>

                    {cargando ? (
                        <Box display="flex" justifyContent="center" alignItems="center" style={{ marginTop: '40px' }}>
                            <CircularProgress style={{ color: '#D5A408' }} />
                        </Box>
                    ) : (
                        <TableContainer style={{ marginTop: '20px' }}>
                            <Table>
                                <TableHead className={classes.tableHeader}>
                                    <TableRow>
                                        <TableCell className={classes.headerCell}>Cliente/Empresa</TableCell>
                                        <TableCell className={classes.headerCell}>Fecha de Sesión</TableCell>
                                        <TableCell className={classes.headerCell}>Asesor Principal</TableCell>
                                        <TableCell className={classes.headerCell}>Área de Asesoría</TableCell>
                                        <TableCell className={classes.headerCell}>Tipo de Contacto</TableCell>
                                        <TableCell className={classes.headerCell}>Tiempo (h:mm)</TableCell>
                                    </TableRow>
                                </TableHead>
                                <TableBody>
                                    {rows.map((row, index) => (
                                        <TableRow key={index} style={{ backgroundColor: index % 2 === 0 ? 'white' : '#F5F5F5' }}>
                                            <TableCell>
                                                <Link component="button" variant="body2" onClick={() => handleNavigate(`/asesoria/ver/${row.id}`)}>
                                                    {row.clienteNombre}
                                                </Link>
                                            </TableCell>
                                            <TableCell>{formatearFecha(row.fechaSesion)}</TableCell>
                                            <TableCell>{row.asesores && row.asesores[0] ? row.asesores[0].nombreCompleto : '—'}</TableCell>
                                            <TableCell>{row.areaAsesoriaNombre}</TableCell>
                                            <TableCell>{row.tipoContactoNombre}</TableCell>
                                            <TableCell>{row.tiempoContacto && typeof row.tiempoContacto === 'string' ? row.tiempoContacto.slice(0, 5) : '—'}</TableCell>
                                        </TableRow>
                                    ))}
                                </TableBody>
                            </Table>
                        </TableContainer>
                    )}
                </Paper>
            </main>
        </div>
    );
};

export default ListaAsesoria;
