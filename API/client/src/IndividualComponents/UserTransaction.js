import React from "react";
import tealGradientImage from "./../Images/tealgrad.jpeg";

export default function UserTransaction({ transactionData }) {
    const transactionDate = new Date(transactionData.date).toLocaleDateString("en-US");
    const transactionTime = new Date(transactionData.date).toLocaleTimeString("en-US");
    const containerStyle = {
        backgroundImage: `url(${tealGradientImage})`,
        borderRadius: 10,
        padding: 20,
        margin: 20,
        textAlign: "left",
        display: "flex",
        justifyContent: "space-evenly",
        alignItems: "center",
        color: "#ffc107",
    };
    const spanStyle = {
        color: "#313131",
        fontSize: 16,
    };
    return (
        <div className="dream-shadow" style={containerStyle}>
            <div>
                <h2 style={{ width: 150 }}>{transactionData.companyName}</h2>
            </div>
            <div>
                <h4>
                    <span style={spanStyle}>Symbol: </span>
                    {transactionData.symbol}
                </h4>
                <hr />
                <h4>
                    <span style={spanStyle}>Type:</span>{" "}
                    {transactionData.type === "sell" ? "Sale" : "Purchase"}
                </h4>
                <hr />
                <h4>
                    <span style={spanStyle}>Price:</span> $
                    {transactionData.transactionPrice}
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
    );
}
