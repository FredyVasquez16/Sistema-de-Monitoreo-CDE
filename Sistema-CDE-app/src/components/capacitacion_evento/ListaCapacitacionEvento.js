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
import { obtenerCapacitaciones } from '../../actions/CapacitacionEventoAction';

// Estilos consistentes con las vistas anteriores
const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
        backgroundColor: '#033565',
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

// Formatea la fecha en un formato legible (es-HN)
const formatearFecha = (fecha) => {
    if (!fecha) return '—';
    const d = new Date(fecha);
    return isNaN(d.getTime()) ? fecha : d.toLocaleDateString('es-HN', { year: 'numeric', month: 'long', day: 'numeric' });
};

const ListaCapacitacionEvento = () => {
    const classes = useStyles();
    const history = useHistory();
    const [, dispatch] = useStateValue();
    const [rows, setRows] = useState([]);
    const [cargando, setCargando] = useState(true);

    useEffect(() => {
        const cargarCapacitaciones = async () => {
            try {
                const data = await obtenerCapacitaciones();
                setRows(data || []);
            } catch (error) {
                console.error('Error al cargar las capacitaciones y eventos:', error);
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Error al cargar las capacitaciones y eventos', severity: 'error' },
                });
            } finally {
                setCargando(false);
            }
        };

        cargarCapacitaciones();
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
                                    placeholder="Buscar por título, tema..."
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
                                onClick={() => handleNavigate('/capacitacion_evento/nuevo')}
                            >
                                Nueva Capacitación
                            </Button>
                        </Grid>
                    </Grid>

                    <TableContainer style={{ marginTop: '20px' }}>
                        <Table>
                            <TableHead className={classes.tableHeader}>
                                <TableRow>
                                    <TableCell className={classes.headerCell}>Título de la Capacitación</TableCell>
                                    <TableCell className={classes.headerCell}>Fecha de Inicio</TableCell>
                                    <TableCell className={classes.headerCell}>Fecha de Cierre</TableCell>
                                    <TableCell className={classes.headerCell}>Tema Principal</TableCell>
                                    <TableCell className={classes.headerCell}>Estado</TableCell>
                                </TableRow>
                            </TableHead>
                            <TableBody>
                                {cargando ? (
                                    <TableRow>
                                        <TableCell colSpan={5} align="center">
                                            <CircularProgress style={{ color: '#D5A408' }} />
                                        </TableCell>
                                    </TableRow>
                                ) : (
                                    rows.map((row, index) => (
                                        <TableRow key={row.id} style={{ backgroundColor: index % 2 === 0 ? 'white' : '#F5F5F5' }}>
                                            <TableCell>
                                                <Link component="button" variant="body2" onClick={() => handleNavigate(`/capacitacion_evento/ver/${row.id}`)}>
                                                    {row.titulo}
                                                </Link>
                                            </TableCell>
                                            <TableCell>{formatearFecha(row.fechaInicio)}</TableCell>
                                            <TableCell>{formatearFecha(row.fechaCierre)}</TableCell>
                                            <TableCell>{row.temaPrincipalId}</TableCell>
                                            <TableCell>{row.estado || '—'}</TableCell>
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

export default ListaCapacitacionEvento;
