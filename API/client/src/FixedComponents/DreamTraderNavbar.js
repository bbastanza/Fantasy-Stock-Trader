import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect,
} from "react-router-dom";
import Portfolio from "./../Pages/Portfolio";
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";

export default function DreamTraderNavbar({
    availableFunds,
    allocatedFunds,
    isLoggedIn,
    setIsLoggedIn,
}) {
    return (
        <Router>
            {isLoggedIn ? (
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
            ) : null}

            <Switch>
                <Route path="/purchase">
                    <Purchase availableFunds={availableFunds} />
                </Route>
                <Route path="/portfolio">
                    <Portfolio allocatedFunds={allocatedFunds} />
                </Route>
                <Route path="/login">
                    <Login setIsLoggedIn={setIsLoggedIn} />
                </Route>
                <Route path="/register">
                    <Register setIsLoggedIn={setIsLoggedIn} />
                </Route>
                <Route path="/">
                    <Redirect to="/login" />
                </Route>
            </Switch>
        </Router>
    );
}
