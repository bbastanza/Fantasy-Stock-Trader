import React, { useState, useRef, useEffect } from "react";
import { useHistory } from "react-router-dom";
import { useCurrentUser } from "./../contexts/LoginContext";
import { TweenMax, Power3 } from "gsap";
import Modal from "./../FixedComponents/Modal";
import buyImage from "./../Images/buy.png";
import sellImage from "./../Images/sell.png";
import moneyBagImage from "./../Images/moneybag.png";
import cashImage from "./../Images/cash.png";

export default function Welcome() {
    const currentUser = useCurrentUser();
    const history = useHistory();
    let displayRef = useRef(null);
    const [slideNumber, setSlideNumber] = useState(0);
    const [buttonText, setButtonText] = useState("Next");
    const images = [moneyBagImage, buyImage, sellImage, cashImage];
    const textContent = [`Welcome to Dream Trader ${currentUser}!`, "Buy Low.", "Sell High.", "Stack Cash!"];

    useEffect(() => {
        TweenMax.to(displayRef, .5, {
            opacity: 1,
            y: 15,
            ease: Power3.easeOut,
        });
    }, [slideNumber]);

    function handleClick() {
        TweenMax.to(displayRef, 0, {
            opacity: 0,
            y: 0,
        });
        slideNumber === images.length - 1 ? history.push("dashboard") : setSlideNumber(slideNumber + 1);
        if (slideNumber === images.length - 2) setButtonText("Let's get stared!");
    }

    return (
        <Modal>
            <div ref={el => (displayRef = el)} style={{ opacity: 0}}>
                <img src={images[slideNumber]} alt={textContent[slideNumber]} style={{ height: 300, width: "auto" }} />
                <h2 style={{ padding: 20 }}>{textContent[slideNumber]}</h2>
                <button onClick={handleClick} className="btn btn-lg btn-info dream-btn m-1">
                    {buttonText}
                </button>
            </div>
        </Modal>
    );
}
