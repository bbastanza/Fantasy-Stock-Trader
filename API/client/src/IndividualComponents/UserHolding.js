import React from "react";
import { Link } from "react-router-dom";
import { beautifyNumber } from "./../helpers/beautifyNumber";

export default function UserHolding({ holdingData, allocatedFunds }) {
    const totalShares = parseFloat(holdingData.totalShares).toFixed(4);
    const stockValue = "$" + beautifyNumber(holdingData.value);
    const allocationPercent =
        parseFloat((holdingData.value / allocatedFunds) * 100).toFixed(1) + "%";

    return (
        <div className="holding-container">
            <div
                className="user-holding dream-shadow row"
                style={{ justifyContent: "center" }}>
                <div className="col-md-3 col-sm-12">
                    <h3>{holdingData.companyName}</h3>
                    <Link
                        to={{
                            pathname: "/sell",
                            state: {
                                holdingData: holdingData,
                            },
                        }}>
                        <button className="btn btn-info dream-btn">Sell</button>
                    </Link>
                </div>
                <table className="table table-warning col-md-8 col-sm-12 holding-table">
                    <thead>
                        <tr>
                            <th>Abbr.</th>
                            <th>Shares</th>
                            <th>Value</th>
                            <th>Allocation</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{holdingData.symbol}</td>
                            <td>{totalShares}</td>
                            <td>{stockValue}</td>
                            <td>{allocationPercent}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}
