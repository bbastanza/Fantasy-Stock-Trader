import React from "react";
import { Link } from "react-router-dom";

export default function UserHolding({ holdingData, allocatedFunds }) {
    return (
        <div className="holding-container">
            <div className="user-holding dream-shadow row" style={{ justifyContent: "center" }}>
                <div className="col-md-3 col-sm-12">
                    <h1>{holdingData.asset}</h1>
                    <Link to={{
                        pathname: "/sell",
                        state: {
                            holdingData: holdingData
                        }
                    }}>
                        <button className="btn btn-info dream-btn">Sell</button>
                    </Link>
                </div>
                <table className="table table-warning col-md-8 col-sm-12">
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
                            <td>{holdingData.abbr}</td>
                            <td>{parseFloat(holdingData.shares).toFixed(4)}</td>
                            <td>{"$" + Math.round(parseFloat(holdingData.value).toFixed(2))}</td>
                            <td>{parseFloat((holdingData.value / allocatedFunds) * 100).toFixed(1) + "%"}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}
