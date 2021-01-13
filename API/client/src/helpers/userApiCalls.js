import axios from "axios";
import { parseError } from "./errorHandling";

export async function loginUser(userInput) {
    let responseData;
    try {
        const request = await axios.post("/user/login", {
            userName: userInput.userName,
            password: userInput.password,
        });
        responseData = request.data;
    } catch (error) {
        responseData = parseError(error.response);
    }
    return responseData;
}

export async function logoutUser() {
    try {
        await axios.post("/user/logout", {
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
        });
    } catch (error) {
        console.log(error.response.data);
    }
}

export async function addUser(userInput) {
    let responseData;
    try {
        const request = await axios.post("/user/add", {
            userName: userInput.userName,
            password: userInput.password,
            email: userInput.email,
        });
        responseData = request.data;
    } catch (error) {
        responseData = parseError(error.response);
    }
    return responseData;
}

export async function deleteUser(userInput) {
    let responseData;
    try {
        const request = await axios.post("/user/delete", {
            userName: userInput.userName,
            password: userInput.password,
        });
        responseData = request.data;
    } catch (error) {
        responseData = parseError(error.response);
    }
    return responseData;
}

export async function getUserData() {
    let responseData;
    try {
        const request = await axios.post("/user/getUserData", {
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
        });
        responseData = request.data;
    } catch (error) {
        responseData = parseError(error.response);
    }
    return responseData;
}

export async function getUserTransactions() {
    let responseData;
    try {
        const request = await axios.post("/user/transactions", {
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
        });
        responseData = request.data;
    } catch (error) {
        responseData = parseError(error.response);
    }
    return responseData;
}
