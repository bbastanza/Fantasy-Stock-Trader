import React from "react";
import { Link } from "react-router-dom";

export default function UserHolding({ holdingData, allocatedFunds }) {
    return (
        <div className="holding-container">
            <div className="user-holding dream-shadow row" style={{ justifyContent: "center" }}>
                <div className="col-md-3 col-sm-12">
                    <h3>{holdingData.companyName}</h3>
                    <Link to={{
                        pathname: "/sell",
                        state: {
                            holdingData: holdingData
                        }
                    }}>
                        <button className="btn btn-info dream-btn">Sell</button>
                    </Link>
                </div>
                <table className="table table-warning col-md-8 col-sm-12" style={{alignSelf: "center"}}>
                    <thead>
                        <tr>
                            <th>Abbr.</th>
                            <th>Shares</th>
                            <th>Value</th>
                            <th>Allcoation</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr>
                            <td>{holdingData.symbol}</td>
                            <td>{parseFloat(holdingData.totalShares).toFixed(4)}</td>
                            <td>{"$" + parseFloat(holdingData.value).toFixed(2)}</td>
                            <td>{parseFloat((holdingData.value / allocatedFunds) * 100).toFixed(1) + "%"}</td>
                        </tr>
                    </tbody>
                </table>
            </div>
        </div>
    );
}
