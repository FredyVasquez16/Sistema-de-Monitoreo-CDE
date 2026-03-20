import React, { useEffect, useState } from 'react';
import { MuiThemeProvider, CircularProgress, Box } from "@material-ui/core";
import theme from "./theme/theme";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import { Snackbar } from "@material-ui/core";
import MuiAlert from '@material-ui/lab/Alert';
import { useStateValue } from "./Context/store";
import { obtenerUsuarioActual } from "./actions/UsuarioAction";

// Importa el componente de rutas protegidas
import RutasProtegidas from "./components/navegation/RutasProtegidas";

// --- Importaciones de todos tus Componentes de vistas ---
import RegistrarUsuario from "./components/security/RegistrarUsuario";
import Login from "./components/security/Login";
import PerfilUsuario from "./components/security/PerfilUsuario";
import ForgotPassword from './components/security/ForgotPassword';
import ListaContactos from "./components/contactos/ListaContactos";
import VerContacto from './components/contactos/VerContacto';
import NuevoContacto from './components/contactos/NuevoContacto';
import NuevoClienteEmpresa from './components/cliente_empresa/NuevoClienteEmpresa';
import VerClienteEmpresa from './components/cliente_empresa/VerClienteEmpresa';
import ListaClienteEmpresa from './components/cliente_empresa/ListaClienteEmpresa';
import NuevaAsesoria from './components/asesorias/NuevaAsesoria';
import ListaAsesoria from './components/asesorias/ListaAsesoria';
import VerAsesoria from './components/asesorias/VerAsesoria';
import VerReporte from './components/reportes/VerReporte';
import NuevoCapacitacionEvento from './components/capacitacion_evento/NuevoCapacitacionEvento';
import ListaCapacitacionEvento from './components/capacitacion_evento/ListaCapacitacionEvento';
import VerCapacitacionEvento from './components/capacitacion_evento/VerCapacitacionEvento';
import NuevoSesion from './components/capacitacion_evento/NuevoSesion';
import EditarContacto from './components/contactos/EditarContacto';
import EditarClienteEmpresa from './components/cliente_empresa/EditarClienteEmpresa';

function App() {
    const [{ sesionUsuario, openSnackbar }, dispatch] = useStateValue();
    const [iniciaApp, setIniciaApp] = useState(false);

    useEffect(() => {
        const verificarSesion = async () => {
            try {
                // La acción se encarga de hacer el dispatch si el token es válido
                await obtenerUsuarioActual(dispatch);
            } catch (error) {
                // Si hay un error (ej. token inválido), la acción ya limpió la sesión.
                console.error("No se pudo verificar la sesión al iniciar la app:", error);
            } finally {
                // Una vez verificado, permitimos que la app se renderice
                setIniciaApp(true);
            }
        };

        verificarSesion();
    }, [dispatch]);

    const handleClose = (event, reason) => {
        if (reason === 'clickaway') {
            return;
        }
        dispatch({
            type: 'OPEN_SNACKBAR',
            payload: {
                open: false,
                mensaje: '',
                severity: 'info'
            }
        });
    };

    // Mientras se verifica la sesión, mostramos un indicador de carga a pantalla completa
    if (!iniciaApp) {
        return (
            <Box display="flex" justifyContent="center" alignItems="center" minHeight="100vh" bgcolor="#033565">
                <CircularProgress style={{ color: '#D5A408' }} />
            </Box>
        );
    }

    return (
        <React.Fragment>
            <Snackbar
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
                open={openSnackbar ? openSnackbar.open : false}
                autoHideDuration={4000}
                onClose={handleClose}
            >
                <MuiAlert
                    elevation={6}
                    variant="filled"
                    onClose={handleClose}
                    severity={openSnackbar ? openSnackbar.severity : 'info'}
                >
                    {openSnackbar ? openSnackbar.mensaje : ''}
                </MuiAlert>
            </Snackbar>

            <Router>
                <MuiThemeProvider theme={theme}>
                    <Switch>
                        {/* --- Rutas Públicas --- */}
                        <Route path="/auth/login" exact component={Login} />
                        <Route path="/auth/registrar" exact component={RegistrarUsuario} />
                        <Route path="/auth/forgot_password" exact component={ForgotPassword} />

                        {/* --- Rutas Protegidas --- */}
                        <RutasProtegidas path="/auth/perfil" exact component={PerfilUsuario} />
                        <RutasProtegidas path="/contactos" exact component={ListaContactos} />
                        <RutasProtegidas path="/contactos/ver/:id" exact component={VerContacto} />
                        <RutasProtegidas path="/contactos/editar/:id" exact component={EditarContacto} />
                        <RutasProtegidas path="/contactos/nuevo" exact component={NuevoContacto} />

                        <RutasProtegidas path="/cliente_empresa/nuevo" exact component={NuevoClienteEmpresa} />
                        <RutasProtegidas path="/cliente_empresa/ver/:id" exact component={VerClienteEmpresa} />
                        <RutasProtegidas path="/cliente_empresa/editar/:id" exact component={EditarClienteEmpresa} />
                        <RutasProtegidas path="/cliente_empresa" exact component={ListaClienteEmpresa} />
                        <RutasProtegidas path="/asesoria/nuevo" exact component={NuevaAsesoria} />
                        <RutasProtegidas path="/asesoria" exact component={ListaAsesoria} />
                        <RutasProtegidas path="/asesoria/ver" exact component={VerAsesoria} />
                        <RutasProtegidas path="/reporte" exact component={VerReporte} />
                        <RutasProtegidas path="/capacitacion_evento/nuevo" exact component={NuevoCapacitacionEvento} />
                        <RutasProtegidas path="/capacitacion_evento" exact component={ListaCapacitacionEvento} />
                        <RutasProtegidas path="/capacitacion_evento/ver" exact component={VerCapacitacionEvento} />
                        <RutasProtegidas path="/capacitacion_evento/sesion/nueva" exact component={NuevoSesion} />

                        {/* --- Ruta por Defecto --- */}
                        <Route path="/" exact component={Login} />
                    </Switch>
                </MuiThemeProvider>
            </Router>
        </React.Fragment>
    );
}

export default App;