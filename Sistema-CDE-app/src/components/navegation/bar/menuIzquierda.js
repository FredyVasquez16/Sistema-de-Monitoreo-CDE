// src/components/navegation/bar/menuIzquierda.js

import React from 'react';
import {
    Box,
    Divider,
    List,
    ListItem,
    ListItemIcon,
    ListItemText,
    makeStyles,
    Icon
} from "@material-ui/core";
import { NavLink } from "react-router-dom";

const useStyles = makeStyles((theme) => ({
    drawerContent: {
        backgroundColor: '#033565',
        color: '#FFFFFF',
        height: '100%',        // asegura ocupar todo el drawer
        minHeight: '100vh',    // fuerza a cubrir la ventana completa
        display: 'flex',
        flexDirection: 'column',
    },
    header: {
        padding: theme.spacing(2),
        display: 'flex',
        justifyContent: 'center',
        alignItems: 'center',
        backgroundColor: theme.palette.primary.main,
    },
    logo: {
        width: '180px',
        height: 'auto',
    },
    listContainer: {
        flexGrow: 1,
        overflowY: 'auto',
    },
    divider: {
        backgroundColor: 'rgba(255, 255, 255, 0.12)',
    },
    listItem: {
        paddingTop: theme.spacing(1.5),
        paddingBottom: theme.spacing(1.5),
        paddingLeft: theme.spacing(3),
        '&:hover': {
            backgroundColor: 'rgba(255, 255, 255, 0.08)',
        },
    },
    listItemIcon: {
        minWidth: '40px',
        color: 'rgba(255, 255, 255, 0.7)',
    },
    listItemText: {
        fontSize: '0.95rem',
        fontWeight: 500,
    },
    divider: {
        backgroundColor: 'rgba(255, 255, 255, 0.12)',
    },
    activeListItem: {
        backgroundColor: 'rgba(213, 164, 8, 0.2)',
        borderLeft: `4px solid ${theme.palette.primary.main}`,
        paddingLeft: `calc(${theme.spacing(3)}px - 4px)`,
        '& .MuiListItemIcon-root, & .MuiListItemText-primary': {
            color: theme.palette.primary.main,
            fontWeight: 'bold',
        },
    },
}));

const menuItems = [
    { text: 'Contactos', icon: 'contact_phone', path: '/contactos' },
    { text: 'Cliente / Empresa', icon: 'apartment', path: '/cliente_empresa' },
    { text: 'Asesorías', icon: 'assignment', path: '/asesoria' },
    { text: 'Capacitación / Evento', icon: 'school', path: '/capacitacion_evento' },
    { text: 'Reportes', icon: 'bar_chart', path: '/reporte' },
];

export const MenuIzquierda = () => {
    const classes = useStyles();

    return (
        <div className={classes.drawerContent}>
            <Box className={classes.header}>
                <img src="/Logo CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
            </Box>

            <div className={classes.listContainer}>
                <List>
                    <ListItem
                        component={NavLink}
                        button
                        to="/auth/perfil"
                        className={classes.listItem}
                        activeClassName={classes.activeListItem}
                    >
                        <ListItemIcon className={classes.listItemIcon}><Icon>account_circle</Icon></ListItemIcon>
                        <ListItemText classes={{ primary: classes.listItemText }} primary="Perfil" />
                    </ListItem>
                </List>
                <Divider className={classes.divider} />
                <List>
                    {menuItems.map((item) => (
                        <ListItem
                            key={item.text}
                            component={NavLink}
                            button
                            to={item.path}
                            className={classes.listItem}
                            activeClassName={classes.activeListItem}
                        >
                            <ListItemIcon className={classes.listItemIcon}><Icon>{item.icon}</Icon></ListItemIcon>
                            <ListItemText classes={{ primary: classes.listItemText }} primary={item.text} />
                        </ListItem>
                    ))}
                </List>
            </div>
        </div>
    );
};

export default MenuIzquierda;