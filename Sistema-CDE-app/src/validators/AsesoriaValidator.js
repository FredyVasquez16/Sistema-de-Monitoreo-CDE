export const validarAsesoria = (formState) => {
    const tempErrors = {};

    // --- Reglas de validación existentes ---
    if (!formState.clienteId) tempErrors.clienteId = "El Cliente es obligatorio.";
    if (!formState.fechaSesion) tempErrors.fechaSesion = "La Fecha de Sesión es obligatoria.";
    if (!formState.tipoContactoId) tempErrors.tipoContactoId = "El Tipo de Contacto es obligatorio.";
    if (!formState.areaAsesoriaId) tempErrors.areaAsesoriaId = "El Área de Asesoría es obligatoria.";
    if (!formState.fuenteFinanciamientoId) tempErrors.fuenteFinanciamientoId = "La Fuente de Financiamiento es obligatoria.";

    // --- NUEVAS REGLAS AÑADIDAS ---
    // Asumimos que el usuario escribirá/seleccionará al menos un valor.
    if (!formState.listaAsesores) {
        tempErrors.listaAsesores = "Debe seleccionar al menos un asesor.";
    }
    if (!formState.listaContactos) {
        tempErrors.listaContactos = "Debe seleccionar al menos un contacto.";
    }

    return tempErrors;
};