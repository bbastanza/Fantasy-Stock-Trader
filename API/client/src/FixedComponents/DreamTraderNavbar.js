import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { LoginContext } from "./../contexts/LoginContext";
import Arrow2 from "../Images/arrow2.png";

export default function DreamTraderNavbar() {
    return (
        <LoginContext.Consumer>
            {context => {
                return context.isLoggedIn ? (
                    <Navbar collapseOnSelect expand="lg" bg="warning" variant="light">
                        <Navbar.Brand href="/" style={{ fontFamily: "Dream" }}>
                            dream trader
                            <img height={20} src={Arrow2} alt="arrow" />
                        </Navbar.Brand>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="mr-auto" />
                            <NavDropdown style={{color: "grey"}}title={context.isLoggedIn ? "Brian" : "LogIn"} id="basic-nav-dropdown">
                                <NavDropdown.Item onClick={()=> console.log("Log out")}>Log Out</NavDropdown.Item>
                                <NavDropdown.Item onClick={()=> console.log("Go to transaction page")}>View Transactions</NavDropdown.Item>
                            </NavDropdown>
                            <Nav>
                                <Nav.Link href={context.isLoggedIn ? "/dashboard" : "/login"}>Dashboard</Nav.Link>
                            </Nav>
                        </Navbar.Collapse>
                    </Navbar>
                ) : null;
            }}
        </LoginContext.Consumer>
    );
}
