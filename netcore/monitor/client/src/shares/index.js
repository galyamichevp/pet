import { createStore } from 'redux';
import socket from './reducers'

let store = createStore(socket);

export default store;