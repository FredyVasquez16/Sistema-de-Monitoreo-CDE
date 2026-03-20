// src/Context/reducers/openSnackbarReducer.js

const initialState = {
    open: false,
    mensaje: '', // Cambié 'message' a 'mensaje' para ser consistente con el resto del código
    severity: 'info' // Añadimos severity para los colores
};

const openSnackbarReducer = (state = initialState, action) => {
    switch (action.type) {
        case "OPEN_SNACKBAR":
            // CORRECCIÓN: Ahora leemos de 'action.payload'
            // que es lo que se envía desde los componentes.
            return {
                ...state,
                open: action.payload.open,
                mensaje: action.payload.mensaje,
                severity: action.payload.severity
            };
        default:
            return state;
    }
}

export default openSnackbarReducer;