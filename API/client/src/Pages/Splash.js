import React from "react";
import SwiftSlider from "react-swift-slider";
import { Link } from "react-router-dom";
import stock1 from "../Images/1stock.jpg";
import stock2 from "../Images/2stock.jpg";
import stock3 from "../Images/3stock.jpg";
import stock4 from "../Images/4stock.jpg";
import Arrow from "../Images/arrow3.png"

export default function Splash() {
    const imgData = [
        { id: 1, src: stock1 },
        { id: 2, src: stock2 },
        { id: 3, src: stock3 },
        { id: 4, src: stock4 },
    ];

    return (
        <div style={{margin: 40}}>
            <div
                className="row"
                style={{ margin: "10% auto", alignItems: "center", display: "flex", textAlign: "center" }}>
                <div className="col-lg-6 col-md-12 dream-shadow" 
                     style={{ 
                            textAlign: "center", 
                            padding: 50, 
                            backgroundColor: "rgba(255, 255, 255, .05)", 
                            borderRadius: 10,
                            marginBottom: 20}}>
                    <h1 className="title">dream trader<img style={{height: 40}}src={Arrow} alt="arrow"/></h1>
                    <h5 style={{fontFamily: "monospace", color: "seashell"}}>
                        Welcome to dream trader! This is your place to test the market without having to invest real
                        money. Get started making your fantasy fortune today!
                    </h5>
                    <div>
                        <Link to="/Register">
                            <button className="btn btn-warning btn-lg btn-shadow" 
                                    style={{ margin: 20 }}>
                                Register
                            </button>
                        </Link>
                        <Link to="/Login">
                            <button className="btn btn-warning btn-lg btn-shadow" 
                                    style={{ margin: 20 }}>
                                Login
                            </button>
                        </Link>
                    </div>
                </div>
                <div className="col-lg-6 col-md-12" 
                     style={{ padding: "0 20px 0 50px", alignSelf: "center" }}>
                    <SwiftSlider 
                        height={400} 
                        interval={4000} 
                        data={imgData} 
                        enableNextAndPrev={false}
                        showDots={false} />
                </div>
            </div>
        </div>
    );
}
