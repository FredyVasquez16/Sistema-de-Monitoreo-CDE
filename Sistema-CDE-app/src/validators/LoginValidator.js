export const validarLogin = (usuario) => {
    const tempErrors = {};

    // Regla para el correo electrónico
    if (!usuario.email) {
        tempErrors.email = "El correo electrónico es obligatorio.";
    } else if (!/\S+@\S+\.\S+/.test(usuario.email)) {
        // Expresión regular simple para validar el formato del email
        tempErrors.email = "El formato del correo electrónico no es válido.";
    }

    // Regla para la contraseña
    if (!usuario.password) {
        tempErrors.password = "La contraseña es obligatoria.";
    }

    return tempErrors;
};