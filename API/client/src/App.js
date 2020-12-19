import React from "react";
import Footer from "./FixedComponents/Footer";
import "./App.css";
import "bootstrap/dist/css/bootstrap.css";
import LoginContextProvider from "./contexts/LoginContext";
import PageRouter from "./FixedComponents/PageRouter";

function App() {
    return (
        <div className="App">
            <LoginContextProvider>
                <PageRouter />
            </LoginContextProvider>
            <Footer />
        </div>
    );
}

export default App;
