import React from "react";

export default function UserHolding({ data, allocatedFunds }) {
    console.log(data);
    return (
        <div className="holding-container">
            <div
                className="user-holding row"
                style={{ justifyContent: "center" }}
            >
                <h1 className="col-3">{data.asset}</h1>
                <table className="table table-warning col-8">
                    <thead>
                        <tr>
                            <th>Abbr.</th>
                            <th>Amount</th>
                            <th>Value</th>
                            <th>Allcoation</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{data.abbr}</td>
                            <td>{parseFloat(data.amount).toFixed(4)}</td>
                            <td>
                                {"$" +
                                    Math.round(
                                        parseFloat(data.value).toFixed(2)
                                    )}
                            </td>
                            <td>
                                {parseFloat(
                                    (data.value / allocatedFunds) * 100
                                ).toFixed(1) + "%"}
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}
