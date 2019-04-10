import {
    SET_SOCKET_STATUS_MESSAGE,
    RUN_SOCKET,
    STOP_SOCKET
} from '../constants/socket';

export function setStatusMessage(message) {
    return {
        type: SET_SOCKET_STATUS_MESSAGE,
        message
    }
};

export function runSocket(url, onOpen, onClose, onMessage, onUpdate) {
    return {
        type: RUN_SOCKET,
        url,
        onOpen,
        onClose,
        onMessage,
        onUpdate
    }
};

export function stopSocket(reason) {
    return { type: STOP_SOCKET, reason }
};