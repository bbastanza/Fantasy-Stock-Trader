import React from "react";
import Portfolio from "./Portfolio";
import Purchase from "./Purchase";
import Login from "./Login";
import Register from "./Register";
import {
    BrowserRouter as Router,
    Switch,
    Route,
    Redirect,
} from "react-router-dom";

export default function PageRouter({ availableFunds, allocatedFunds }) {
    return (
        <Router>
            <Switch>
                <Route exact path="/purchase">
                    <Purchase availableFunds={availableFunds} />
                </Route>
                <Route exact path="/portfolio">
                    <Portfolio allocatedFunds={allocatedFunds} />
                </Route>
                <Route exact path="/login">
                    <Login />
                </Route>
                <Route exact path="/register">
                    <Register />
                </Route>
                <Route exact path="/">
                    <Redirect to="/login" />
                </Route>
            </Switch>
        </Router>
    );
}
