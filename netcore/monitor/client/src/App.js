import React, { Component } from 'react';
import './App.css';
import res from './resources';

import SocketPanel from './components/panels/socket';

class App extends Component {
  constructor(props) {
    super(props);

    let scheme = (document.location.protocol === "https:" ? "wss" : "ws");
    let port = (":" + 5000);

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
      connectionUrl: scheme + "://" + document.location.hostname + port + "/api/monitors/run",
      connectionUrlStatus: true,
      port: port,
      scheme: scheme,
      sendMessageStatus: false,
      sendButtonStatus: false,
      socket: null,
      stateLabel: res.readyToConnect,
      message: "",
      logMessages: [],
      monitors: monitorsInitialState,
      hasError: false
    }
  };



  addLogMessage(logMessage) {
    /* this.setState(
      state => ({
        ...state,
        ['logMessages']: [
          ...state['logMessages'].slice((state['logMessages'].length - 15), 10), logMessage
        ]
      })
    ); */
  }

  addMonitorState(value) {
    /* this.setState(
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
    ); */
  }




  /* disable() {
    this.setState(
      state => ({
        ...state,
        ['sendMessageStatus']: false,
        ['sendButtonStatus']: false,
        ['closeButtonStatus']: false,
      })
    );
  }

  enable() {
    this.setState(
      state => ({
        ...state,
        ['sendMessageStatus']: true,
        ['sendButtonStatus']: true,
        ['closeButtonStatus']: true,
      })
    );
  } */


  changeState(propName, val) {
    this.setState(
      state => ({
        ...state,
        [propName]: val
      })
    );
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

  render() {
    const { connectionUrl, closeButtonStatus, stateLabel, connectionUrlStatus, connectButtonStatus, logMessages, monitors, hasError } = this.state;

    if (hasError) {
      return <h1>{res.CommonError}</h1>;
    }

    let hdd = [];

    for (let index = 0; index < monitors['HDD'].value.Current.length; index = index + 2) {
      hdd[index] = {
        free: monitors['HDD'].value.Current[index],
        total: monitors['HDD'].value.Current[index + 1]
      }
    }

    return (
      <div className="App">
        <SocketPanel
          connectionUrl={connectionUrl}
          addLogMessage={this.addLogMessage}
          addMonitorState={this.addMonitorState} />




        {/* <h2>{res.Resources}</h2>
        <div>
          <table className="table-log">
            <tbody id="monitors">
              {Object.keys(monitors['CPU'].value.Current).length > 0
                ? Object.keys(monitors['CPU'].value.Current).map((monitor, index) => (
                  <tr key={index}>
                    <td>{res.CPU} {index}</td>
                    <td className="table_td-monitor-value">
                      <div className="shell">
                        <div className="bar" style={{ width: monitors['CPU'].value.Current[monitor] + '%' }}><span>{monitors['CPU'].value.Current[monitor] + " %"}</span></div>
                      </div>
                    </td>
                  </tr>
                )) : null}
              <tr key="ram">
                <td>{res.RAM}</td>
                <td className="table_td-monitor-value">
                  <div className="shell">
                    <div className="bar" style={{ width: (monitors['RAM'].value.Current[0] / monitors['RAM'].value.Current[1]) * 100 + '%' }}><span>{Math.floor(monitors['RAM'].value.Current[0]) + "/" + Math.floor(monitors['RAM'].value.Current[1]) + " MB"}</span></div>
                  </div>
                </td>
              </tr>
              {
                Object.keys(hdd).length > 0
                  ? Object.keys(hdd).map((monitor, index) => (
                    <tr key={index}>
                      <td>{res.HDD} {index}</td>
                      <td className="table_td-monitor-value">
                        <div className="shell">
                          <div className="bar" style={{ width: (hdd[monitor].free / hdd[monitor].total) * 100 + '%' }}><span>{Math.floor(hdd[monitor].free) + "/" + Math.floor(hdd[monitor].total) + " MB"}</span></div>
                        </div>
                      </td>
                    </tr>
                  )) : null}
            </tbody>
          </table>
        </div> */}







       {/*  <h2>{res.CommunicationLog}</h2>
        <div className="table-log-container">
          <table className="table-log">
            <thead>
            </thead>
            <tbody id="commsLog">
              {Object.keys(logMessages).length > 0
                ? Object.keys(logMessages).map((logMessage, index) => (
                  <tr key={logMessage}>
                    <td className="commslog-data">{logMessages[logMessages.length - 1 - index].data}</td>
                  </tr>
                )) : null}
            </tbody>
          </table>
        </div> */}
      </div>
    );
  }
}

export default App;
