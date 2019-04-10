import React, { Component } from 'react';
import PropTypes from 'prop-types'
import { connect } from 'react-redux'
import { runSocket, stopSocket, setStatusMessage } from '../../shares/actions/socket'
import res from './resources';
import './socket.css';
import Monitor from '../monitors/monitor';
import Changelog from '../changelogs/changelog';

class SocketPanel extends Component {
    static propTypes = {
        connectionUrl: PropTypes.string.isRequired,
        onConnect: PropTypes.func.isRequired,
        onDisconnect: PropTypes.func.isRequired/* ,
        addMonitorState: PropTypes.func.isRequired,
        addLogMessage: PropTypes.func.isRequired */
    }
    constructor(props) {
        super(props);

        let monitorsInitialState = {
            CPU: {
                value: {
                    Current: [0]
                }
            },
            RAM: {
                value: {
                    Current: [0]
                }
            },
            HDD: {
                value: {
                    Current: [0]
                }
            }
        };

        this.state = {
            logMessages: [],
            monitors: monitorsInitialState,
            hasError: false
        }
    };

    onUpdate(socket) {
        if (!socket) {
            this.props.onDisconnect(res.SocketClosing)
        } else {
            switch (socket.readyState) {
                case WebSocket.CLOSED:
                    this.props.setStatusMessage(res.ClosedStatus);
                    break;
                case WebSocket.CLOSING:
                    this.props.setStatusMessage(res.ClosingStatus);
                    break;
                case WebSocket.CONNECTING:
                    this.props.setStatusMessage(res.ConnectingStatus);
                    break;
                case WebSocket.OPEN:
                    this.props.setStatusMessage(res.OpenStatus);
                    break;
                default:
                    this.props.setStatusMessage(res.UnknownState + socket.readyState);
                    break;
            }
        }
    }

    onOpen(event) {
        this.onUpdate(event.target);

        this.props.addLogMessage({
            from: "-",
            to: "-",
            data: res.ConnectionOpenedState
        });
    }

    onClose = function (event) {
        if (event.code === 1006) {
            this.setState({ hasError: true });
            return;
        }

        this.onUpdate(event.target);

        this.props.addLogMessage({
            from: "-",
            to: "-",
            data: "Connection closed. Code: " + event.code + ". Reason: " + event.reason
        });
    }

    onMessage = function (event) {
        var value = JSON.parse(event.data);

        this.addMonitorState(value);

        this.addLogMessage({
            from: res.Server,
            to: res.Client,
            data: "New state: " + value.Type + ": " + value.Current + "%"
        });
    }

    changeInput(propName, e) {
        var value = e.target.value;

        this.setState(
            state => ({
                ...state,
                [propName]: value
            })
        );
    }

    changeStatus(val) {
        this.props.status = val;
    }

    addMonitorState(value) {
        this.setState(
            state => ({
                ...state,
                ['monitors']: {
                    ...state['monitors'],
                    [value.Type]: {
                        ...state['monitors'][value.Type],
                        value
                    }
                }
            })
        );
    }

    addLogMessage(logMessage) {
        this.setState(
            state => ({
                ...state,
                ['logMessages']: [
                    ...state['logMessages'].slice((state['logMessages'].length - 15), 10), logMessage
                ]
            })
        );
    }

    render() {
        return (
            <div>
                <h1>{res.Monitoring}</h1>
                <p id="stateLabel">{this.props.message}</p>
                <div>
                    <label htmlFor="connectionUrl">{res.connectUrlTitle}</label>
                    <input id="connectionUrl" value={this.props.connectionUrl} disabled={this.props.status} onChange={this.changeInput.bind(this, 'connectionUrl')} />
                    <button id="connectButton" type="submit" disabled={this.props.status} onClick={() => { this.props.onConnect(this.props.connectionUrl, this.onOpen.bind(this), this.onClose.bind(this), this.onMessage.bind(this), this.onUpdate.bind(this)) }}>{res.connectBtnText}</button>
                    <button id="closeButton" disabled={!this.props.status} onClick={() => { this.props.onDisconnect(res.SocketClosing) }}>{res.closeSocketBtnText}</button>
                </div>
                <Monitor state={this.state} />
                <Changelog state={this.state} />
            </div>
        );
    }
}

const mapStateToProps = (state) => {
    return {
        status: state.instance.status,
        webSocket: state.instance.webSocket,
        message: state.instance.message || res.readyToConnect
    }
}

const mapDispatchToProps = (dispatch) => {
    return {
        onConnect: (url, onOpen, onClose, onMessage, onUpdate) => {
            dispatch(runSocket(url, onOpen, onClose, onMessage, onUpdate));
        },
        onDisconnect: (reason) => {
            dispatch(stopSocket(reason));
        },
        setStatusMessage: (message) => {
            dispatch(setStatusMessage(message));
        }
    }
}

const SocketPanelConnect = connect(
    mapStateToProps,
    mapDispatchToProps
)(SocketPanel)

export default SocketPanelConnect