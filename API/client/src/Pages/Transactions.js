import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button } from "react-bootstrap";
import { getUserTransactions } from "./../helpers/userApiCalls";
import UserTransaction from "./../IndividualComponents/UserTransaction";
import CircleAnimation from "./../IndividualComponents/CircleAnimation";

export default function Transactions() {
    const history = useHistory();
    const [transactions, setTransactions] = useState([]);
    const [pageTransactions, setPageTransaction] = useState([])
    const [pageNumber, setPageNumber] = useState(0)

    useEffect(() => {
        (async () => {
            const transactionResponse = await getUserTransactions();
            setTransactions(transactionResponse.reverse());
            console.log(transactionResponse);
        })();
        // TODO add error handling
    }, []);

    useEffect(() => {
        (() => {
            const transactionsPerPage = [];
            for (let i = (5 * pageNumber - 5);i <= (i + 5); i++){
                transactions.push(transactions[i])
            }
            setPageTrasactions(transactionsPerPage)
        })();
    }, [pageNumber])

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
                  <Pagination
                        transactionCount={transactions.length}
                        changePage={setPageNumber}/>
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
