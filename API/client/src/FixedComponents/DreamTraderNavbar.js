import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { LoginContext } from "./../contexts/LoginContext";

export default function DreamTraderNavbar() {
    return (
        <LoginContext.Consumer>
            {context => {
                console.log(
                    "From DreamTraderNavbar.js Context: " + context.isLoggedIn
                );
                console.log(
                    "From DreamTraderNavbar.js Session Storage: " +
                        sessionStorage.getItem("isLoggedIn")
                );
                return context.isLoggedIn ? (
                    <Navbar
                        collapseOnSelect
                        expand="lg"
                        bg="warning"
                        variant="light"
                    >
                        <Navbar.Brand href="/">Dream Trader</Navbar.Brand>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="mr-auto" />
                            <Nav>
                                <Nav.Link href="/purchase">Purchase</Nav.Link>
                                <Nav.Link eventKey={2} href="/portfolio">
                                    Portfolio
                                </Nav.Link>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                ) : null;
            }}
        </LoginContext.Consumer>
    );
}
