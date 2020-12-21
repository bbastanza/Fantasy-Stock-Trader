import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar";
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Splash from "./../Pages/Splash";
import Sell from "./../Pages/Sell";
import Transactions from "./../Pages/Transactions";
import Welcome from "./../Pages/Welcome"

export default function PageRouter() {
    return (
        <Router>
            <DreamTraderNavbar />
            <Switch>
                <Route path="/purchase" render={props => <Purchase {...props} />} />
                <Route path="/sell" render={props => <Sell {...props} />} />
                <Route path="/dashboard" render={props => <Dashboard {...props} />} />
                <Route path="/transactions" render={() => <Transactions />} />
                <Route path="/login" render={() => <Login />} />
                <Route path="/register" render={() => <Register />} />
                <Route path="/welcome" render={() => <Welcome />} />
                <Route path="/" render={() => <Splash />} />
            </Switch>
        </Router>
    );
}
