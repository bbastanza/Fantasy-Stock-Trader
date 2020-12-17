import React from "react";

export default function CircleAnimation() {
    const circleStyle = {
        margin: "70px auto",
        width: 50,
        height: 50,
        border: "5px solid rgba(255,255,255,.2)",
        borderRadius: "50%",
        borderRightColor: "#ffc134",
    };

    return <div className="circle-animation" style={circleStyle} />;
}
