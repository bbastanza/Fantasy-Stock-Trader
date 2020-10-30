import React, { useState } from "react";
import DreamTraderNavbar from "./FixedComponents/DreamTraderNavbar";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";

function App() {
    const [availableFunds, setAvailableFunds] = useState(10000);
    const [allocatedFunds, setAllocatedFunds] = useState(986);
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    console.log(isLoggedIn);
    return (
        <div className="App">
            <DreamTraderNavbar
                availableFunds={availableFunds}
                allocatedFunds={allocatedFunds}
                isLoggedIn={isLoggedIn}
                setIsLoggedIn={setIsLoggedIn}
            />
        </div>
    );
}

export default App;
