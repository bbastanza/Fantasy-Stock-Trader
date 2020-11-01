import React, { useState } from "react";
import Form from "react-bootstrap/Form";
import FormControl from "react-bootstrap/FormControl";
import InputGroup from "react-bootstrap/InputGroup";
import Button from "react-bootstrap/Button";

export default function Purchase({ availableFunds }) {
    const [unavailableFunds, setUnavailableFunds] = useState(0);
    const [purchaseAmount, setPurchaseAmount] = useState(0);
    const [validInput, setValidInput] = useState(true);
    const [stock, setStock] = useState("Brookfield Property REIT Inc. (BPYU)");
    const unavailableStyle = { backgroundColor: "#ffb3b9" };
    const numberRegex = /^[0-9]*$/;

    function checkFunds(e) {
        const amount = e.target.value;

        if (!numberRegex.test(amount)) setValidInput(false);
        else setValidInput(true);

        if (amount > availableFunds)
            setUnavailableFunds(amount - availableFunds);
        else {
            setPurchaseAmount(amount);
            setUnavailableFunds(0);
        }
    }

    function handleSubmit(e) {
        e.preventDefault();
        if (!validInput || unavailableFunds > 0 || purchaseAmount <= 0) {
            console.log("no purchase");
            return;
        }
        console.log(`purchased ${stock} in the amount of $${purchaseAmount}`);
    }

    return (
        <div className="purchase-form-container">
            <div className="purchase-form">
                <h1 className="title">Purchase</h1>
                <h2 className="available-funds">
                    Available Funds: {availableFunds}
                </h2>
                <Form
                    onSubmit={handleSubmit}
                    style={{ justifyContent: "center", textAlign: "center" }}
                >
                    <Form.Group
                        controlId="exampleForm.ControlSelect1"
                        onChange={e => setStock(e.target.value)}
                    >
                        <Form.Label className="purchase-form-label">
                            Stock
                        </Form.Label>
                        <Form.Control as="select">
                            <option>
                                Brookfield Property REIT Inc. (BPYU)
                            </option>
                            <option>Brighthouse Financial Inc. (BHF)</option>
                            <option>NRG Energy Inc. (NRG)</option>
                            <option>Ardagh Group SA (ARD)</option>
                            <option>NortonLifeLock Inc. (NLOK)</option>
                        </Form.Control>
                    </Form.Group>
                    <Form.Group>
                        <Form.Label className="purchase-form-label">
                            Amount
                        </Form.Label>
                        <InputGroup onChange={e => checkFunds(e)}>
                            <InputGroup.Prepend>
                                <InputGroup.Text>$</InputGroup.Text>
                            </InputGroup.Prepend>
                            <FormControl
                                style={
                                    unavailableFunds > 0 || !validInput
                                        ? unavailableStyle
                                        : null
                                }
                                aria-label="Amount (to the nearest dollar)"
                            />
                            <InputGroup.Append>
                                <InputGroup.Text>.00</InputGroup.Text>
                            </InputGroup.Append>
                        </InputGroup>
                    </Form.Group>
                    <h6
                        className="text-secondary"
                        style={{ padding: "20px 0 5px" }}
                    >
                        Buy with confidence (because it's not real money!)
                    </h6>

                    <Button
                        variant="warning"
                        type="submit"
                        className="dt-button btn-lg"
                    >
                        Buy Now!
                    </Button>
                </Form>
            </div>
            {unavailableFunds > 0 || !validInput ? (
                <div className="error-in-form">
                    {unavailableFunds > 0 ? (
                        <h3>
                            You do not have enough funds for this transaction.
                            Please reduce your purchase amount by $
                            {unavailableFunds}.
                        </h3>
                    ) : null}
                    {!validInput ? <h3>The amount must be a number.</h3> : null}
                </div>
            ) : null}
        </div>
    );
}
