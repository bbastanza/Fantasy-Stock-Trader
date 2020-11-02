import React, { useState, useEffect, useContext } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import { Link } from "react-router-dom";
import { LoginContext } from "./../contexts/LoginContext";

export default function Register() {
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState(true);
    const loginContext = useContext(LoginContext);

    function handleSubmit(e) {
        e.preventDefault();
        loginContext.setIsLoggedIn(true);
        sessionStorage.setItem("isLoggedIn", JSON.stringify(true));
    }

    function logOut() {
        loginContext.setIsLoggedIn(false);
        sessionStorage.setItem("isLoggedIn", JSON.stringify(false));
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
        <LoginContext.Consumer>
            {context => {
                return !context.isLoggedIn ? (
                    <div style={{ margin: "auto" }}>
                        <h1 className="title">Register</h1>
                        <Form
                            onSubmit={e => handleSubmit(e)}
                            className="login-container"
                        >
                            <Form.Group as={Row} controlId="formBasicEmail">
                                <Form.Label column sm="3">
                                    Email address
                                </Form.Label>
                                <Col sm="9">
                                    <Form.Control
                                        type="email"
                                        placeholder="Enter email"
                                    />
                                </Col>
                            </Form.Group>
                            <p className="text-muted" style={{ fontSize: 12 }}>
                                We'll never share your email with anyone else.
                            </p>

                            <Form.Group
                                as={Row}
                                controlId="formPlaintextPassword"
                            >
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

                            <Form.Group
                                as={Row}
                                controlId="formPlaintextConfirmPassword"
                            >
                                <Form.Label column sm="3">
                                    Confirm Password
                                </Form.Label>
                                <Col sm="9">
                                    <Form.Control
                                        style={
                                            !matchPassword
                                                ? nonMatchingStyle
                                                : null
                                        }
                                        type="password"
                                        placeholder="Confirm Password"
                                        onChange={e => {
                                            setConfirmPassword(e.target.value);
                                        }}
                                    />
                                </Col>
                            </Form.Group>

                            <Button
                                variant="warning"
                                type="submit"
                                style={{ margin: 15 }}
                            >
                                Register
                            </Button>

                            <h6
                                className="text-muted"
                                style={{ padding: "20px 0 5px" }}
                            >
                                Already have an account? Login instead.
                            </h6>

                            <Link to="/login">
                                <Button variant="secondary">Log In</Button>
                            </Link>
                        </Form>
                        {!matchPassword ? (
                            <div className="error-in-form">
                                <h3>Passwords do not match.</h3>
                            </div>
                        ) : null}
                    </div>
                ) : (
                    <Button
                        variant="secondary"
                        style={{ margin: 40 }}
                        onClick={logOut}
                        href="/login"
                    >
                        Log Out
                    </Button>
                );
            }}
        </LoginContext.Consumer>
    );
}
