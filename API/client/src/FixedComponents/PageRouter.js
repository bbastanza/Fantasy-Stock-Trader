import React from "react";
import { BrowserRouter as Router, Switch, Route} from "react-router-dom";
import Dashboard from "../Pages/Dashboard";
import DreamTraderNavbar from "./../FixedComponents/DreamTraderNavbar"
import Purchase from "./../Pages/Purchase";
import Login from "./../Pages/Login";
import Register from "./../Pages/Register";
import Splash from "./../Pages/Splash";
import Sell from "./../Pages/Sell"
import Transactions from "./../Pages/Transactions"

export default function PageRouter() {

    return (
        <Router>
            <DreamTraderNavbar/>
            <Switch>
                <Route path="/purchase">
                    <Purchase />
                </Route>
                <Route path="/transactions" render={() => <Transactions/>}/>
                <Route path="/dashboard" render={props => <Dashboard {...props} />} />
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
