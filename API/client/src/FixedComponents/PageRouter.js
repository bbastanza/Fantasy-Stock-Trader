import React from "react";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar";
import Welcome from "./../Pages/Welcome"
import Dashboard from "../Pages/Dashboard";
import Transactions from "./../Pages/Transactions";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Purchase from "./../Pages/Purchase";
import Sell from "./../Pages/Sell";
import Splash from "./../Pages/Splash";

export default function PageRouter() {
    return (
        <Router>
            <DreamTraderNavbar />
            <Switch>
                <Route path="/welcome" render={() => <Welcome />} />
                <Route path="/dashboard" render={props => <Dashboard {...props} />} />
                <Route path="/transactions" render={() => <Transactions />} />
                <Route path="/login" render={() => <Login />} />
                <Route path="/register" render={() => <Register />} />
                <Route path="/purchase" render={props => <Purchase {...props} />} />
                <Route path="/sell" render={props => <Sell {...props} />} />
                <Route path="/" render={() => <Splash />} />
            </Switch>
        </Router>
    );
}
