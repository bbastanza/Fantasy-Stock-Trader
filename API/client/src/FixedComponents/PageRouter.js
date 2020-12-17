import React from "react";
import { BrowserRouter as Router, Switch, Route} from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar"
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Splash from "./../Pages/Splash";
import Sell from "./../Pages/Sell"

export default function PageRouter({ availableFunds, allocatedFunds }) {

    return (
        <Router>
            <DreamTraderNavbar/>
            <Switch>
                <Route path="/purchase">
                    <Purchase availableFunds={availableFunds} />
                </Route>
                <Route path="/dashboard">
                    <Dashboard Funds={allocatedFunds} />
                </Route>
                <Route path="/login">
                    <Login />
                </Route>
                <Route path="/register">
                    <Register />
                </Route>
                <Route path="/sell" render={props => <Sell {...props} />}/>
                <Route path="/">
                    <Splash />
                </Route>
            </Switch>
        </Router>
    );
}
