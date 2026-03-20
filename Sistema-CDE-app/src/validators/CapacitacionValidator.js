// Este archivo contiene todas las reglas de validación para el formulario de Capacitación.

export const validarCapacitacion = (formState) => {
    let tempErrors = {};

    // Reglas de validación basadas en tu backend

    if (!formState.TipoId) {
        tempErrors.TipoId = "Debe seleccionar un tipo (Capacitación o Evento).";
    }

    if (!formState.Titulo) {
        tempErrors.Titulo = "El título es obligatorio.";
    } else if (formState.Titulo.length > 1000) {
        tempErrors.Titulo = "El título no puede exceder los 1000 caracteres.";
    }

    if (!formState.Titulo) {
        tempErrors.Titulo = "El título es obligatorio.";
    } else if (formState.Titulo.length > 1000) {
        tempErrors.Titulo = "El título no puede exceder los 1000 caracteres.";
    }

    if (!formState.FechaInicio) tempErrors.FechaInicio = "La fecha de inicio es obligatoria.";
    if (!formState.FechaCierre) tempErrors.FechaCierre = "La fecha de cierre es obligatoria.";
    if (!formState.FechaInformes) tempErrors.FechaInformes = "La fecha de informes es obligatoria.";
    if (!formState.HoraProgramada) tempErrors.HoraProgramada = "La hora programada es obligatoria.";
    
    // Para los Select, verificamos que se haya seleccionado un valor (no es el string vacío inicial)
    if (!formState.TemaPrincipalId) tempErrors.TemaPrincipalId = "El tema principal es obligatorio.";
    if (!formState.FormatoProgramaId) tempErrors.FormatoProgramaId = "El formato del programa es obligatorio.";
    
    if (!formState.NumeroSesiones || formState.NumeroSesiones <= 0) tempErrors.NumeroSesiones = "El número de sesiones debe ser mayor que cero.";
    
    if (!formState.Direccion) {
        tempErrors.Direccion = "La dirección es obligatoria.";
    } else if (formState.Direccion.length > 1000) {
        tempErrors.Direccion = "La dirección no puede exceder los 1000 caracteres.";
    }

    if (!formState.Ciudad) {
        tempErrors.Ciudad = "La ciudad es obligatoria.";
    } else if (formState.Ciudad.length > 100) {
        tempErrors.Ciudad = "La ciudad no puede exceder los 100 caracteres.";
    }

    if (!formState.Departamento) {
        tempErrors.Departamento = "El departamento es obligatorio.";
    } else if (formState.Departamento.length > 100) {
        tempErrors.Departamento = "El departamento no puede exceder los 100 caracteres.";
    }

    if (!formState.LugarDesarrollo) {
        tempErrors.LugarDesarrollo = "El lugar de desarrollo es obligatorio.";
    } else if (formState.LugarDesarrollo.length > 200) {
        tempErrors.LugarDesarrollo = "El lugar de desarrollo no puede exceder los 200 caracteres.";
    }

    return tempErrors;
};