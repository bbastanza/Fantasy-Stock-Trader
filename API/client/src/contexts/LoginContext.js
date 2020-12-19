import React, { createContext, useState, useEffect, useContext } from "react";

const LoginContext = createContext();
const LoginUpdateContext = createContext();
const CurrentUserContext = createContext();
const UpdateUserContext = createContext();

export function useLogin() {
    return useContext(LoginContext);
}

export function useUpdateLogin() {
    return useContext(LoginUpdateContext);
}

export function useCurrentUser() {
    return useContext(CurrentUserContext);
}

export function useUpdateUser() {
    return useContext(UpdateUserContext);
}

export default function LoginContextProvider({ children }) {
    const [loggedIn, setLoggedIn] = useState(false);
    const [currentUser, setCurrentUser] = useState("");

    useEffect(() => {
        (() => {
            if (!JSON.parse(localStorage.getItem("sessionId"))) return;
            const expireString = JSON.parse(localStorage.getItem("expires"));
            const expireDateTime = new Date(expireString).getTime();
            if (expireDateTime > new Date().getTime()) setLoggedIn(true);
            setCurrentUser(JSON.parse(localStorage.getItem("currentUser")))
        })();
    }, []);

    return (
        <LoginContext.Provider value={loggedIn}>
            <LoginUpdateContext.Provider value={setLoggedIn}>
                <CurrentUserContext.Provider value={currentUser}>
                <UpdateUserContext.Provider value={setCurrentUser}>
                    {children}
                </UpdateUserContext.Provider>
                </CurrentUserContext.Provider>
            </LoginUpdateContext.Provider>
        </LoginContext.Provider>
    );
}
