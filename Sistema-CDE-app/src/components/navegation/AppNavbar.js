import React from 'react';
import { AppBar } from "@material-ui/core";
import BarSesion from "./bar/BarSesion";
import { useStateValue } from '../../Context/store';

const AppNavbar = () => {
    const [{ sesionUsuario }, dispatch] = useStateValue();

    return sesionUsuario
        ? (sesionUsuario.autenticado === true ? <AppBar position='static'><BarSesion /></AppBar> : null)
        : null;

    /*return (
        <AppBar>
            <BarSesion/>
        </AppBar>
    );*/
};

export default AppNavbar;