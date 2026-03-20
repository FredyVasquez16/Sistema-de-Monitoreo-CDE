// src/components/pages/ListaAsesoria.js

import React from 'react';
import {
    AppBar, Toolbar, Typography, Button, Box, Paper, Grid, TextField,
    InputAdornment, IconButton, Table, TableContainer, TableHead, TableRow,
    TableCell, TableBody, Link
} from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Search, FilterList, Add } from '@material-ui/icons';
import { useHistory } from 'react-router-dom';

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

// Datos de ejemplo para la tabla de asesorías
const createData = (cliente, fecha, asesor, area, tipo, tiempo) => {
    return { cliente, fecha, asesor, area, tipo, tiempo };
};

const rows = [
    createData('Café Copán', '2025-08-15', 'Kristel Padilla', 'Finanzas', 'Presencial', '1:30'),
    createData('Inversiones S. de R.L.', '2025-08-14', 'Jose Manuel', 'Marketing Digital', 'En Línea', '1:00'),
    createData('Tienda La Bendición', '2025-08-12', 'Kristel Padilla', 'Plan de Negocios', 'Telefónico', '0:45'),
];

const ListaAsesoria = () => {
    const classes = useStyles();
    const history = useHistory();

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
                                            {/* Eventualmente, este enlace podría llevar a ver el detalle de la asesoría */}
                                            <Link component="button" variant="body2">
                                                {row.cliente}
                                            </Link>
                                        </TableCell>
                                        <TableCell>{row.fecha}</TableCell>
                                        <TableCell>{row.asesor}</TableCell>
                                        <TableCell>{row.area}</TableCell>
                                        <TableCell>{row.tipo}</TableCell>
                                        <TableCell>{row.tiempo}</TableCell>
                                    </TableRow>
                                ))}
                            </TableBody>
                        </Table>
                    </TableContainer>
                </Paper>
            </main>
        </div>
    );
};

export default ListaAsesoria;