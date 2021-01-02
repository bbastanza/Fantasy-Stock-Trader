import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { Button } from "react-bootstrap";
import { getUserTransactions } from "./../helpers/userApiCalls";
import CircleAnimation from "./../IndividualComponents/CircleAnimation";
import UserTransaction from "./../IndividualComponents/UserTransaction";
import Pagination from "./../IndividualComponents/Pagination";

export default function Transactions() {
    const history = useHistory();
    const [transactions, setTransactions] = useState([]);
    const [isLoading, setIsLoading] = useState(true);
    const [errorMessage, setErrorMessage] = useState("");
    const [currentPage, setCurrentPage] = useState(1);
    const transactionsPerPage = 5;

    const lastTransactionIndex = currentPage * transactionsPerPage;
    const firstTransactionIndex = lastTransactionIndex - transactionsPerPage;
    const currentTransactions = transactions.slice(firstTransactionIndex, lastTransactionIndex);

    useEffect(() => {
        (async () => {
            const transactionResponse = await getUserTransactions();
            setErrorMessage("");

            if (transactionResponse.friendlyMessage) {
                setErrorMessage(transactionResponse.friendlyMessage);
            } else {
                setTransactions(transactionResponse.reverse());
            }
            setIsLoading(false);
        })();
    }, []);

    const containerStyle = {
        width: "70%",
        justifyContent: "center",
        margin: "auto auto 60px",
        minWidth: 300,
    };

    return (
        <div style={containerStyle}>
            <h1 className="title">transactions</h1>
            {!!errorMessage ? (
                <>
                    {!isLoading && transactions.length > 1 ? <h2>No transaction yet. Let's buy some stocks!</h2> : null}
                    {!isLoading ? (
                        <div>
                            <Pagination
                                transactionsPerPage={transactionsPerPage}
                                totalPages={transactions.length}
                                changePage={setCurrentPage}
                            />
                            {currentTransactions.map(transaction => {
                                return <UserTransaction key={transaction.date} transactionData={transaction} />;
                            })}
                            {!!errorMessage ? <p>{errorMessage}</p> : null}
                            <Button
                                onClick={() => history.push("/dashboard")}
                                className="btn-info dream-btn"
                                style={{ margin: 20 }}
                            >
                                Back to Dashboard
                            </Button>
                            {currentTransactions.length > 2 ? (
                                <div>
                                    <Pagination
                                        transactionsPerPage={transactionsPerPage}
                                        totalPages={transactions.length}
                                        changePage={setCurrentPage}
                                    />
                                </div>
                            ) : null}
                        </div>
                    ) : (
                        <CircleAnimation />
                    )}
                </>
            ) : (
                <p>{errorMessage}</p>
            )}
        </div>
    );
}
