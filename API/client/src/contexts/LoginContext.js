import React, { createContext, useState } from "react";

export const LoginContext = createContext();

export default function LoginContextProvider(props) {
    const [isLoggedIn, setIsLoggedIn] = useState(
        sessionStorage.getItem("isLoggedIn") === null
            ? false
            : sessionStorage.getItem("isLoggedIn")
    );
    console.log(
        "From LoginContext Session Storage: " +
            sessionStorage.getItem("isLoggedIn")
    );
    console.log(
        "From LoginContext State: " + sessionStorage.getItem("isLoggedIn")
    );
    return (
        <LoginContext.Provider
            value={{ isLoggedIn: isLoggedIn, setIsLoggedIn: setIsLoggedIn }}
        >
            {props.children}
        </LoginContext.Provider>
    );
}
