import React, { useContext } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Nav from "react-bootstrap/Nav";
import { LoginContext } from "./../contexts/LoginContext";

export default function Login() {
    // const loginContext = useContext(LoginContext);

    function handleSubmit(e) {
        e.preventDefault();
        // loginContext.setIsLoggedIn(true);
        sessionStorage.setItem("isLoggedIn", JSON.stringify(true));
    }

    function logOut() {
        // loginContext.setIsLoggedIn(false);
        sessionStorage.setItem("isLoggedIn", JSON.stringify(false));
    }

    return (
        <LoginContext.Consumer>
            {context => {
                console.log(context);
                return !context.isLoggedIn ? (
                    <div style={{ margin: "auto" }}>
                        <h1 className="title">Log In</h1>
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
                                    />
                                </Col>
                            </Form.Group>{" "}
                            <Button
                                variant="warning"
                                type="submit"
                                style={{ margin: 15 }}
                            >
                                Log In
                            </Button>
                            <h6
                                className="text-muted"
                                style={{ padding: "20px 0 5px" }}
                            >
                                No Account? Register Now!
                            </h6>
                            <Nav.Link href="/register">
                                <Button variant="secondary">Register</Button>
                            </Nav.Link>
                        </Form>
                    </div>
                ) : (
                    <Button
                        variant="secondary"
                        style={{ margin: 40 }}
                        onClick={logOut}
                    >
                        Log Out
                    </Button>
                );
            }}
        </LoginContext.Consumer>
    );
}
