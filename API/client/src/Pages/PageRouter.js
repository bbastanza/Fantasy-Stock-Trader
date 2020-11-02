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
import { LoginContext } from "./../contexts/LoginContext";

export default function PageRouter({ availableFunds, allocatedFunds }) {
    return (
        <LoginContext.Consumer>
            <Router>
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
                    <Route path="/register">
                        <Register />
                    </Route>
                    <Route path="/">
                        <Redirect to="/login" />
                    </Route>
                </Switch>
            </Router>
        </LoginContext.Consumer>
    );
}
