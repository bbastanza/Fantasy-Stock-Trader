import React, { useEffect, useState } from "react";
import Modal from "./../FixedComponents/Modal";
import Form from "react-bootstrap/Form";
import Button from "react-bootstrap/Button";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import { useHistory } from "react-router-dom";

export default function Sell(props) {
    const history = useHistory();
    const [unavailableShares, setUnavailableShares] = useState(0);
    const [saleAmount, setSaleAmount] = useState(0);
    const [validInput, setValidInput] = useState(true);
    const [holdingData, setHoldingData] = useState("");
    const [sellAll, setSellAll] = useState(false);
    const unavailableStyle = { backgroundColor: "#ffb3b9" };
    const numberRegex = /^[0-9.]*$/;

    function checkShares(shares) {
        !numberRegex.test(shares) ? setValidInput(false) : setValidInput(true);

        if (shares > holdingData.shares) setUnavailableShares(shares - holdingData.shares);
        else {
            setSaleAmount(shares);
            setUnavailableShares(0);
        }
    }

    useEffect(() => {
        if (props.location.state.holdingData) {
            setHoldingData(props.location.state.holdingData);
        }
    });

    function handleSubmit(e) {
        e.preventDefault();
        if (!validInput || unavailableShares > 0) return;

        if (sellAll) console.log(`Sold all ${holdingData.asset}`);
        else console.log(`Sold ${saleAmount} ${holdingData.asset}`);

        history.push("/dashboard")

    }

    function toggleSellAll() {
        setSellAll(!sellAll);
        console.log(sellAll);
    }

    return (
        <Modal>
            <div className="login-container dream-shadow">
                <div className="purchase-form"></div>
                <h1 className="title">sell</h1>
                <h2>{holdingData.asset}</h2>
                <h2>Total Shares: {holdingData.shares}</h2>
                <h2>Current Value: ${holdingData.value}</h2>
                <Form onSubmit={handleSubmit} style={{ justifyContent: "center", textAlign: "center" }}>
                    <Form.Group>
                        <Form.Check
                            inline
                            id="sellAll"
                            type="checkbox"
                            onChange={toggleSellAll}
                            label={"Sell All " + holdingData.abbr}
                            checked={sellAll}
                        />
                    </Form.Group>
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
                    <Button type="submit">Sell Shares</Button>
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
