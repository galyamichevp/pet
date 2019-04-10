import React, { Component } from 'react';
import res from './resources';
import './changelog.css';

class ChangeLog extends Component {
    constructor(props) {
        super(props);
    };

    render() {
        const { logMessages } = this.props.state;

        return (
            <div>
                <h2>{res.CommunicationLog}</h2>
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
                </div>
            </div>
        );
    }
}

export default ChangeLog