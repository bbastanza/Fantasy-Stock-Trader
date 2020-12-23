import React, { useState, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { beautifyNumber } from "./../helpers/beautifyFunds";
import { getStockData, initializePurchase } from "../helpers/transactionApiCalls";
import Form from "react-bootstrap/Form";
import FormControl from "react-bootstrap/FormControl";
import InputGroup from "react-bootstrap/InputGroup";
import Button from "react-bootstrap/Button";
import Modal from "./../FixedComponents/Modal";
import DotAnimation from "./../IndividualComponents/DotAnimation";

export default function Purchase(props) {
    const history = useHistory();
    const [unavailableFunds, setUnavailableFunds] = useState(0);
    const [availableFunds, setAvailableFunds] = useState(0);
    const [purchaseAmount, setPurchaseAmount] = useState(0);
    const [validInput, setValidInput] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [stockData, setStockData] = useState();
    const [errorMessage, setErrorMessage] = useState();
    const numberRegex = /^[0-9]*$/;

    useEffect(() => {
        if (props.location.state.availableFunds) {
            setAvailableFunds(props.location.state.availableFunds);
        } else history.push("/dashboard");
    }, [history, props.location.state.availableFunds]);

    function checkFunds(e) {
        const amount = e.target.value;

        !numberRegex.test(amount) ? setValidInput(false) : setValidInput(true);

        if (amount > availableFunds) setUnavailableFunds(amount - availableFunds);
        else {
            setPurchaseAmount(amount);
            setUnavailableFunds(0);
        }
    }

    async function handleSubmit(e) {
        e.preventDefault();
        setErrorMessage("");
        setIsLoading(true);

        if (!validInput || unavailableFunds > 0 || purchaseAmount <= 0) return;
        
        const purchaseData = await initializePurchase({
            symbol: stockData.symbol,
            amount: purchaseAmount,
        });

        if (!purchaseData.friendlyMessage) history.push("/dashboard");
        else setErrorMessage(purchaseDatas.friendlyMessage)
        setIsLoading(false);
    }

    async function searchStock(value) {
        setStockData(null);
        if (value.length > 1) {
            const stockData = await getStockData(value);
            if (stockData) {
                setStockData(stockData);
            }
        }
    }

    const unavailableStyle = { backgroundColor: "#ffb3b9" };

    return (
        <Modal>
            <div className="login-container dream-shadow">
                <div className="purchase-form">
                    <h1 className="title">purchase</h1>
                    <h2 className="available-funds">Available Funds: {beautifyNumber(availableFunds)}</h2>
                    {!isLoading ? (
                        <>
                            {stockData ? (
                                <>
                                    <h3>{stockData.companyName}</h3>
                                    <h3>${beautifyNumber(stockData.latestPrice)}</h3>
                                </>
                            ) : null}
                            <Form onSubmit={handleSubmit} style={{ justifyContent: "center", textAlign: "center" }}>
                                <Form.Group >
                                    <Form.Label className="purchase-form-label">Stock</Form.Label>
                                    <Form.Control
                                        type="text"
                                        onChange={e => searchStock(e.target.value)}
                                    ></Form.Control>
                                </Form.Group>

                                <Form.Group>
                                    <Form.Label className="purchase-form-label">Amount</Form.Label>
                                    <InputGroup onChange={e => checkFunds(e)}>
                                        <InputGroup.Prepend>
                                            <InputGroup.Text>$</InputGroup.Text>
                                        </InputGroup.Prepend>
                                        <FormControl
                                            style={unavailableFunds > 0 || !validInput ? unavailableStyle : null}
                                            aria-label="Amount (to the nearest dollar)"
                                        />
                                        <InputGroup.Append>
                                            <InputGroup.Text>.00</InputGroup.Text>
                                        </InputGroup.Append>
                                    </InputGroup>
                                </Form.Group>

                                <h6 className="text-secondary" style={{ padding: "20px 0 5px" }}>
                                    Buy with confidence (because it's not real money!)
                                </h6>

                                {errorMessage !== ""
                                    ? <p>{errorMessage}</p>
                                    : null}

                                <Button variant="info" type="submit" className="dream-btn">
                                    Buy Now!
                                </Button>
                            </Form>
                        </>
                    ) : (
                        <DotAnimation />
                    )}
                </div>
                {unavailableFunds > 0 || !validInput ? (
                    <div className="error-in-form">
                        {unavailableFunds > 0 ? (
                            <h3>
                                You do not have enough funds for this transaction. Please reduce your purchase amount by
                                ${unavailableFunds}.
                            </h3>
                        ) : null}
                        {!validInput ? <h3>The amount must be a number.</h3> : null}
                    </div>
                ) : null}
            </div>
        </Modal>
    );
}
