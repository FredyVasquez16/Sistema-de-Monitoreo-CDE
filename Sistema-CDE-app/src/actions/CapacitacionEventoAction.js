import HttpCliente from '../services/HttpCliente';

export const guardarCapacitacion = (objetoCapacitacion) => {
    return HttpCliente.post('/Capacitacion', objetoCapacitacion);
}

export const obtenerCapacitaciones = async () => {
    const response = await HttpCliente.get('/Capacitacion');
    return response.data.data;
}

export const obtenerCapacitacionPorId = async (id) => {
    const response = await HttpCliente.get(`/Capacitacion/${id}`);
    return response.data.data;
}

export const actualizarCapacitacion = async (id, objetoCapacitacion) => {
    const response = await HttpCliente.put(`/Capacitacion/${id}`, objetoCapacitacion);
    return response;
}

export const eliminarCapacitacion = async (id) => {
    const response = await HttpCliente.delete(`/Capacitacion/${id}`);
    return response;
}
