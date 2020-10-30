import React, { useState, useEffect } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import NavLink from "react-bootstrap/NavLink";

export default function Register({ setIsLoggedIn }) {
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState(true);

    function handleSubmit(e) {
        e.preventDefault();
        setIsLoggedIn(true);
    }

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
            <h1 className="title">Register</h1>
            <Form onSubmit={e => handleSubmit(e)} className="login-container">
                <Form.Group as={Row} controlId="formBasicEmail">
                    <Form.Label column sm="3">
                        Email address
                    </Form.Label>
                    <Col sm="9">
                        <Form.Control type="email" placeholder="Enter email" />
                    </Col>
                </Form.Group>
                <p className="text-muted" style={{ fontSize: 12 }}>
                    We'll never share your email with anyone else.
                </p>

                <Form.Group as={Row} controlId="formPlaintextPassword">
                    <Form.Label column sm="3">
                        Password
                    </Form.Label>
                    <Col sm="9">
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
                    <Form.Label column sm="3">
                        Confirm Password
                    </Form.Label>
                    <Col sm="9">
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

                <Button variant="warning" type="submit" style={{ margin: 15 }}>
                    Register
                </Button>
                <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
                    Already have an account? Login instead.
                </h6>

                <NavLink to="/" exact>
                    <Button variant="secondary">Log In</Button>
                </NavLink>
            </Form>
            {!matchPassword ? (
                <div>
                    <h5 style={{ color: "#dd5b36" }}>
                        Passwords do not match.
                    </h5>
                </div>
            ) : null}
        </div>
    );
}
