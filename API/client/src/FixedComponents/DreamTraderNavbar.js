import React from "react";
import { useHistory } from "react-router-dom";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import { LoginContext } from "./../contexts/LoginContext";
import Arrow2 from "../Images/arrow2.png";

export default function DreamTraderNavbar() {
    const history = useHistory();

    function logout() {
        console.log("logout");
    }

    function viewTransactions() {
        history.push("/transactions");
    }

    return (
        <LoginContext.Consumer>
            {context => {
                return context.isLoggedIn ? (
                    <Navbar collapseOnSelect expand="lg" bg="warning" variant="light">
                        <Navbar.Brand href="/" style={{ fontFamily: "Dream", color: "#313131" }}>
                            dream trader
                            <img height={20} src={Arrow2} alt="arrow" />
                        </Navbar.Brand>
                        <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                        <Navbar.Collapse id="responsive-navbar-nav">
                            <Nav className="mr-auto" />
                            <NavDropdown
                                title={context.isLoggedIn ? JSON.parse(localStorage.getItem("currentUser")) : "LogIn"}
                                id="basic-nav-dropdown"
                            >
                                <NavDropdown.Item onClick={logout}>Log Out</NavDropdown.Item>
                                <NavDropdown.Item onClick={viewTransactions}>View Transactions</NavDropdown.Item>
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
