export const validarForgotPassword = (email) => {
const tempErrors = {};
if (!email) {
    tempErrors.email = "El correo electrónico es obligatorio.";
} else if (!/\S+@\S+\.\S+/.test(email)) {
    tempErrors.email = "El formato del correo electrónico no es válido.";
}

return tempErrors;
};