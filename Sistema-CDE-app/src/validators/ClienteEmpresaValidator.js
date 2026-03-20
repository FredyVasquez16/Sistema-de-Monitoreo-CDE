export const validarClienteEmpresa = (formState) => {
    const tempErrors = {};
    const emailRegex = /\S+@\S+\.\S+/;
    const phoneRegex = /^[0-9]{8,}$/; // Expresión regular para al menos 8 dígitos numéricos

    // --- Sección 1: Información Principal ---
    if (!formState.nombreCliente) tempErrors.nombreCliente = "El nombre es obligatorio.";
    if (formState.nombreCliente && formState.nombreCliente.length > 100) tempErrors.nombreCliente = "El nombre no puede exceder los 100 caracteres.";

    if (!formState.contactoPrimarioId) tempErrors.contactoPrimarioId = "Debe seleccionar un Contacto Primario.";
    if (!formState.asesorPrincipalId) tempErrors.asesorPrincipalId = "Debe seleccionar un Asesor Principal.";
    if (!formState.nombrePropietarioId) tempErrors.nombrePropietarioId = "Debe seleccionar el Nombre del Propietario.";

    if (!formState.nivelCliente) tempErrors.nivelCliente = "El nivel del tipo de cliente es obligatorio.";
    //if (!formState.contactoPrimario) tempErrors.contactoPrimario = "El contacto primario es obligatorio.";
    if (!formState.estadoCliente) tempErrors.estadoCliente = "El estado del tipo de cliente es obligatorio.";
    //if (!formState.asesorPrincipal) tempErrors.asesorPrincipal = "El usuario (asesor) es obligatorio.";
    if (!formState.serviciosSolicitados) tempErrors.serviciosSolicitados = "El servicio solicitado es obligatorio.";

    // --- Sección 2: Dirección ---
    if (!formState.direccionFisica) tempErrors.direccionFisica = "La dirección física es obligatoria.";
    if (formState.direccionFisica && formState.direccionFisica.length > 1000) tempErrors.direccionFisica = "La dirección física no puede exceder los 1000 caracteres.";

    if (!formState.municipio) tempErrors.municipio = "El municipio es obligatorio.";
    if (formState.municipio && formState.municipio.length > 100) tempErrors.municipio = "El municipio no puede exceder los 100 caracteres.";

    if (!formState.departamento) tempErrors.departamento = "El departamento es obligatorio.";
    if (formState.departamento && formState.departamento.length > 100) tempErrors.departamento = "El departamento no puede exceder los 100 caracteres.";
    // --- Sección: Fuentes de Financiamiento ---
    if (!formState.fuenteFinanciamiento) tempErrors.fuenteFinanciamiento = "La fuente de financiamiento es obligatoria.";

    // --- Sección 3: Contacto y Fecha ---
    if (!formState.numeroTelefono) {
        tempErrors.numeroTelefono = "El teléfono es obligatorio.";
    } else if (!phoneRegex.test(formState.numeroTelefono)) {
        tempErrors.numeroTelefono = "El teléfono debe tener al menos 8 dígitos.";
    }

    if (!formState.correoElectronico) {
        tempErrors.correoElectronico = "El correo es obligatorio.";
    } else if (!emailRegex.test(formState.correoElectronico)) {
        tempErrors.correoElectronico = "El formato del correo electrónico no es válido.";
    } else if (formState.correoElectronico.length > 100) {
        tempErrors.correoElectronico = "El correo no puede exceder los 100 caracteres.";
    }

    if (!formState.fechaInicio) tempErrors.fechaInicio = "La fecha de inicio es obligatoria.";
    if (!formState.fechaIngresos) tempErrors.fechaIngresos = "La fecha de ingresos brutos es obligatoria.";
    if (!formState.fechaGanancias) tempErrors.fechaGanancias = "La fecha de ganancias/pérdidas es obligatoria.";
    if (!formState.fechaEstablecimiento) tempErrors.fechaEstablecimiento = "La fecha de establecimiento es obligatoria.";

    // --- Sección 4: Detalles de la Empresa ---
    if (!formState.tipoOrganizacion) tempErrors.tipoOrganizacion = "El tipo de organización es obligatorio.";
    if (!formState.tipoEmpresa) tempErrors.tipoEmpresa = "El tipo de empresa es obligatorio.";
    if (!formState.tamanoEmpresa) tempErrors.tamanoEmpresa = "El tamaño de la empresa es obligatorio.";
    if (!formState.tipoContabilidad) tempErrors.tipoContabilidad = "El tipo de contabilidad es obligatorio.";
    if (!formState.nivelFormalizacion) tempErrors.nivelFormalizacion = "El nivel de formalización es obligatorio.";
    //if (!formState.nombrePropietario) tempErrors.nombrePropietario = "El nombre del propietario es obligatorio.";
    if (!formState.generoPropietario) tempErrors.generoPropietario = "El género del propietario es obligatorio.";

    // --- Sección 5: Empleados y Operaciones ---
    if (formState.empleadosCompleto === '' || formState.empleadosCompleto < 0) tempErrors.empleadosCompleto = "El número de empleados a tiempo completo es obligatorio.";

    // --- Sección 6: Información Financiera ---
    if (formState.ingresosAnuales === '' || formState.ingresosAnuales < 0) tempErrors.ingresosAnuales = "Los ingresos brutos anuales son obligatorios.";

    // --- Sección 7: Detalles del Negocio ---
    if (!formState.descripcionProducto) tempErrors.descripcionProducto = "La descripción del producto o servicio es obligatoria.";
    if (formState.descripcionProducto && formState.descripcionProducto.length > 2000) tempErrors.descripcionProducto = "La descripción no puede exceder los 2000 caracteres.";

    if (!formState.estatusInicial) tempErrors.estatusInicial = "El estatus inicial es obligatorio.";
    if (formState.estatusInicial && formState.estatusInicial.length > 200) tempErrors.estatusInicial = "El estatus inicial no puede exceder los 200 caracteres.";

    if (!formState.estatusActual) tempErrors.estatusActual = "El estatus actual es obligatorio.";
    if (formState.estatusActual && formState.estatusActual.length > 200) tempErrors.estatusActual = "El estatus actual no puede exceder los 200 caracteres.";

    return tempErrors;
};