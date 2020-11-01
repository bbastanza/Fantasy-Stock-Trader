import React, { createContext, useState } from "react";

export const LoginContext = createContext();

export default function LoginContextProvider(props) {
    const [isLoggedIn, setIsLoggedIn] = useState(
        !sessionStorage.getItem("isLoggedIn")
            ? false
            : sessionStorage.getItem("isLoggedIn")
    );
    console.log(isLoggedIn);
    return (
        <LoginContext.Provider
            value={{ isLoggedIn: isLoggedIn, setIsLoggedIn: setIsLoggedIn }}
        >
            {props.children}
        </LoginContext.Provider>
    );
}
