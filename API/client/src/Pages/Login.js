import React, { useState } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";
import { loginUser } from "../helpers/userApiCalls";
import { useUpdateLogin, useUpdateUser } from "./../contexts/LoginContext";
import Modal from "./../FixedComponents/Modal";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import DotAnimation from "./../IndividualComponents/DotAnimation";

export default function Login() {
    const history = useHistory();
    const [userName, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [loginError, setLoginError] = useState(false);
    const updateLogin = useUpdateLogin();
    const updateUser = useUpdateUser();


    async function handleSubmit(e) {
        e.preventDefault();
        setLoginError(false);
        setIsLoading(true);

        const data = await loginUser({ userName: userName, password: password });

        if (data.sessionId) {
            updateLogin(true);
            updateUser(userName)
            localStorage.setItem("sessionId", JSON.stringify(data.sessionId));
            localStorage.setItem("expires", JSON.stringify(data.expireTime));
            localStorage.setItem("currentUser", JSON.stringify(userName));

            history.push("/dashboard");
        } else {
            // TODO handle error
            setLoginError(true);
            setIsLoading(false);
        }
    }

    return (
        <Modal>
            <div className="dream-shadow login-container">
                <h1 className="title">login</h1>
                <Form onSubmit={e => handleSubmit(e)}>
                    <Form.Group as={Row}>
                        <Form.Label column sm="3">
                            Username
                        </Form.Label>
                        <Col sm="9">
                            <Form.Control
                                type="text"
                                placeholder="Enter Username"
                                onChange={e => setUsername(e.target.value)}
                            />
                        </Col>
                    </Form.Group>
                    <Form.Group as={Row}>
                        <Form.Label column sm="3">
                            Password
                        </Form.Label>
                        <Col sm="9">
                            <Form.Control
                                type="password"
                                placeholder="Password"
                                onChange={e => setPassword(e.target.value)}
                            />
                        </Col>
                    </Form.Group>{" "}
                    {!isLoading ? (
                        <>
                            <Button variant="info" type="submit" className="btn-shadow" style={{ margin: 15 }}>
                                Log In
                            </Button>
                            <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
                                No Account? Register Now!
                            </h6>
                            <Link to="/register">
                                <Button variant="secondary">Register</Button>
                            </Link>
                        </>
                    ) : (
                        <DotAnimation />
                    )}
                </Form>
            </div>
        </Modal>
    );
}
