// src/components/security/Login.js

import React, { useState } from 'react';
import { Container, TextField, Button, Box, Grid, Link, Typography, CircularProgress } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import { loginUsuario } from "../../actions/UsuarioAction";
import { Link as RouterLink } from 'react-router-dom';
import { validarLogin } from '../../validators/LoginValidator';
import { useStateValue } from '../../Context/store'; // Importamos el dispatch
import { withRouter } from 'react-router-dom';

const useStyles = makeStyles((theme) => ({
    root: {
        background: 'linear-gradient(to bottom, #1e4959, #8a7a2a)',
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        padding: theme.spacing(2),
    },
    paper: {
        backgroundColor: '#f5f5f5',
        padding: theme.spacing(4, 3),
        borderRadius: theme.spacing(1.5),
        boxShadow: '0 4px 12px 0 rgba(0,0,0,0.2)',
        display: 'flex',
        flexDirection: 'column',
        alignItems: 'center',
        width: '100%',
    },
    logo: {
        width: '250px',
        height: 'auto',
        marginBottom: theme.spacing(1),
    },
    form: {
        width: '100%',
    },
    textField: {
        '& .MuiOutlinedInput-root': {
            borderRadius: '8px',
            backgroundColor: '#ffffff',
            '& fieldset': {
                borderColor: '#c0c0c0',
            },
        },
    },
    submit: {
        marginTop: theme.spacing(3),
        padding: theme.spacing(1.2),
        borderRadius: '8px',
        backgroundColor: '#033565',
        color: 'white',
        fontSize: '1rem',
        fontWeight: 'bold',
        position: 'relative',
        '&:hover': {
            backgroundColor: '#355d83',
        }
    },
    linksContainer: {
        marginTop: theme.spacing(2),
    },
    buttonProgress: {
        color: '#8a7a2a',
        position: 'absolute',
        top: '50%',
        left: '50%',
        marginTop: -12,
        marginLeft: -12,
    },
}));

const Login = (props) => { // Añadimos props para history
    const classes = useStyles();
    const [, dispatch] = useStateValue();
    const [usuario, setUsuario] = useState({ email: '', password: '' });
    const [errors, setErrors] = useState({});
    const [loading, setLoading] = useState(false);

    const ingresarValoresMemoria = (e) => {
        const { name, value } = e.target;
        setUsuario(anterior => ({ ...anterior, [name]: value }));
    };

    /*const loginUsuarioBoton = async (e) => {
        e.preventDefault();
        const validationErrors = validarLogin(usuario);
        setErrors(validationErrors);

        // SOLO si no hay errores de validación, procedemos
        if (Object.keys(validationErrors).length === 0) {
            setLoading(true);
            try {
                // LA LLAMADA A LA API AHORA ESTÁ DENTRO DEL TRY
                const response = await loginUsuario(usuario, dispatch);
                console.log("Datos:", response);

                if (response.status === 200) {
                    const { data } = response.data;
                    window.localStorage.setItem("token_seguridad", data.token);

                    dispatch({
                        type: 'INICIAR_SESION',
                        sesion: data,
                        autenticado: true
                    });

                    // Usamos props.history.push para una navegación más limpia en lugar de recargar la página
                    props.history.push("/contactos");

                }
            } catch (error) {
                console.error("Error en el login:", error.response || error);

                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: error.response?.data?.mensaje || 'Credenciales incorrectas o error en el servidor.',
                        severity: 'error'
                    }
                });
            } finally {
                setLoading(false);
            }
        }
    };*/

    const loginUsuarioBoton = async (e) => {
        e.preventDefault();
        const validationErrors = validarLogin(usuario);
        setErrors(validationErrors);

        if (Object.keys(validationErrors).length === 0) {
            setLoading(true);
            try {
                // CAMBIO: Ya no pasamos 'dispatch' a la acción
                const response = await loginUsuario(usuario);

                if (response.status === 200) {
                    const { data } = response.data;
                    window.localStorage.setItem("token_seguridad", data.token);

                    dispatch({
                        type: 'INICIAR_SESION',
                        sesion: data,
                        autenticado: true
                    });

                    props.history.push("/contactos");
                }
            } catch (error) {
                // AHORA ESTE BLOQUE SE EJECUTARÁ CORRECTAMENTE CUANDO LA API DEVUELVA 401
                console.error("Error en el login:", error.response || error);

                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: {
                        open: true,
                        mensaje: error.response?.data?.mensaje || 'Credenciales incorrectas o error en el servidor.',
                        severity: 'error'
                    }
                });
            } finally {
                setLoading(false);
            }
        }
    };

    return (
        <Box className={classes.root}>
            <Container maxWidth="xs">
                <div className={classes.paper}>
                    <img src="/Logo%20CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <form className={classes.form} onSubmit={loginUsuarioBoton}>
                        <TextField
                            name="email" value={usuario.email} onChange={ingresarValoresMemoria} variant="outlined" margin="normal"
                            fullWidth id="email" label="Correo Electrónico" autoComplete="email" autoFocus
                            className={classes.textField} error={!!errors.email} helperText={errors.email} disabled={loading}
                        />
                        <TextField
                            name="password" value={usuario.password} onChange={ingresarValoresMemoria} variant="outlined" margin="normal"
                            fullWidth label="Contraseña" type="password" id="password" autoComplete="current-password"
                            className={classes.textField} error={!!errors.password} helperText={errors.password} disabled={loading}
                        />
                        <Button type="submit" fullWidth variant="contained" className={classes.submit} disabled={loading}>
                            LOGIN
                            {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
                        </Button>
                        <Grid container className={classes.linksContainer}>
                            <Grid item xs>
                                <Link component={RouterLink} to="/auth/forgot_password" variant="body2">
                                    ¿Olvidaste tu contraseña?
                                </Link>
                            </Grid>
                        </Grid>
                    </form>
                </div>
            </Container>
        </Box>
    );
};

export default withRouter(Login);