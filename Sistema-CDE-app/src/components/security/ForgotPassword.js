import React, { useState } from 'react';
import { Container, TextField, Button, Box, Typography, Link, Grid } from "@material-ui/core";
import { makeStyles } from '@material-ui/core/styles';
import { Link as RouterLink } from 'react-router-dom';
// 1. Importa la nueva función de validación
import { validarForgotPassword } from '../../validators/ForgotPasswordValidator';

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
        marginBottom: theme.spacing(2),
    },
    title: {
        marginBottom: theme.spacing(1),
    },
    subtitle: {
        marginBottom: theme.spacing(3),
        textAlign: 'center',
        color: theme.palette.text.secondary,
    },
    form: {
        width: '100%',
    },
    textField: {
        '& .MuiOutlinedInput-root': {
            borderRadius: '8px',
            backgroundColor: '#ffffff',
            '& fieldset': { borderColor: '#c0c0c0' },
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
        '&:hover': {
            backgroundColor: '#355d83',
        }
    },
    backLink: {
        marginTop: theme.spacing(2),
    }
}));

const ForgotPassword = () => {
    const classes = useStyles();
    const [email, setEmail] = useState('');
    // 2. Añade un estado para los errores
    const [errors, setErrors] = useState({});

    const handleSubmit = async (e) => {
        e.preventDefault();
        // 3. Llama a la función de validación
        const validationErrors = validarForgotPassword(email);
        setErrors(validationErrors);

        // Si no hay errores, procede a la lógica de envío
        if (Object.keys(validationErrors).length === 0) {
            try {
                // Lógica para llamar a la acción del backend
                alert(`Si existe una cuenta asociada a ${email}, se ha enviado un correo con instrucciones para restablecer la contraseña.`);
            } catch (error) {
                console.error("Error al solicitar recuperación:", error);
                alert("Ocurrió un error. Por favor, intente de nuevo más tarde.");
            }
        }
    };

    return (
        <Box className={classes.root}>
            <Container maxWidth="xs">
                <div className={classes.paper}>
                    <img src="/Logo%20CDE.png" alt="Logo CDE MIPYME" className={classes.logo} />
                    
                    <Typography component="h1" variant="h5" className={classes.title}>
                        Recuperar Contraseña
                    </Typography>
                    <Typography component="p" variant="body2" className={classes.subtitle}>
                        Ingresa tu correo electrónico y te enviaremos un enlace para restablecer tu contraseña.
                    </Typography>

                    <form className={classes.form} onSubmit={handleSubmit}>
                        <TextField
                            name="email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            variant="outlined"
                            margin="normal"
                            // required <-- SE HA ELIMINADO
                            fullWidth
                            id="email"
                            label="Correo Electrónico"
                            autoComplete="email"
                            autoFocus
                            className={classes.textField}
                            // 4. Aplica las props de error
                            error={!!errors.email}
                            helperText={errors.email}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            className={classes.submit}
                        >
                            Enviar Enlace
                        </Button>
                        <Grid container justify="center" className={classes.backLink}>
                            <Grid item>
                                <Link component={RouterLink} to="/auth/login" variant="body2">
                                    Volver al Login
                                </Link>
                            </Grid>
                        </Grid>
                    </form>
                </div>
            </Container>
        </Box>
    );
};

export default ForgotPassword;