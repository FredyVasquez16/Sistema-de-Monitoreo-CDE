// src/components/navegation/bar/MenuDerecha.js

import React from "react";
import { List, ListItem, ListItemText, Link, makeStyles } from "@material-ui/core";

const useStyles = makeStyles((theme) => ({
    drawerContent: {
        backgroundColor: "#033565",   // mismo azul que el menú izquierdo
        color: "#FFFFFF",
        height: "100%",
        minHeight: "100vh",           // ocupa toda la pantalla
        display: "flex",
        flexDirection: "column",
    },
    list: {
        flexGrow: 1,
    },
    listItemText: {
        fontSize: "0.95rem",
        fontWeight: 500,
        color: "rgba(255, 255, 255, 0.7)",   // 👈 mismo color que el izquierdo
    },
}));

export const MenuDerecha = ({ salirSesion, usuario }) => {
    const classes = useStyles();

    return (
        <div className={classes.drawerContent}>
            <List className={classes.list}>
                <ListItem button component={Link}>
                    <ListItemText
                        classes={{ primary: classes.listItemText }}
                        primary={usuario ? usuario.nombreCompleto : ""}
                    />
                </ListItem>
                <ListItem button onClick={salirSesion}>
                    <i className="material-icons" style={{ color: "rgba(255, 255, 255, 0.7)" }}>
                        exit_to_app
                    </i>
                    <ListItemText
                        classes={{ primary: classes.listItemText }}
                        primary="Salir"
                    />
                </ListItem>
            </List>
        </div>
    );
};

export default MenuDerecha;
