import React from "react";
import { BrowserRouter as Router, Switch, Route, Redirect } from "react-router-dom";
import { useLogin } from "./../contexts/LoginContext";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar";
import Welcome from "./../Pages/Welcome";
import Dashboard from "../Pages/Dashboard";
import Transactions from "./../Pages/Transactions";
import Login from "./../Modals/Login";
import Register from "./../Modals/Register";
import Purchase from "./../Modals/Purchase";
import Sell from "./../Modals/Sell";
import Splash from "./../Pages/Splash";
import ExpiredSession from "./../Modals/ExpiredSession"
import DeleteUser from "./../Modals/DeleteUser"

export default function PageRouter() {
    const isLoggedIn = useLogin();
    return (
        <Router>
            <DreamTraderNavbar />
            <Switch>
                <Route path="/expired" render={() => <ExpiredSession />} />
                <Route path="/delete_account" render={() => <DeleteUser />} />
                <Route path="/dashboard" render={() => (isLoggedIn ? <Dashboard /> : <Splash />)} />
                <Route path="/purchase" render={props => (isLoggedIn ? <Purchase {...props} /> : <Splash />)} />
                <Route path="/sell" render={props => (isLoggedIn ? <Sell {...props} /> : <Splash />)} />
                <Route path="/transactions" render={() => (isLoggedIn ? <Transactions /> : <Splash />)} />
                <Route path="/login" render={() => <Login />} />
                <Route path="/register" render={() => <Register />} />
                <Route path="/welcome" render={() => <Welcome />} />
                <Route exact path="/" render={() => <Splash />} />
                <Route path="/">
                    <Redirect to="/" />
                </Route>
            </Switch>
        </Router>
    );
}
