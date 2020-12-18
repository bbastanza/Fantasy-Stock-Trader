import axios from "axios";

export async function loginUser(userInput) {
    let responseData;
    try {
        const request = await axios.post("/user/login", {
            userName: userInput.userName,
            password: userInput.password,
        });
        responseData = request.data;
    } catch (error) {
        responseData = error.response.data;
    }
    return responseData;
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
        responseData = error.response.data;
    }
    return responseData;
}

export async function deleteUser(userInput) {
    let responseData;
    try {
        const request = await axios.delete("/user/delete", {
            userName: userInput.userName,
            password: userInput.password,
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
        });
        responseData = request.data;
    } catch (error) {
        responseData = error.response.data;
    }
    return responseData;
}

export async function getUserData() {
    let responseData;
    try {
        const request = await axios.post("/user/get", {
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
        });
        responseData = request.data;
    } catch (error) {
        responseData = error.response.data;
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
        responseData = error.response.data;
    }
    return responseData;
}

export async function initializeSale(saleData) {
    let responseData;
    try {
        const request = await axios.post(`/transaction/sell`, {
            sessionId: JSON.parse(localStorage.getItem("sessionId")),
            symbol: saleData.symbol,
            shareAmount: saleData.shareAmount,
            sellAll: saleData.sellAll,
        });
        responseData = request.data
    } catch (error) {
        responseData = error.response.data;
    }
    return responseData;
}

export async function initializePurchase(purchaseData) {
    let responseData;
    try {
        const request = await axios.post(`/transaction/purchase`, {
                sessionId: JSON.parse(localStorage.getItem("sessionId")),
                symbol: purchaseData.symbol,
                amount: purchaseData.amount,
        });
        responseData = request.data
    } catch (error) {
        responseData = error.response.data;
    }
    return responseData;
}

export async function getStockData(symbol) {
    let responseData;
    try {
        const request = await axios.get(`/stockData/${symbol}`);
        responseData = request.data;
    } catch (error) {
        responseData = error.response.data;
    }
    return responseData;
}
