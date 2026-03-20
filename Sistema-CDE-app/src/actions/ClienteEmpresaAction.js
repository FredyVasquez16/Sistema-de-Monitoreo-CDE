import HttpCliente from '../services/HttpCliente';

export const guardarClienteEmpresa = (objetoClienteEmpresa) => {
    return HttpCliente.post('/ClienteEmpresa', objetoClienteEmpresa);
}

export const obtenerTiposClientesNivel = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposClientesNivel');
}

export const obtenerServiciosSolicitados = async () => {
    return HttpCliente.get('/ClienteEmpresa/ServiciosSolicitados');
}

export const obtenerTiposClientesEstado = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposClientesEstado');
}

export const obtenerTiposOrganizacion = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposOrganizacion');
}

export const obtenerTiposEmpresa = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposEmpresa');
}

export const obtenerTamanosEmpresa = async () => {
    return HttpCliente.get('/ClienteEmpresa/TamanoEmpresa');
}

export const obtenerTipoContabilidades = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposContabilidad');
}

export const obtenerNivelesFormalizacion = async () => {
    return HttpCliente.get('/ClienteEmpresa/NivelesFormalizacion');
}

export const obtenerTiposComercioInternacional = async () => {
    return HttpCliente.get('/ClienteEmpresa/TiposComercioInternacional');
}

export const obtenerFuentesFinanciamiento = async () => {
    return HttpCliente.get('/ClienteEmpresa/FuentesFinanciamiento');
}

export const obtenerClientesEmpresas = async () => {
    const response = await HttpCliente.get('/ClienteEmpresa');
    return response.data.data; // Ajusta si tu backend retorna en otra propiedad
}

export const obtenerClienteEmpresaPorId = async (id) => {
    const response = await HttpCliente.get(`/ClienteEmpresa/${id}`);
    return response.data.data; // Ajusta según la estructura de tu backend
}

export const actualizarClienteEmpresa = async (id, objetoClienteEmpresa) => {
    const response = await HttpCliente.put(`/ClienteEmpresa/${id}`, objetoClienteEmpresa);
    return response; // Puedes retornar response.data si tu backend lo retorna así
}

export const eliminarClienteEmpresa = async (id) => {
    const response = await HttpCliente.delete(`/ClienteEmpresa/${id}`);
    return response; // Puedes retornar response.data si tu backend lo retorna así
}

export const buscarAsesoresPorTermino = async (termino) => {
    if (!termino || termino.trim() === '') {
        return [];
    }
    try {
        const response = await HttpCliente.get(`/ClienteEmpresa/BuscarAsesores?termino=${termino}`);
        return response.data.data;
    } catch (error) {
        console.error('Error buscando asesores:', error);
        return [];
    }
};

// Función para buscar clientes/empresas por término
export const buscarClientesEmpresasPorTermino = async (termino) => {
    if (!termino || termino.trim() === '') {
        return [];
    }
    try {
        const response = await HttpCliente.get(`/ClienteEmpresa/buscar?termino=${termino}`);
        return response.data.data;
    } catch (error) {
        console.error('Error buscando clientes/empresas:', error);
        return [];
    }
};

// Función para obtener clientes/empresas con filtros
export const obtenerClientesEmpresasFiltrados = async (filtros = {}) => {
    try {
        const params = new URLSearchParams();
        
        if (filtros.termino) params.append('termino', filtros.termino);
        if (filtros.nivel) params.append('nivel', filtros.nivel);
        if (filtros.estado) params.append('estado', filtros.estado);
        if (filtros.tipoOrganizacion) params.append('tipoOrganizacion', filtros.tipoOrganizacion);
        if (filtros.tipoEmpresa) params.append('tipoEmpresa', filtros.tipoEmpresa);
        if (filtros.tamano) params.append('tamano', filtros.tamano);
        if (filtros.servicio) params.append('servicio', filtros.servicio);
        if (filtros.departamento) params.append('departamento', filtros.departamento);
        
        const queryString = params.toString();
        const url = queryString ? `/ClienteEmpresa/filtrar?${queryString}` : '/ClienteEmpresa';
        
        const response = await HttpCliente.get(url);
        return response.data.data;
    } catch (error) {
        console.error("Error al obtener clientes/empresas filtrados:", error);
        return [];
    }
};