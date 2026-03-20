// src/components/pages/VerReporte.js

import React from 'react';
import { AppBar, Toolbar, Typography, Button, Box, Paper, Grid } from '@material-ui/core';
import { makeStyles } from '@material-ui/core/styles';
import { Bar, Doughnut } from 'react-chartjs-2';

const useStyles = makeStyles((theme) => ({
    root: {
        flexGrow: 1,
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
    reportCard: {
        padding: theme.spacing(2),
        backgroundColor: '#FFFFFF',
    },
}));

const asesoriasMesData = {
  labels: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio'],
  datasets: [{
    label: 'Número de Asesorías',
    data: [12, 19, 8, 15, 7, 11, 21],
    backgroundColor: 'rgba(66, 165, 245, 0.6)',
    borderColor: 'rgba(66, 165, 245, 1)',
    borderWidth: 1,
  }],
};

const clientesDepartamentoData = {
    labels: ['Copan', 'Lempira', 'Ocotepeque', 'Santa Bárbara'],
    datasets: [{
        label: '# de Clientes',
        data: [45, 25, 18, 12],
        backgroundColor: ['rgba(255, 159, 64, 0.7)', 'rgba(75, 192, 192, 0.7)', 'rgba(153, 102, 255, 0.7)', 'rgba(255, 99, 132, 0.7)'],
        borderColor: ['rgba(255, 159, 64, 1)', 'rgba(75, 192, 192, 1)', 'rgba(153, 102, 255, 1)', 'rgba(255, 99, 132, 1)'],
        borderWidth: 1,
    }],
};

const contactosGeneroData = {
    labels: ['Masculino', 'Femenino'],
    datasets: [{
        label: '# de Contactos',
        data: [120, 135],
        backgroundColor: ['rgba(54, 162, 235, 0.7)', 'rgba(255, 99, 132, 0.7)'],
        borderColor: ['rgba(54, 162, 235, 1)', 'rgba(255, 99, 132, 1)'],
        borderWidth: 1,
    }],
};

const VerReporte = () => {
    const classes = useStyles();

    const chartOptions = (title) => ({
        responsive: true,
        legend: { position: 'top' },
        title: { display: true, text: title, fontSize: 16 },
    });

    return (
        <div className={classes.root}>
            <AppBar position="static" className={classes.appBar}>
                <Toolbar>
                    <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Box className={classes.navLinks}><Button color="inherit" className={classes.navButton}>Indicadores y Reportes</Button></Box>
                    <Box className={classes.userInfo}><Typography variant="subtitle1" style={{fontWeight: 'bold'}}>Freddy Yoel Vasquez</Typography><Typography variant="body2">CDE MYPIME ROC</Typography></Box>
                </Toolbar>
            </AppBar>

            <main className={classes.content}>
                <Grid container spacing={3}>
                    <Grid item xs={12} lg={6}>
                        <Paper className={classes.reportCard}>
                            <Bar options={chartOptions('Asesorías Realizadas por Mes')} data={asesoriasMesData} />
                        </Paper>
                    </Grid>
                    <Grid item xs={12} md={6} lg={3}>
                        <Paper className={classes.reportCard}>
                            <Doughnut options={chartOptions('Distribución de Clientes por Departamento')} data={clientesDepartamentoData} />
                        </Paper>
                    </Grid>
                    <Grid item xs={12} md={6} lg={3}>
                        <Paper className={classes.reportCard}>
                            <Doughnut options={chartOptions('Distribución de Contactos por Género')} data={contactosGeneroData} />
                        </Paper>
                    </Grid>
                </Grid>
            </main>
        </div>
    );
};

export default VerReporte;