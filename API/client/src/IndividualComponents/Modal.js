import React from "react";
import ReactDOM from "react-dom"

const modalStyle = {
    position: "fixed",
    top: "50%",
    left: "50%",
    maxHeight: "100vh",
    overflowY: "auto",
    transform: "translate(-50%, -50%)",
    zIndex: 1000,
    color: "#ffc107",
    width: "80vw",
    textAlign: "center",

};

const overlayStyle = {
    position: "fixed",
    top: 0,
    left: 0,
    right: 0,
    bottom: 0,
    backgroundColor: "rgba(0,0,0,.7)",
    zIndex: 1000,
};

export default function Modal({ children }) {
    return ReactDOM.createPortal(
        <>
            <div style={overlayStyle} />
            <div style={modalStyle}>{children}</div>
        </>
    ,
    document.getElementById("portal"))
}
