export const validarContacto = (formState) => {
    const tempErrors = {};
    const emailRegex = /\S+@\S+\.\S+/;
    const phoneRegex = /^[0-9]{8,}$/; // Al menos 8 dígitos numéricos
    const dniRegex = /^[0-9]{13}$/; // Asumiendo formato DNI de Honduras sin guiones
    const rtnRegex = /^[0-9]{14}$/; // Asumiendo formato RTN de Honduras sin guiones


    if (!formState.nombre) {
        tempErrors.nombre = "El campo Nombre es obligatorio.";
    } else if (formState.nombre.length > 100) {
        tempErrors.nombre = "El campo Nombre no puede exceder los 100 caracteres.";
    }

    if (!formState.apellidos) {
        tempErrors.apellidos = "El campo Apellido es obligatorio.";
    } else if (formState.apellidos.length > 100) {
        tempErrors.apellidos = "El campo Apellido no puede exceder los 100 caracteres.";
    }

    if (!formState.fechaNacimiento) {
        tempErrors.fechaNacimiento = "El campo Fecha de Nacimiento es obligatorio.";
    }

    if (!formState.dni) {
        tempErrors.dni = "El campo DNI es obligatorio.";
    } else if (!dniRegex.test(formState.dni)) {
        tempErrors.dni = "El DNI debe tener 13 dígitos numéricos.";
    } else if (formState.dni.length > 20) {
        tempErrors.dni = "El campo DNI no puede exceder los 20 caracteres.";
    }

    if (!formState.nacionalidad) {
        tempErrors.nacionalidad = "El campo Nacionalidad es obligatorio.";
    } else if (formState.nacionalidad.length > 70) {
        tempErrors.nacionalidad = "La Nacionalidad no puede exceder los 70 caracteres.";
    }

    if (!formState.genero) {
        tempErrors.genero = "El campo Género es obligatorio.";
    }

    if (!formState.numeroTelefono) {
        tempErrors.numeroTelefono = "El campo Teléfono es obligatorio.";
    } else if (!phoneRegex.test(formState.numeroTelefono)) {
        tempErrors.numeroTelefono = "El teléfono debe tener al menos 8 dígitos.";
    }

    if (!formState.correoElectronico) {
        tempErrors.correoElectronico = "El campo Correo es obligatorio.";
    } else if (!emailRegex.test(formState.correoElectronico)) {
        tempErrors.correoElectronico = "El campo Correo debe ser un correo electrónico válido.";
    } else if (formState.correoElectronico.length > 100) {
        tempErrors.correoElectronico = "El Correo no puede exceder los 100 caracteres.";
    }

    if (!formState.rtn) {
        tempErrors.rtn = "El campo RTN es obligatorio.";
    } else if (!rtnRegex.test(formState.rtn)) {
        tempErrors.rtn = "El RTN debe tener 14 dígitos numéricos.";
    } else if (formState.rtn.length > 20) {
        tempErrors.rtn = "El campo RTN no puede exceder los 20 caracteres.";
    }

    if (!formState.direccion) {
        tempErrors.direccion = "El campo Dirección es obligatorio.";
    } else if (formState.direccion.length > 1000) {
        tempErrors.direccion = "La Dirección no puede exceder los 1000 caracteres.";
    }

    if (!formState.municipio) {
        tempErrors.municipio = "El campo municipio es obligatorio.";
    }

    if (!formState.departamento) {
        tempErrors.departamento = "El campo Departamento es obligatorio.";
    }

    if (!formState.cargo) {
        tempErrors.cargo = "El campo Cargo es obligatorio.";
    } else if (formState.cargo.length > 100) {
        tempErrors.cargo = "El Cargo no puede exceder los 100 caracteres.";
    }

    if (!formState.nivelEstudio) {
        tempErrors.nivelEstudio = "Debe seleccionar un Nivel de Estudio válido.";
    }

    return tempErrors;
};