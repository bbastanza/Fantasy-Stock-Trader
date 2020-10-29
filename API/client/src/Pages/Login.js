import React, { useState } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import Link from "react-router-dom/Link";

export default function Login() {
    return (
        <div style={{ margin: "auto" }}>
            <h1>Login</h1>
            <Form className="login-container">
                <Form.Group as={Row} controlId="formBasicEmail">
                    <Form.Label column sm="2">
                        Email address
                    </Form.Label>
                    <Col sm="10">
                        <Form.Control type="email" placeholder="Enter email" />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} controlId="formPlaintextPassword">
                    <Form.Label column sm="2">
                        Password
                    </Form.Label>
                    <Col sm="10">
                        <Form.Control type="password" placeholder="Password" />
                    </Col>
                </Form.Group>

                <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
                    No Account? Register instead.
                </h6>

                <Form.Group
                    as={Row}
                    style={{ justifyContent: "center", padding: 10 }}
                >
                    <Col sm="2">
                        <Link to="/register">
                            <Button variant="secondary">Register</Button>
                        </Link>
                    </Col>
                    <Col sm="2">
                        <Button variant="warning" type="submit">
                            Login
                        </Button>
                    </Col>
                </Form.Group>
            </Form>
        </div>
    );
}
