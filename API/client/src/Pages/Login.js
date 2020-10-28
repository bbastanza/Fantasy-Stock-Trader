import React from "react";
import Form from "react-bootstrap/Form";

export default function Login() {
    return (
        <div>
            <h1>Login</h1>
            <Form>
                <Form.Group controlId="exampleForm.ControlInput1">
                    <Form.Label>Email address</Form.Label>
                    <Form.Control type="email" placeholder="name@example.com" />
                </Form.Group>
            </Form>
        </div>
    );
}
