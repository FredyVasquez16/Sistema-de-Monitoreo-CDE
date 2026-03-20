export const validarSesion = (formState) => {
    const tempErrors = {};

    // Reglas para los campos obligatorios
    if (!formState.titulo) {
        tempErrors.titulo = "El título de la sesión es obligatorio.";
    }

    if (!formState.fechaInicio) {
        tempErrors.fechaInicio = "La fecha de inicio es obligatoria.";
    }

    if (!formState.horaInicio) {
        tempErrors.horaInicio = "La hora de inicio es obligatoria.";
    }
    
    if (!formState.fechaFinal) {
        tempErrors.fechaFinal = "La fecha de finalización es obligatoria.";
    }
    
    if (!formState.horaFinal) {
        tempErrors.horaFinal = "La hora de finalización es obligatoria.";
    }

    return tempErrors;
};