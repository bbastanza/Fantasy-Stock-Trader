import React, { useState, useEffect } from "react";
import { addUser } from "../helpers/userApiCalls";
import { useHistory } from "react-router-dom";
import { useUpdateLogin, useUpdateUser } from "./../contexts/LoginContext";
import { Link } from "react-router-dom";
import Modal from "./../FixedComponents/Modal";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import DotAnimation from "./../IndividualComponents/DotAnimation";

export default function Register() {
    const updateLogin = useUpdateLogin();
    const updateUser = useUpdateUser();
    const history = useHistory();
    const [email, setEmail] = useState("");
    const [userName, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [confirmPassword, setConfirmPassword] = useState("");
    const [matchPassword, setMatchPassword] = useState(true);
    const [isLoading, setIsLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState(false);

    async function handleSubmit(e) {
        e.preventDefault();
        setIsLoading(true);
        if (!matchPassword) {
            setIsLoading(false)
            return;
        }

        if (email.length === 0 || password.length === 0 || email.length === 0) {
            setErrorMessage("Please fill our all fields.");
            setIsLoading(false);
            return;
        }

        const data = await addUser({ userName: userName, password: password, email: email });
        if (data.sessionId) {
            updateLogin(true);
            updateUser(userName);
            localStorage.setItem("sessionId", JSON.stringify(data.sessionId));
            localStorage.setItem("expires", JSON.stringify(data.expireTime));
            localStorage.setItem("currentUser", JSON.stringify(userName));

            history.push("/welcome");
        } else {
            setErrorMessage(data.ClientMessage);
        }
        setIsLoading(false);
    }

    useEffect(() => {
        if (password !== confirmPassword && password.length <= confirmPassword.length) setMatchPassword(false);
        else setMatchPassword(true);
    }, [password, confirmPassword]);

    const nonMatchingStyle = { backgroundColor: "#ffb3b9" };

    return (
        <>
            <Modal>
                <div className="dream-shadow login-container">
                    <h1 className="title">register</h1>
                    {!isLoading ? (
                        <>
                            <Form onSubmit={e => handleSubmit(e)}>
                                <Form.Group as={Row}>
                                    <Form.Label column sm="3">
                                        Username
                                    </Form.Label>
                                    <Col sm="9">
                                        <Form.Control
                                            required={true}
                                            type="text"
                                            placeholder="Enter Username"
                                            onChange={e => setUsername(e.target.value)}
                                        />
                                    </Col>
                                </Form.Group>
                                <Form.Group as={Row}>
                                    <Form.Label column sm="3">
                                        Email address
                                    </Form.Label>
                                    <Col sm="9">
                                        <Form.Control
                                            type="email"
                                            placeholder="Enter email"
                                            onChange={e => setEmail(e.target.value)}
                                        />
                                    </Col>
                                </Form.Group>
                                <p className="text-muted" style={{ fontSize: 12 }}>
                                    We'll never share your email with anyone else.
                                </p>

                                <Form.Group as={Row}>
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

                                <Form.Group as={Row}>
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

                                {errorMessage.length > 0 ? <p>{errorMessage}</p> : null}

                                <Button variant="info" type="submit" className="btn-shadow" style={{ margin: 15 }}>
                                    Register
                                </Button>

                                <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
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
                        </>
                    ) : (
                        <DotAnimation />
                    )}
                </div>
            </Modal>
        </>
    );
}
