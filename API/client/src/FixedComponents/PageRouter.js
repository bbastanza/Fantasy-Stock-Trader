import React from "react";
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import { useLogin } from "./../contexts/LoginContext";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar";
import Welcome from "./../Pages/Welcome";
import Dashboard from "../Pages/Dashboard";
import Transactions from "./../Pages/Transactions";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Purchase from "./../Pages/Purchase";
import Sell from "./../Pages/Sell";
import Splash from "./../Pages/Splash";

export default function PageRouter() {
    const isLoggedIn = useLogin();
    return (
        <Router>
            <DreamTraderNavbar />
            <Switch>
                <Route
                    path="/dashboard"
                    render={props => (isLoggedIn ? <Dashboard {...props} /> : <Redirect to="/" />)}
                />
                <Route
                    path="/purchase"
                    render={props => (isLoggedIn ? <Purchase {...props} /> : <Redirect to="/" />)}
                />
                <Route 
                    path="/sell" 
                    render={props => (isLoggedIn ? <Sell {...props} /> : <Redirect to="/" />)} 
                />
                <Route 
                    path="/transactions" 
                    render={() => (isLoggedIn ? <Transactions /> : <Redirect to="/" />)} 
                />
                <Route path="/login" render={() => <Login />} />
                <Route path="/register" render={() => <Register />} />
                <Route path="/welcome" render={() => <Welcome />} />
                <Route path="/" render={() => <Splash />} />
            </Switch>
        </Router>
    );
}
