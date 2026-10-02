import HttpCliente from '../services/HttpCliente';
import { obtenerFuentesFinanciamiento as obtenerFuentesFinanciamientoCliente } from './ClienteEmpresaAction';

export const guardarAsesoria = (objetoAsesoria) => {
    return HttpCliente.post('/Asesoria', objetoAsesoria);
}

export const obtenerAsesorias = async () => {
    const response = await HttpCliente.get('/Asesoria');
    return response.data.data;
}

export const obtenerAsesoriaPorId = async (id) => {
    const response = await HttpCliente.get(`/Asesoria/${id}`);
    return response.data.data;
}

export const actualizarAsesoria = async (id, objetoAsesoria) => {
    const response = await HttpCliente.put(`/Asesoria/${id}`, objetoAsesoria);
    return response;
}

export const eliminarAsesoria = async (id) => {
    const response = await HttpCliente.delete(`/Asesoria/${id}`);
    return response;
}

export const obtenerTiposContactos = async () => {
    try {
        const response = await HttpCliente.get('/Asesoria/TiposContactos');
        return response.data.data;
    } catch (error) {
        console.error('Error cargando tipos de contacto:', error);
        return [];
    }
}

export const obtenerAreasAsesoria = async () => {
    try {
        const response = await HttpCliente.get('/Asesoria/AreasAsesoria');
        return response.data.data;
    } catch (error) {
        console.error('Error cargando áreas de asesoría:', error);
        return [];
    }
}

export const obtenerAyudasAdicionales = async () => {
    // Por ahora retornamos opciones predefinidas
    // TODO: Crear endpoint en backend si se necesita dinámico
    return [
        { id: 1, descripcion: 'Asesoría Especializada' },
        { id: 2, descripcion: 'Estudio de Mercado' },
        { id: 3, descripcion: 'Diagnóstico Empresarial' },
        { id: 4, descripcion: 'Plan de Negocios' },
        { id: 5, descripcion: 'Asistencia Técnica' },
        { id: 6, descripcion: 'Acompañamiento' }
    ];
}

export const obtenerReferencias = async () => {
    // Por ahora retornamos opciones predefinidas
    // TODO: Crear endpoint en backend si se necesita dinámico
    return [
        { id: 1, descripcion: ' PROCHOMAN' },
        { id: 2, descripcion: ' CIDH' },
        { id: 3, descripcion: ' IHCAFE' },
        { id: 4, descripcion: ' UCBD' },
        { id: 5, descripcion: ' SAG' },
        { id: 6, descripcion: ' Banadesa' },
        { id: 7, descripcion: ' Cooperativa' },
        { id: 8, descripcion: ' Referencias personales' },
        { id: 9, descripcion: ' Redes sociales' },
        { id: 10, descripcion: ' Visitas puerta a puerta' },
        { id: 11, descripcion: ' Otros' }
    ];
}

export const obtenerFuentesFinanciamiento = async () => {
    try {
        const result = await obtenerFuentesFinanciamientoCliente();
        return result || [];
    } catch (error) {
        console.error('Error cargando fuentes de financiamiento:', error);
        return [];
    }
}

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

export const buscarContactosPorTermino = async (termino) => {
    if (!termino || termino.trim() === '') {
        return [];
    }
    try {
        const response = await HttpCliente.get(`/Contacto/buscar?termino=${termino}`);
        return response.data.data;
    } catch (error) {
        console.error('Error buscando contactos:', error);
        return [];
    }
};

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
