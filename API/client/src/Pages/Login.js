import React, { useState } from "react";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";

import Nav from "react-bootstrap/Nav";

export default function Login() {
    return (
        <div style={{ margin: "auto" }}>
            <h1 className="title">Login</h1>
            <Form className="login-container">
                <Form.Group as={Row} controlId="formBasicEmail">
                    <Form.Label column sm="3">
                        Email address
                    </Form.Label>
                    <Col sm="9">
                        <Form.Control type="email" placeholder="Enter email" />
                    </Col>
                </Form.Group>
                <Form.Group as={Row} controlId="formPlaintextPassword">
                    <Form.Label column sm="3">
                        Password
                    </Form.Label>
                    <Col sm="9">
                        <Form.Control type="password" placeholder="Password" />
                    </Col>
                </Form.Group>{" "}
                <Button variant="warning" type="submit" style={{ margin: 15 }}>
                    Login
                </Button>
                <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
                    No Account? Register instead.
                </h6>
                <Nav.Link to="/register">
                    <Button variant="secondary">Register</Button>
                </Nav.Link>
            </Form>
        </div>
    );
}
