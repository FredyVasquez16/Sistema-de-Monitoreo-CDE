// src/components/navegation/bar/BarSesion.js

import React, { useState } from 'react';
import { Avatar, Button, Drawer, IconButton, Toolbar, Typography } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import FotoUsuarioTemp from "../../../logo.svg";
import { useStateValue } from "../../../Context/store";
import MenuIzquierda from './menuIzquierda';
import { withRouter } from 'react-router-dom';
import MenuDerecha from "./menuDerecha";

const useStyles = makeStyles((theme) => ({
    seccionDesktop: {
        display: 'none',
        [theme.breakpoints.up('md')]: {
            display: 'flex',
            alignItems: 'center', // Alinea los items verticalmente
        }
    },
    seccionMobile: {
        display: 'flex',
        [theme.breakpoints.up('md')]: {
            display: 'none',
        }
    },
    grow: {
        flexGrow: 1
    },
    avatarSize: {
        width: 40,
        height: 40,
    },
    list: {
        width: 250, // Un ancho más estándar para el menú
    },
    listItemText: {
        fontWeight: 600,
        fontSize: '14px',
        paddingLeft: 15,
        color: '#000000',
    }
}));

const BarSesion = (props) => {
    const classes = useStyles();
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const [abrirMenuIzquierda, setAbrirMenuIzquierda] = useState(false);
    const [abrirMenuDerecha, setAbrirMenuDerecha] = useState(false);

    const cerrarMenuIzquierda = () => {
        setAbrirMenuIzquierda(false);
    };

    const abrirMenuIzquierdaAction = () => {
        setAbrirMenuIzquierda(true);
    };

    const cerrarMenuDerecha = () => {
        setAbrirMenuDerecha(false);
    };

    const abrirMenuDerechaAction = () => {
        setAbrirMenuDerecha(true);
    };

    const salirSesionApp = () => {
        // Limpiamos el token y el estado global
        window.localStorage.removeItem("token_seguridad");
        dispatch({
            type: "SALIR_SESION",
            payload: {
                nuevoUsuario: null,
                autenticado: false,
            }
        });
        props.history.push("/auth/login"); // Redirigimos al login
    };

    return (
        <React.Fragment>
            <Drawer open={abrirMenuIzquierda} onClose={cerrarMenuIzquierda} anchor="left">
                <div className={classes.list} role="presentation" onKeyDown={cerrarMenuIzquierda} onClick={cerrarMenuIzquierda}>
                    <MenuIzquierda classes={classes} />
                </div>
            </Drawer>

            <Drawer open={abrirMenuDerecha} onClose={cerrarMenuDerecha} anchor="right">
                <div role='button' onKeyDown={cerrarMenuDerecha} onClick={cerrarMenuDerecha}>
                    <MenuDerecha
                        classes={classes}
                        salirSesion={salirSesionApp}
                        usuario={sesionUsuario?.usuario} // Usamos optional chaining para seguridad
                    />
                </div>
            </Drawer>

            <Toolbar>
                <IconButton color="inherit" onClick={abrirMenuIzquierdaAction}>
                    <i className="material-icons">menu</i>
                </IconButton>
                <Typography variant="h6">Sistema de Monitoreo CDE</Typography>
                <div className={classes.grow}></div>

                {/* --- SECCIÓN DE ESCRITORIO MEJORADA --- */}
                <div className={classes.seccionDesktop}>
                    <Button color="inherit">
                        {sesionUsuario?.usuario?.nombreCompleto || ""}
                    </Button>
                    <Button color="inherit" onClick={salirSesionApp}>
                        Salir
                    </Button>
                    <Avatar
                        src={sesionUsuario?.usuario?.imagen || FotoUsuarioTemp}
                        className={classes.avatarSize}
                    />
                </div>

                <div className={classes.seccionMobile}>
                    <IconButton color="inherit" onClick={abrirMenuDerechaAction}>
                        <i className="material-icons">more_vert</i>
                    </IconButton>
                </div>
            </Toolbar>
        </React.Fragment>
    );
};

export default withRouter(BarSesion);