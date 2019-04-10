import { combineReducers } from 'redux'
import {
    GET_SOCKET,
    SET_SOCKET_STATUS_MESSAGE,
    RUN_SOCKET,
    STOP_SOCKET
} from '../constants/socket';

function instance(state = { webSocket: {}, status: false, message: '' }, action) {
    switch (action.type) {
        case GET_SOCKET:
            return state;
        case SET_SOCKET_STATUS_MESSAGE:
            let message = action.message;
            return { ...state, message };
        case RUN_SOCKET:
            let webSocket = new WebSocket(action.url);

            webSocket.onopen = action.onOpen;
            webSocket.onclose = action.onClose;
            webSocket.onmessage = action.onMessage;
            webSocket.onupdate = action.onUpdate;

            let status = true;
            return { ...state, webSocket, status }
        case STOP_SOCKET:
            state.webSocket.close(1000, action.reason);
            return { ...state, webSocket: {}, status: false }
        default:
            return state
    }
}

const socket = combineReducers({
    instance
})

export default socket