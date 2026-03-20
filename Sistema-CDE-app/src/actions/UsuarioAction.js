import HttpCliente from '../services/HttpCliente';
import axios from 'axios';

const instancia = axios.create();
instancia.CancelToken = axios.CancelToken;
instancia.isCancel = axios.isCancel;

export const registrarUsuario = usuario => {
    return new Promise((resolve, eject) => {
        instancia.post('/Auth/signin', usuario).then(response => {
            /*if (response.status === 200) {
                resolve(response.data);
            } else {
                eject(new Error('Error al registrar el usuario'));
            }*/
            resolve(response);
        })
    })
}

/*export const registrarUsuario = async (usuario) => {
    const response = await HttpCliente.post('/Auth/signin', usuario);
    return response.data;
}*/

export const obtenerUsuarioActual = (dispatch) => {
    return new Promise((resolve, reject) => {
        // El interceptor ya está inyectando el token desde localStorage
        HttpCliente.get('/Auth')
            .then((response) => {
                if (response.data && response.data.data) {
                    // Si el token es válido, actualizamos el estado global
                    dispatch({
                        type: 'INICIAR_SESION',
                        sesion: response.data.data,
                        autenticado: true,
                    });
                }
                resolve(response);
            })
            .catch((error) => {
                // Si el token NO es válido, limpiamos la sesión y el token
                window.localStorage.removeItem("token_seguridad");
                dispatch({
                    type: 'SALIR_SESION', // Limpia el estado global
                });
                reject(error);
            });
    });
};

export const actualizarUsuario = (usuario) => {
    return new Promise((resolve, reject) => {
        HttpCliente.put('/Auth', usuario).then(response => {
            resolve(response);
        })
            .catch(error => {
                resolve(error.response);
            });
    })
}

/*export const loginUsuario = (usuario, dispatch) => {
    return new Promise((resolve, eject) => {
        instancia.post('/Auth/login', usuario).then(response => {

            dispatch({
                type: 'INICIAR_SESION',
                sesion: response.data.data,
                autenticado: true
            });

            resolve(response);
        }).catch(error => {
            resolve(error.response);
        });
    });
}*/

export const loginUsuario = (usuario) => {
    return HttpCliente.post('/Auth/login', usuario);
};

/*export const loginUsuario = (usuario) => {
    // HttpCliente.post ya devuelve una promesa. Simplemente la retornamos.
    // Esto evita capas innecesarias y posibles problemas de contexto.
    return HttpCliente.post('/Auth/login', usuario);
};*/