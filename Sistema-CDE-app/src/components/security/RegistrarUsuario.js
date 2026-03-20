import React, {useState} from 'react';
import { Container, TextField, Button, Box, Grid, Typography } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import {registrarUsuario} from "../../actions/UsuarioAction";

// Usamos exactamente los mismos estilos que en el Login para mantener la consistencia.
const useStyles = makeStyles((theme) => ({
    // Estilo para el contenedor principal que ocupa toda la pantalla
    root: {
        background: 'linear-gradient(to bottom, #1e4959, #8a7a2a)',
        minHeight: '100vh',
        display: 'flex',
        alignItems: 'center',
        justifyContent: 'center',
        padding: theme.spacing(2), // Agrega padding para que el 'paper' no se pegue a los bordes en pantallas pequeñas
    },
    // Contenedor blanco del formulario
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
    // Estilo para el logo
    logo: {
        width: '250px',
        height: 'auto',
        marginBottom: theme.spacing(1),
    },
    // Estilo para el título "Registrar Usuario"
    title: {
        marginBottom: theme.spacing(3),
    },
    form: {
        width: '100%',
    },
    // Estilo para los campos de texto (copiado de tu Login.js)
    textField: {
        '& .MuiOutlinedInput-root': {
            borderRadius: '8px',
            backgroundColor: '#ffffff',
            '& fieldset': {
                borderColor: '#c0c0c0',
            },
        },
        '& .MuiInputLabel-formControl':{
            transform: 'translate(14px, 14px) scale(1)',
        },
        '& .MuiInputLabel-shrink': {
            transform: 'translate(14px, -6px) scale(0.75)',
        },
        '& .MuiOutlinedInput-input': {
            padding: '12px 14px',
        },
    },
    // Estilo para el botón (copiado de tu Login.js)
    submit: {
        marginTop: theme.spacing(3),
        padding: theme.spacing(1.2),
        borderRadius: '8px',
        backgroundColor: '#033565',
        color: 'white',
        fontSize: '1rem',
        fontWeight: 'bold',
        '&:hover': {
            backgroundColor: '#355d83',
        }
    },
}));

const RegistrarUsuario = () => {
    // Instanciamos los estilos
    const classes = useStyles();

    const [usuario, setUsuario] = useState({
        Nombre: '',
        Apellido: '',
        Email: '',
        UserName: '',
        Password: '',
        ConfirmacionPassword: ''
    })

    const ingresarValoresMemoria = e =>{
        const { name, value } = e.target;
        setUsuario(anterior => ({
            ...anterior,
            [name]: value
        }))
    }

    const registrarUsuarioBoton = e => { //evento para el boton
        e.preventDefault();

        registrarUsuario(usuario).then(response => {
            if (response.status === 200 || response.status === 201) {
                alert("Usuario registrado exitosamente");
            } else {
                alert("Error al registrar el usuario");
            }
            console.log('se registro exitosamente el usuario', response);
            window.localStorage.setItem("token_seguridad", response.data.data.token);
        });
    }

    /*const registrarUsuarioBoton = async (e) => {
        e.preventDefault();
        //console.log('Datos del usuario a registrar:', usuario);
        try {
            const data = await registrarUsuario(usuario);
            console.log('Respuesta directa del servidor:', data);

            const token = data.data.token;

            if (token) {
                window.localStorage.setItem("token_seguridad", token);
                alert("¡Éxito! Usuario registrado correctamente.");
            }

        } catch (error) {
            console.error("Falló el registro:", error);
            alert("Ocurrió un error al registrar el usuario.");
        }
    }*/

    return (
        <Box className={classes.root}>

            <Container component="main" maxWidth="md">
                <div className={classes.paper}>

                    <img src="/Logo%20CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />

                    <Typography component="h1" variant="h5" className={classes.title}>
                        Registro de Usuario
                    </Typography>

                    <form className={classes.form} noValidate>
                        <Grid container spacing={2}>
                            <Grid item xs={12} md={6}>
                                <TextField name="Nombre" value={usuario.Nombre} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su nombre" className={classes.textField} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="Apellido" value={usuario.Apellido} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su apellido" className={classes.textField} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="Email" value={usuario.Email} onChange={ingresarValoresMemoria} type="email" variant="outlined" fullWidth label="Ingrese su e-mail" className={classes.textField} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="UserName" value={usuario.UserName} onChange={ingresarValoresMemoria} variant="outlined" fullWidth label="Ingrese su nombre de usuario" className={classes.textField} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="Password" value={usuario.Password} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Ingrese su contraseña" className={classes.textField} />
                            </Grid>
                            <Grid item xs={12} md={6}>
                                <TextField name="ConfirmacionPassword" value={usuario.ConfirmacionPassword} onChange={ingresarValoresMemoria} type="password" variant="outlined" fullWidth label="Confirme su contraseña" className={classes.textField} />
                            </Grid>
                        </Grid>
                        <Grid container justify="center">
                            <Grid item xs={12} md={6}>
                                <Button type="submit" onClick={registrarUsuarioBoton} fullWidth variant="contained" size="large" className={classes.submit}>
                                    Enviar
                                </Button>
                            </Grid>
                        </Grid>
                    </form>
                </div>
            </Container>
        </Box>
    );
};

export default RegistrarUsuario;