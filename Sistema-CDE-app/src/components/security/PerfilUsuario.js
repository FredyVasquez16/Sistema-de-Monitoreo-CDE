import React, { useEffect, useState } from 'react';
import { Button, Container, Grid, TextField, Typography, Box, CircularProgress, Paper } from "@material-ui/core";
import { makeStyles } from "@material-ui/core/styles";
import { useHistory } from "react-router-dom";
import { actualizarUsuario, obtenerUsuarioActual } from "../../actions/UsuarioAction";
import { useStateValue } from "../../Context/store";

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
    title: {
        marginBottom: theme.spacing(3),
        color: '#033565',
        fontWeight: 'bold',
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
        '& .MuiInputLabel-formControl': {
            transform: 'translate(14px, 14px) scale(1)',
        },
        '& .MuiInputLabel-shrink': {
            transform: 'translate(14px, -6px) scale(0.75)',
        },
        '& .MuiOutlinedInput-input': {
            padding: '12px 14px',
        },
    },
    buttonContainer: {
        marginTop: theme.spacing(3),
    },
    submit: {
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
    cancelButton: {
        padding: theme.spacing(1.2),
        borderRadius: '8px',
        backgroundColor: '#F44336',
        color: 'white',
        fontSize: '1rem',
        fontWeight: 'bold',
        '&:hover': {
            backgroundColor: '#d32f2f',
        }
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

const PerfilUsuario = () => {
    const classes = useStyles();
    const history = useHistory();
    const [{ sesionUsuario }, dispatch] = useStateValue();
    const [loading, setLoading] = useState(false);

    const [usuario, setUsuario] = useState({
        nombreCompleto: '',
        email: '',
        password: '',
        confirmarPassword: '',
        userName: ''
    });

    useEffect(() => {
        if (sesionUsuario && sesionUsuario.autenticado && sesionUsuario.usuario) {
            setUsuario(prev => ({ ...prev, ...sesionUsuario.usuario, password: '', confirmarPassword: '' }));
        } else {
            setLoading(true);
            obtenerUsuarioActual(dispatch)
                .then(response => {
                    if (response && response.data && response.data.data) {
                        setUsuario(prev => ({ ...prev, ...response.data.data, password: '', confirmarPassword: '' }));
                    }
                })
                .catch(error => { console.error("Error al obtener usuario actual:", error); })
                .finally(() => { setLoading(false); });
        }
    }, [dispatch, sesionUsuario]);

    const ingresarValoresMemoria = e => {
        const { name, value } = e.target;
        setUsuario(anterior => ({ ...anterior, [name]: value }));
    };

    const handleVolver = () => {
        history.goBack();
    };

    const guardarUsuario = async (e) => {
        e.preventDefault();
        setLoading(true);
        const usuarioActualizar = {
            nombrecompleto: usuario.nombreCompleto,
            email: usuario.email,
            userName: usuario.userName,
            password: usuario.password
        };
        try {
            const response = await actualizarUsuario(usuarioActualizar);
            if (response.status === 200) {
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: 'Perfil actualizado correctamente.', severity: 'success' }
                });
                setUsuario(prev => ({ ...prev, password: '', confirmarPassword: '' }));
            } else {
                const erroresBackend = response.data?.errors;
                let mensajeError = 'No se pudo guardar la actualización.';
                if (erroresBackend) {
                    const errorKeys = Object.keys(erroresBackend);
                    if (errorKeys.length > 0) {
                        mensajeError = erroresBackend[errorKeys[0]][0];
                    }
                }
                dispatch({
                    type: 'OPEN_SNACKBAR',
                    payload: { open: true, mensaje: mensajeError, severity: 'error' }
                });
            }
        } catch (error) {
            console.error("Error en actualizarUsuario:", error);
            dispatch({
                type: 'OPEN_SNACKBAR',
                payload: { open: true, mensaje: 'Error de conexión o servidor al actualizar el perfil.', severity: 'error' }
            });
        } finally {
            setLoading(false);
        }
    }

    if (loading && !usuario.nombreCompleto) {
        return (
            <Box className={classes.root} style={{ justifyContent: 'center', alignItems: 'center' }}>
                <CircularProgress size={60} style={{ color: 'white' }} />
            </Box>
        );
    }

    return (
        <Box className={classes.root}>
            <Container component="main" maxWidth="md">
                <div className={classes.paper}>
                    <img src="/Logo%20CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    <Typography component="h1" variant="h5" className={classes.title}>
                        Perfil de Usuario
                    </Typography>
                    <form className={classes.form} onSubmit={guardarUsuario}>
                        <Grid container spacing={2}>
                            {/* ... Tus campos de TextField ... */}
                            <Grid item xs={12}>
                                <TextField name="nombreCompleto" value={usuario.nombreCompleto || ''} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Nombre Completo" className={classes.textField} disabled={loading} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="email" value={usuario.email || ''} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Email" className={classes.textField} disabled={loading} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="userName" value={usuario.userName || ''} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Nombre de usuario" className={classes.textField} disabled={loading} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="password" value={usuario.password || ''} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Nueva contraseña (opcional)" className={classes.textField} disabled={loading} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="confirmarPassword" value={usuario.confirmarPassword || ''} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirmar Contraseña" className={classes.textField} disabled={loading} />
                            </Grid>
                        </Grid>

                        <Box
                            className={classes.buttonContainer}
                            display="flex"
                            justifyContent="center"
                            width="100%"
                            mt={3} // Equivalente a marginTop: theme.spacing(3)
                        >
                            <Grid container spacing={2} justifyContent="center" style={{ maxWidth: 400 }}> {/* Limita el ancho máximo para mejor apariencia */}
                                <Grid item xs={6}>
                                    <Button
                                        type="submit"
                                        fullWidth
                                        variant="contained"
                                        size="large"
                                        className={classes.submit}
                                        disabled={loading}
                                    >
                                        Guardar Cambios
                                        {loading && <CircularProgress size={24} className={classes.buttonProgress} />}
                                    </Button>
                                </Grid>
                                <Grid item xs={6}>
                                    <Button
                                        fullWidth
                                        variant="contained"
                                        size="large"
                                        className={classes.cancelButton}
                                        onClick={handleVolver}
                                        disabled={loading}
                                    >
                                        Volver
                                    </Button>
                                </Grid>
                            </Grid>
                        </Box>
                    </form>
                </div>
            </Container>
        </Box>
    );
};

export default PerfilUsuario;