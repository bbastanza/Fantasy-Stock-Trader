import React, { createContext, useState, useRef, useContext } from "react";

const LoginContext = createContext();
const LoginUpdateContext = createContext();

export function useLogin() {
    return useContext(LoginContext);
}

export function useUpdateLogin() {
    return useContext(LoginUpdateContext);
}

export default function LoginContextProvider({ children }) {
    // const [loggedIn, setLoggedIn] = useState(false);
    let loggedIn = useRef(false)

    function setLoggedIn(bool){
        loggedIn.current = bool
    }

    

    return (
        <LoginContext.Provider value={loggedIn}>
            <LoginUpdateContext.Provider value={setLoggedIn}>
                {children}
            </LoginUpdateContext.Provider>
        </LoginContext.Provider>
    );
}
