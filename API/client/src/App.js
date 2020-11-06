import React, { useState } from "react";
import DreamTraderNavbar from "./FixedComponents/DreamTraderNavbar";
import axios from "axios";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import LoginContextProvider from "./contexts/LoginContext";
import PageRouter from "./FixedComponents/PageRouter";

function App() {
    const [availableFunds, setAvailableFunds] = useState(10000);
    const [allocatedFunds, setAllocatedFunds] = useState(986);
    const [userHoldings, setUserHoldings] = useState([]);

    async function getUserHoldings(userName) {
        try {
            const response = await axios.get(`/users/${userName}`);
            console.log(response);
            allocateResposeData(response.data);
        } catch {
            // error handling
        }
    }

    function allocateResponseData(responseData) {
        setAvailableFunds([...responseData.unallocated]);
        setUserHoldings(responseData.holdings);

        let allocatedFunds = 0;
        for (const holding of response.data.holdings) {
            allocatedFunds += holding.value;
        }
        setAllocatedFunds(allocatedFunds);
    }

    return (
        <div className="App">
            <LoginContextProvider>
                <DreamTraderNavbar />
                <PageRouter
                    availableFunds={availableFunds}
                    allocatedFunds={allocatedFunds}
                    userHoldings={userHoldings}
                />
            </LoginContextProvider>
        </div>
    );
}

export default App;
