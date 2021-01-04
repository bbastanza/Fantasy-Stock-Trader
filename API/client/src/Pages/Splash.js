import React, { useRef, useEffect } from "react";
import { Link } from "react-router-dom";
import { useLogin, useUpdateLogin, useUpdateUser } from "./../contexts/LoginContext";
import { TweenMax, Power3 } from "gsap";
import SwiftSlider from "react-swift-slider";
import stock1 from "../Images/1stock.jpg";
import stock2 from "../Images/2stock.jpg";
import stock3 from "../Images/3stock.jpg";
import stock4 from "../Images/4stock.jpg";
import Arrow from "../Images/arrow3.png";

export default function Splash() {
    let textRef = useRef(null);
    let imageRef = useRef(null);
    const loggedIn = useLogin();
    const updateLogin = useUpdateLogin();
    const updateUser = useUpdateUser();
    const imgData = [
        { id: 1, src: stock1 },
        { id: 2, src: stock2 },
        { id: 3, src: stock3 },
        { id: 4, src: stock4 },
    ];

    useEffect(() => {
        TweenMax.to(textRef, 3, {
            opacity: 1,
            ease: Power3.easeOut,
        });
        TweenMax.to(imageRef, 3, {
            opacity: 1,
            ease: Power3.easeOut,
        });
    }, []);

    function logout() {
        localStorage.clear();
        updateUser("");
        updateLogin(false);
    }

    return (
        <div style={{ margin: 40 }}>
            <div
                className="row"
                style={{ margin: "10% auto", alignItems: "center", display: "flex", textAlign: "center" }}
            >
                <div
                    ref={el => (textRef = el)}
                    className="col-lg-6 col-md-12 user-holding-container dream-shadow"
                    style={{
                        textAlign: "center",
                        padding: 50,
                        borderRadius: 10,
                        marginBottom: 20,
                        opacity: 0,
                    }}
                >
                    <h1 className="title">
                        dream
                        <br />
                        trader
                        <img style={{ height: 40 }} src={Arrow} alt="arrow" />
                    </h1>
                    <h5 style={{ fontFamily: "monospace", color: "seashell" }}>
                        Welcome to dream trader! This is your place to test the market without having to invest real
                        money. Get started making your fantasy fortune today!
                    </h5>
                    {!loggedIn ? (
                        <div>
                            <Link to="/Register">
                                <button className="btn btn-info btn-lg dream-btn" style={{ margin: 20 }}>
                                    Register
                                </button>
                            </Link>
                            <Link to="/Login">
                                <button className="btn btn-info btn-lg dream-btn" style={{ margin: 20 }}>
                                    Login
                                </button>
                            </Link>
                        </div>
                    ) : (
                        <button className="btn btn-secondary btn-lg dream-btn" style={{ margin: 20 }} onClick={logout}>
                            Logout
                        </button>
                    )}
                </div>
                <div
                    ref={el => (imageRef = el)}
                    className="col-lg-6 col-md-12"
                    style={{ opacity: 0, padding: "0 20px 0 50px", alignSelf: "center" }}
                >
                    <SwiftSlider
                        height={400}
                        interval={4000}
                        data={imgData}
                        enableNextAndPrev={false}
                        showDots={false}
                    />
                </div>
            </div>
        </div>
    );
}
