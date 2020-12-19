import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button } from "react-bootstrap";
import { getUserTransactions } from "./../helpers/userApiCalls";
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
        })();
        // TODO add error handling
    }, []);

    const containerStyle = {
        width: "70%",
        justifyContent: "center",
        margin: "auto",
        minWidth: 300,
    };

    function changePage(pageNumber){
        // TODO pagination component
        // ? How do I render a portion of the array
        // ? maybe using a loop where you set i to a divisable of 5 number the condition to i+5 ???
    }

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
                        className="btn-info dream-btn"
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
