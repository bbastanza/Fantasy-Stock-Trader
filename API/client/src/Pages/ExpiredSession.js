import React, { useEffect, useRef } from "react";
import { useHistory } from "react-router";
import Modal from "./../FixedComponents/Modal";
import DotAnimation from "./../IndividualComponents/DotAnimation";
import { TweenMax, Power3 } from "gsap";

export default function ExpiredSession() {
    let modalRef = useRef(null);
    const history = useHistory();

    useEffect(() => {
        TweenMax.to(modalRef, 1, {
            opacity: 1,
            y: -20,
            ease: Power3.easeOut,
        });
    }, []);

    setTimeout(() => history.push("/login"), 3500);

    return (
        <Modal>
            <div ref={el => (modalRef = el)} style={{ opacity: 0 }} className="dream-shadow login-container">
                <h1 className="title">session expired</h1>
                <h2>Redirecting to Login</h2>
                <DotAnimation />
            </div>
        </Modal>
    );
}
