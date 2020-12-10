import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { LoginContext } from "./../contexts/LoginContext";
import Arrow2 from "../Images/arrow2.png"

export default function DreamTraderNavbar({isLoggedIn}) {
    return (
        <LoginContext.Consumer>
            {context => {
                return context.isLoggedIn ? (
                    <Navbar collapseOnSelect expand="lg" bg="warning" variant="light">
                        <Navbar.Brand href="/" style={{fontFamily: "Dream"}}>dream trader<img height={20} src={Arrow2} alt="arrow"/></Navbar.Brand>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="mr-auto" />
                            <Nav>
                                <Nav.Link 
                                    href={context.isLoggedIn ? "/purchase" : "/login"}>Purchase</Nav.Link>
                                <Nav.Link 
                                    href={context.isLoggedIn ? "/portfolio" : "/login"}>Portfolio</Nav.Link>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                ) : null;
            }}
        </LoginContext.Consumer>
    );
}
