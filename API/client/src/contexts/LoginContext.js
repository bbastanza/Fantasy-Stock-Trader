import React, { createContext, useState } from "react";

export const LoginContext = createContext();

export default function LoginContextProvider(props) {
    const [isLoggedIn, setIsLoggedIn] = useState(
        sessionStorage.getItem("isLoggedIn") === null ||
            sessionStorage.getItem("isLoggedIn") === false
            ? false
            : true
    );

    return (
        <LoginContext.Provider
            value={{
                isLoggedIn: isLoggedIn,
                setIsLoggedIn: setIsLoggedIn,
            }}
        >
            {props.children}
        </LoginContext.Provider>
    );
}
