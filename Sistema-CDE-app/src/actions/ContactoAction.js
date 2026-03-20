import HttpCliente from '../services/HttpCliente';

// funcion para que guarde el contacto
export const guardarContacto = async (objetoContacto) => {
    const response = await HttpCliente.post('/Contacto', objetoContacto);
    return response; // Retornamos toda la respuesta para manejar status y data en el componente
};

export const obtenerEstadosCiviles = async () => {
    const response = await HttpCliente.get('/Contacto/EstadosCiviles');
    return response.data.data; // Asegúrate de que el backend retorna un array
};

export const obtenerNivelesEstudio = async () => {
    const response = await HttpCliente.get('/Contacto/NivelesEstudio');
    return response.data.data; // Asegúrate de que el backend retorna un array
}

export const obtenerCategoriasLaborales = async () => {
    const response = await HttpCliente.get('/Contacto/CategoriasLaborales');
    return response.data.data; // Asegúrate de que el backend retorna un array
}

export const obtenerContactos = async () => {
    const response = await HttpCliente.get('/Contacto');
    return response.data.data; // Ajusta si tu backend retorna en otra propiedad
};

export const obtenerContactoPorId = async (id) => {
    const response = await HttpCliente.get(`/Contacto/${id}`);
    return response.data.data; // Ajusta según la estructura de tu backend
};

export const actualizarContacto = async (id, objetoContacto) => {
    const response = await HttpCliente.put(`/Contacto/${id}`, objetoContacto);
    return response; // Puedes retornar response.data si tu backend lo retorna así
};

export const buscarContactosPorTermino = async (termino) => {
    // Si el término está vacío, no hacemos la llamada para evitar resultados innecesarios
    if (!termino || termino.trim() === '') {
        return [];
    }

    try {
        const response = await HttpCliente.get(`/Contacto/buscar?termino=${termino}`);
        // Devolvemos los datos. Es importante que cada objeto tenga un 'id' y un 'nombre'.
        return response.data.data;
    } catch (error) {
        console.error("Error al buscar contactos:", error);
        return []; // En caso de error, devolvemos un array vacío
    }
};

// Función para obtener contactos con filtros
export const obtenerContactosFiltrados = async (filtros = {}) => {
    try {
        const params = new URLSearchParams();
        
        if (filtros.termino) params.append('termino', filtros.termino);
        if (filtros.departamento) params.append('departamento', filtros.departamento);
        if (filtros.municipio) params.append('municipio', filtros.municipio);
        if (filtros.categoriaLaboral) params.append('categoriaLaboral', filtros.categoriaLaboral);
        if (filtros.tieneNegocio !== undefined) params.append('tieneNegocio', filtros.tieneNegocio);
        
        const queryString = params.toString();
        const url = queryString ? `/Contacto/filtrar?${queryString}` : '/Contacto';
        
        const response = await HttpCliente.get(url);
        return response.data.data;
    } catch (error) {
        console.error("Error al obtener contactos filtrados:", error);
        return [];
    }
};

// Función para obtener las asesorías de un contacto
export const obtenerAsesoriasPorContacto = async (contactoId) => {
    try {
        const response = await HttpCliente.get(`/Contacto/${contactoId}/Asesorias`);
        return response.data.data;
    } catch (error) {
        console.error("Error al obtener asesorías del contacto:", error);
        return [];
    }
};