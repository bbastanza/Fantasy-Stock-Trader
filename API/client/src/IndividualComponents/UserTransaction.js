import React from "react";
import { beautifyNumber } from "./../helpers/beautifyFunds";

export default function UserTransaction({ transactionData }) {
    const transactionDate = new Date(transactionData.date).toLocaleDateString("en-US");
    const transactionTime = new Date(transactionData.date).toLocaleTimeString("en-US");

    const containerStyle = {
        backgroundColor: "#ffeeba",
        borderRadius: 10,
        border: "5px solid #ffdc91",
        margin: 20,
        textAlign: "left",
        display: "flex",
        justifyContent: "space-evenly",
        alignItems: "center",
        color: "#313131",
        padding: 20,
        width: "60%",
        minWidth: 330,
    };

    const spanStyle = {
        color: "#313131",
        fontSize: 16,
    };

    return (
        <div style={{ display: "flex", justifyContent: "center" }}>
            <div className="dream-shadow row" style={containerStyle}>
                <div
                    className="dream-btn col-lg-5 col-sm-12"
                    style={{ backgroundColor: "#ffdc91", width: "auto", padding: 10, height: "auto", borderRadius: 10 }}
                >
                    <h2 style={{ padding: 10, width: 200 }}>{transactionData.companyName}</h2>
                    <hr />
                    <h4>
                        {transactionData.type === "sell" ? "Sale" : "Purchase"}
                    </h4>
                    <hr />
                    <h4>
                        ${beautifyNumber(transactionData.amount)}
                    </h4>
                </div>
                <div className="col-lg-6 col-sm-12">
                    <h4>
                        <span style={spanStyle}>Symbol: </span>
                        {transactionData.symbol}
                    </h4>
                    <hr />
                    <h4>
                        <span style={spanStyle}>Price:</span> ${beautifyNumber(transactionData.transactionPrice)}
                    </h4>
                    <hr />
                    <h4>
                        <span style={spanStyle}>Date:</span> {transactionDate}{" "}
                    </h4>
                    <hr />
                    <h4>
                        <span style={spanStyle}>Time:</span> {transactionTime}{" "}
                    </h4>
                </div>
            </div>
        </div>
    );
}
