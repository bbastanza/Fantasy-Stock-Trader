import React, { useState, useEffect, useRef } from "react";
import { useHistory } from "react-router-dom";
import { useUpdateLogin, useUpdateUser } from "../contexts/LoginContext";
import { TweenMax, Power3 } from "gsap";
import { deleteUser } from "../helpers/userApiCalls";
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
        const data = await deleteUser({ userName: userName, password: password });

        if (!!data.ClientMessage) {
            setErrorMessage(data.ClientMessage);
            setIsLoading(false);
        } else {
            updateLogin(false);
            updateUser("");

            localStorage.clear();

            history.push("/login");
        }
    }

    return (
        <Modal>
            <div
                ref={el => (modalRef = el)}
                style={{ opacity: 0, color: "#FF3D50" }}
                className="dream-shadow login-container"
            >
                <h1 className="title" style={{ color: "#FF3D50" }}>
                    delete account
                </h1>
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
                                            placeholder="Username"
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
                                {!!errorMessage ? <p>{errorMessage}</p> : null}
                                <Button
                                    onClick={() => history.push("/splash")}
                                    variant="secondary"
                                    className="btn-shadow"
                                    style={{ margin: 15 }}
                                >
                                    Cancel
                                </Button>
                                <Button variant="danger" type="submit" className="btn-shadow" style={{ margin: 15 }}>
                                    I am sure I want to delete my account :(
                                </Button>
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
