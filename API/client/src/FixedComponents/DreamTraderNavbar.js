import React from "react";
import Nav from "react-bootstrap/Nav";
import Navbar from "react-bootstrap/Navbar";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Portfolio from "./../Pages/Portfolio";
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";

export default function DreamTraderNavbar({ availableFunds, allocatedFunds }) {
    return (
        <Router>
            <Navbar collapseOnSelect expand="lg" bg="warning" variant="light">
                <Navbar.Brand href="/login">Dream Trader</Navbar.Brand>
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
            <Switch>
                <Route path="/purchase">
                    <Purchase availableFunds={availableFunds} />
                </Route>
                <Route path="/portfolio">
                    <Portfolio allocatedFunds={allocatedFunds} />
                </Route>
                <Route path="/login">
                    <Login />
                </Route>
            </Switch>
        </Router>
    );
}
