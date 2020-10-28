import React, { useState } from "react";
import DreamTraderNavbar from "./FixedComponents/DreamTraderNavbar";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";

function App() {
    const [availableFunds, setAvailableFunds] = useState(10000);
    const [allocatedFunds, setAllocatedFunds] = useState(9786);

    return (
        <div className="App">
            <DreamTraderNavbar
                availableFunds={availableFunds}
                allocatedFunds={allocatedFunds}
            />
        </div>
    );
}

export default App;
