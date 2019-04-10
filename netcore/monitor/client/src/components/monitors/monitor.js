import React, { Component } from 'react';
import res from './resources';
import './monitor.css';

class Monitor extends Component {
    constructor(props) {
        super(props);
    };

    render() {
        const { monitors } = this.props.state;

        let hdd = [];

        for (let index = 0; index < monitors['HDD'].value.Current.length; index = index + 2) {
            hdd[index] = {
                free: monitors['HDD'].value.Current[index],
                total: monitors['HDD'].value.Current[index + 1]
            }
        }

        return (
            <div>
                <h2>{res.Resources}</h2>
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
                </div>
            </div>
        );
    }
}

export default Monitor