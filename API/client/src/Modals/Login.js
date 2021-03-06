import React, { useState, useEffect, useRef } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";
import { loginUser } from "../helpers/userApiCalls";
import { useUpdateLogin, useUpdateUser } from "../contexts/LoginContext";
import { TweenMax, Power3 } from "gsap";
import Modal from "../IndividualComponents/Modal";
import Form from "react-bootstrap/Form";
import Row from "react-bootstrap/Row";
import Col from "react-bootstrap/Col";
import Button from "react-bootstrap/Button";
import DotAnimation from "../IndividualComponents/DotAnimation";

export default function Login() {
    const history = useHistory();
    const [userName, setUsername] = useState("");
    const [password, setPassword] = useState("");
    const [isLoading, setIsLoading] = useState(false);
    const [errorMessage, setErrorMessage] = useState("");
    const updateLogin = useUpdateLogin();
    const updateUser = useUpdateUser();
    let modalRef = useRef(null);

    useEffect(() => {
        TweenMax.to(modalRef, 1, {
            opacity: 1,
            y: -20,
            ease: Power3.easeOut,
        });
    }, []);

    async function handleSubmit(e) {
        e.preventDefault();
        setErrorMessage("");
        setIsLoading(true);

        const canSubmit = !!userName && !!password;

        if (!canSubmit) {
            setErrorMessage("Please fill our all fields.");
            setIsLoading(false);
            return;
        }
        const data = await loginUser({ userName: userName, password: password });

        if (!!data.sessionId) {
            updateLogin(true);
            updateUser(userName);

            localStorage.setItem("sessionId", JSON.stringify(data.sessionId));
            localStorage.setItem("expires", JSON.stringify(data.expireTime));
            localStorage.setItem("currentUser", JSON.stringify(userName));

            history.push("/dashboard");
        } else {
            setErrorMessage(data.ClientMessage);
            setIsLoading(false);
        }
    }

    return (
        <Modal>
            <div ref={el => (modalRef = el)} style={{ opacity: 0 }} className="dream-shadow login-container">
                <h1 className="title">login</h1>
                <>
                    {!isLoading ? (
                        <>
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
                                            onChange={e => {
                                                if (e.target.value.length === 0) setPassword(null);
                                                else setPassword(e.target.value);
                                            }}
                                        />
                                    </Col>
                                </Form.Group>{" "}
                                {!!errorMessage ? <p>{errorMessage}</p> : null}
                                <Button variant="info" type="submit" className="btn-shadow" style={{ margin: 15 }}>
                                    Log In
                                </Button>
                                <h6 className="text-muted" style={{ padding: "20px 0 5px" }}>
                                    No Account? Register Now!
                                </h6>
                                <Link to="/register">
                                    <Button variant="secondary">Register</Button>
                                </Link>
                            </Form>
                        </>
                    ) : (
                        <DotAnimation />
                    )}
                </>
            </div>
        </Modal>
    );
}
