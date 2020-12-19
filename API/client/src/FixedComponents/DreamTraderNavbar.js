import React from "react";
import { useLogin, useUpdateLogin } from "./../contexts/LoginContext";
import { useHistory } from "react-router-dom";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import NavDropdown from "react-bootstrap/NavDropdown";
import Arrow2 from "../Images/arrow2.png";

export default function DreamTraderNavbar() {
    const history = useHistory();
    const loggedIn = useLogin();
    const updateLogin = useUpdateLogin();
    let currentUser = JSON.parse(localStorage.getItem("currentUser"));

    function logout() {
        updateLogin(false);
        localStorage.clear();
        history.push("/splash");
    }

    function viewTransactions() {
        history.push("/transactions");
    }

    return (
        <>
            {loggedIn.current ? (
                <Navbar collapseOnSelect expand="lg" bg="warning" variant="light">
                    <Navbar.Brand href="/" style={{ fontFamily: "Dream", color: "#313131" }}>
                        dream trader
                        <img height={20} src={Arrow2} alt="arrow" />
                    </Navbar.Brand>
                    <Navbar.Toggle aria-controls="responsive-navbar-nav" />
                    <Navbar.Collapse id="responsive-navbar-nav">
                        <Nav className="mr-auto" />
                        <NavDropdown title={currentUser} id="basic-nav-dropdown">
                            <NavDropdown.Item onClick={logout}>Log Out</NavDropdown.Item>
                            <NavDropdown.Item onClick={viewTransactions}>View Transactions</NavDropdown.Item>
                        </NavDropdown>
                        <Nav>
                            <Nav.Link href="/dashboard">Dashboard</Nav.Link>
                        </Nav>
                    </Navbar.Collapse>
                </Navbar>
            ) : null}
        </>
    );
}
