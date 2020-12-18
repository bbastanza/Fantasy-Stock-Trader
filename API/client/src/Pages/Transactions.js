import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import Modal from "./../FixedComponents/Modal";
import { Button } from "react-bootstrap";
import { getUserTransactions } from "./../helpers/axios";
import UserTransaction from "./../IndividualComponents/UserTransaction";
import CircleAnimation from "./../IndividualComponents/CircleAnimation";

export default function Transactions() {
    const history = useHistory();
    const [transactions, setTransactions] = useState([]);

    useEffect(() => {
        (async () => {
            const transactionResponse = await getUserTransactions();
            setTransactions(transactionResponse.reverse());
            console.log(transactionResponse);

            console.log("hi");
        })();
    }, []);

    const containerStyle = {
        width: "70%",
        justifyContent: "center",
        margin: "auto",
        minWidth: 300,
    };
    return (
        <div style={containerStyle}>
            <h1 className="title">transactions</h1>
            {transactions.length > 0 ? (
                <div>
                    {transactions.map(transaction => {
                        return <UserTransaction transactionData={transaction} key={transaction.date} />;
                    })}
                    <Button
                        onClick={() => history.push("/dashboard")}
                        className="btn-info"
                        style={{ margin: 20, marginBottom: 80 }}
                    >
                        Back to Dashboard
                    </Button>
                </div>
            ) : (
                <CircleAnimation />
            )}
        </div>
    );
}
