import React, { createContext, useState, useEffect, useContext } from "react";

const LoginContext = createContext();
const LoginUpdateContext = createContext();

export function useLogin() {
    return useContext(LoginContext);
}

export function useUpdateLogin() {
    return useContext(LoginUpdateContext);
}

export default function LoginContextProvider({ children }) {
    const [loggedIn, setLoggedIn] = useState(false);

   useEffect(() => {
       (() => {
           if (!JSON.parse(localStorage.getItem("sessionId")))
                return;
            const expireString = JSON.parse(localStorage.getItem("expires"))
            const expireDateTime = new Date(expireString).getTime();
            if (expireDateTime > new Date().getTime()) setLoggedIn(true)
       })();
   }, []) 


    

    return (
        <LoginContext.Provider value={loggedIn}>
            <LoginUpdateContext.Provider value={setLoggedIn}>
                {children}
            </LoginUpdateContext.Provider>
        </LoginContext.Provider>
    );
}
