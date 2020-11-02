import React, { useState } from "react";
import DreamTraderNavbar from "./FixedComponents/DreamTraderNavbar";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import LoginContextProvider from "./contexts/LoginContext";
import PageRouter from "./FixedComponents/PageRouter";

function App() {
    const [availableFunds, setAvailableFunds] = useState(10000);
    const [allocatedFunds, setAllocatedFunds] = useState(986);

    return (
        <div className="App">
            <LoginContextProvider>
                <DreamTraderNavbar />
            </LoginContextProvider>
            <PageRouter
                availableFunds={availableFunds}
                allocatedFunds={allocatedFunds}
            />
        </div>
    );
}

export default App;
