import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { initializeSale } from "../helpers/transactionApiCalls";
import Modal from "./../FixedComponents/Modal";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";

export default function Sell(props) {
    const history = useHistory();
    const [unavailableShares, setUnavailableShares] = useState(0);
    const [shareAmount, setShareAmount] = useState(0);
    const [validInput, setValidInput] = useState(true);
    const [holdingData, setHoldingData] = useState("");
    const [sellAll, setSellAll] = useState(false);
    const [errorMessage, setErrorMessage] = useState("")                        
    const unavailableStyle = { backgroundColor: "#ffb3b9" };
    const numberRegex = /^[0-9.]*$/;

    function checkShares(shares) {
        !numberRegex.test(shares) ? setValidInput(false) : setValidInput(true);

        if (shares > holdingData.shares) setUnavailableShares(shares - holdingData.shares);
        else {
            setShareAmount(shares);
            setUnavailableShares(0);
        }
    }

    useEffect(() => {
        if (props.location.state.holdingData) {
            setHoldingData(props.location.state.holdingData);
        } else history.push("/dashboard");
    }, [history, props.location.state.holdingData]);

    async function handleSubmit(e) {
        e.preventDefault();
        setErrorMessage("")
        if ((shareAmount <= 0 && !sellAll) || unavailableShares > 0) return;

        const data = await initializeSale({
            symbol: holdingData.symbol,
            shareAmount: shareAmount,
            sellAll: sellAll,
        });

        data.friendlyMessage
            ? setErrorMessage(data.friendlyMessage)
            : history.push("/dashboard");
    }

    return (
        <Modal>
            <div className="login-container dream-shadow">
                <div className="purchase-form"></div>
                <h1 className="title">sell</h1>
                <div className="available-funds">
                    <h2>{holdingData.companyName}</h2>
                    <h2>Total Shares: {parseFloat(holdingData.totalShares).toFixed(4)}</h2>
                    <h2>Current Value: ${parseFloat(holdingData.value).toFixed(2)}</h2>
                </div>
                <Form onSubmit={handleSubmit} style={{ justifyContent: "center", textAlign: "center" }}>
                    {!sellAll ? (
                        <Form.Group as={Row} controlId="formBasicEmail">
                            <Form.Label column sm="3">
                                How Many Shares?
                            </Form.Label>
                            <Col sm="9">
                                <Form.Control
                                    onChange={e => checkShares(e.target.value)}
                                    step=".01"
                                    type="number"
                                    placeholder="Shares To Sell"
                                    style={unavailableShares > 0 || !validInput ? unavailableStyle : null}
                                />
                            </Col>
                        </Form.Group>
                    ) : null}
                    <Form.Group>
                        <Form.Check
                            inline
                            id="sellAll"
                            type="checkbox"
                            onChange={() => setSellAll(!sellAll)}
                            label={"Sell All " + holdingData.symbol}
                            checked={sellAll}
                        />
                    </Form.Group>
                    <Button type="submit" className="btn-info dream-btn">
                        Sell Shares
                    </Button>
                    <Button className="btn-secondary dream-btn" onClick={() => history.push("/dashboard")}>
                        Cancel
                    </Button>
                </Form>
                {unavailableShares > 0 ? (
                    <div className="error-in-form">
                        {unavailableShares > 0 ? (
                            <h3>
                                {"You do not have enough shares for this transaction. Please reduce your sale amount by " +
                                    unavailableShares.toFixed(4) +
                                    " shares"}
                                .
                            </h3>
                        ) : null}
                    </div>
                ) : null}
            </div>
        </Modal>
    );
}
