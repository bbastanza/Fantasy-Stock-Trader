import React, { useState, useEffect } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Link from "react-router-dom/Link";

export default function Register() {
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState(true);

    useEffect(() => {
        if (
            password !== confirmPassword &&
            password.length <= confirmPassword.length
        )
            setMatchPassword(false);
        else setMatchPassword(true);
    }, [password, confirmPassword]);

    const nonMatchingStyle = { backgroundColor: "#ffb3b9" };

    return (
        <div style={{ margin: "auto" }}>
            <h1>Register</h1>
            <Form className="login-container">
                <Form.Group as={Row} controlId="formBasicEmail">
                    <Form.Label column sm="2">
                        Email address
                    </Form.Label>
                    <Col sm="10">
                        <Form.Control type="email" placeholder="Enter email" />
                    </Col>
                </Form.Group>
                <p className="text-muted" style={{ fontSize: 12 }}>
                    We'll never share your email with anyone else.
                </p>

                <Form.Group as={Row} controlId="formPlaintextPassword">
                    <Form.Label column sm="2">
                        Password
                    </Form.Label>
                    <Col sm="10">
                        <Form.Control
                            type="password"
                            placeholder="Password"
                            onChange={e => {
                                setPassword(e.target.value);
                            }}
                        />
                    </Col>
                </Form.Group>

                <Form.Group as={Row} controlId="formPlaintextPassword">
                    <Form.Label column sm="2">
                        Confirm Password
                    </Form.Label>
                    <Col sm="10">
                        <Form.Control
                            style={!matchPassword ? nonMatchingStyle : null}
                            type="password"
                            placeholder="Confirm Password"
                            onChange={e => {
                                setConfirmPassword(e.target.value);
                            }}
                        />
                    </Col>
                </Form.Group>

                <h6 className="text-muted" style={{ paddingBottom: 15 }}>
                    Already have an account? Login instead.
                </h6>

                <Form.Group as={Row} style={{ justifyContent: "center" }}>
                    <Col sm="2">
                        <Link to="/login">
                            <Button variant="secondary">Login</Button>
                        </Link>
                    </Col>
                    <Col sm="2">
                        <Button variant="warning" type="submit">
                            Register
                        </Button>
                    </Col>
                </Form.Group>
            </Form>
            {!matchPassword ? (
                <div>
                    <h1 style={{ color: "#dd5b36" }}>
                        Passwords do not match.
                    </h1>
                </div>
            ) : null}
        </div>
    );
}
