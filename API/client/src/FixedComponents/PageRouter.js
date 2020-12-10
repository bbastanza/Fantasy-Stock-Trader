import React from "react";
import { BrowserRouter as Router, Switch, Route} from "react-router-dom";
import Portfolio from "./../Pages/Portfolio";
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Splash from "./../Pages/Splash"

export default function PageRouter({ availableFunds, allocatedFunds }) {
    return (
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
                    <Splash />
                </Route>
            </Switch>
        </Router>
    );
}
