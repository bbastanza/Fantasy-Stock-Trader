import React, { useState, useEffect, useRef } from "react";
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
    const [errorMessage, setErrorMessage] = useState("");
    const [unavailableFunds, setUnavailableFunds] = useState(0);
    const [availableFunds, setAvailableFunds] = useState(0);
    const [purchaseAmount, setPurchaseAmount] = useState(0);
    const [validInput, setValidInput] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [isSearching, setIsSearching] = useState(false);
    const [stockData, setStockData] = useState(null);
    const [canSearch, setCanSearch] = useState(false);
    const searchTermRef = useRef("");
    const timeoutRef = useRef(null);
    const numberRegex = /^[0-9]*$/;
    const unavailableStyle = { backgroundColor: "#ffb3b9" };

    useEffect(() => {
        if (props.location.state.availableFunds) {
            setAvailableFunds(props.location.state.availableFunds);
        } else history.push("/dashboard");
    }, [history, props.location.state.availableFunds]);

    useEffect(() => {
        setStockData(null);
        if (timeoutRef.current !== null) clearTimeout(timeoutRef.current);

        const canCallApi = canSearch && !!searchTermRef.current;
        if (canCallApi) {
            (async function () {
                setErrorMessage("");
                setIsSearching(true);
                const stockData = await getStockData(searchTermRef.current);
                if (!stockData.ClientMessage) {
                    stockData.companyName.length > 0
                        ? setStockData(stockData)
                        : setErrorMessage("The stock you are looking for is invalid. Try again.");
                } else {
                    setErrorMessage(stockData.ClientMessage);
                }
                setIsSearching(false);
            })();
        }

        timeoutRef.current = setTimeout(() => {
            timeoutRef.current = null;
            setCanSearch(true);
        }, 1000);
    }, [canSearch]);

    function checkFunds(amount) {
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

        if (!!stockData) {
            const purchaseData = await initializePurchase({
                symbol: stockData.symbol,
                amount: purchaseAmount,
            });
            if (!purchaseData.ClientMessage) history.push("/dashboard");
            else {
                setErrorMessage(purchaseData.ClientMessage);
                setIsLoading(false);
            }
        }
    }

    const hasValidStockData = !!stockData && !isNaN(stockData.latestPrice) && stockData.companyName.length > 0;
    const canSubmit = !!stockData && purchaseAmount > 0 && validInput && unavailableFunds === 0;

    return (
        <Modal>
            <div className="login-container dream-shadow">
                <div className="purchase-form">
                    <h1 className="title">purchase</h1>
                    <h2 className="available-funds">Available Funds: ${beautifyNumber(availableFunds)}</h2>
                    {!isLoading ? (
                        <>
                            <Form onSubmit={handleSubmit} style={{ justifyContent: "center", textAlign: "center" }}>
                                <Form.Group>
                                    <Form.Label className="purchase-form-label">Stock</Form.Label>
                                    <h6 className="text-secondary">Type stock abbreviation to search for a stock.</h6>
                                    <Form.Control
                                        type="text"
                                        onChange={e => {
                                            searchTermRef.current = e.target.value;
                                            setCanSearch(false);
                                        }}
                                    ></Form.Control>
                                </Form.Group>

                                <Form.Group>
                                    <Form.Label className="purchase-form-label">Amount</Form.Label>
                                    <InputGroup onChange={e => checkFunds(e.target.value)}>
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

                                {isSearching ? <DotAnimation /> : null}
                                {errorMessage.length > 0 ? <p>{errorMessage}</p> : null}

                                {hasValidStockData ? (
                                    <div className="available-funds">
                                        <h3>{stockData.companyName}</h3>
                                        <h3>${beautifyNumber(stockData.latestPrice)}</h3>
                                    </div>
                                ) : null}

                                {canSubmit ? (
                                    <Button variant="info" type="submit" className="dream-btn">
                                        Buy Now!
                                    </Button>
                                ) : null}

                                <Button
                                    variant="secondary"
                                    onClick={() => history.push("/dashboard")}
                                    className="dream-btn"
                                >
                                    Cancel
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
