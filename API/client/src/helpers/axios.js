const getHeaders = token => {
    const dreamTraderHeaders = {
        "content-type": "text/json",
        "bearer-token": token,
    };
    return dreamTraderHeaders;
};

async function addUser(userInput) {
    let responseData
    try {
        const request = await axios.post("/user/add", {
            headers: getHeaders(),
            params: {
                userName: userInput.userName,
                password: userInput.password,
                email: userInput.email,
            },
        });
        responseData = request.data;
    }
    catch {
        responseData = request.data;
    }
    return responseData;
}

async function deleteUser(userInput, token){
    let responseData
    try{
        const request = await axios.delete("/user/delete", {
            headers: getHeaders(token),
            params: {
                userName: userInput.userName,
                password: userInput.password,
            }
        })
        responseData = request.data
    }
    catch {
        responseData = request.data;
    }
    return responseData;
} 

async function getUserData(token){
    let responseData;
    try{
        const request = await axios.get("/user/get", {
            headers: getHeaders(token)
        })
        responseData = request.data
    }
    catch{
        responseData = request.data
    }
    return responseData;
}

async function getUserTransactions(token){
    let responseData;
    try{
        const request = await axios.get("/user/transactions", {
            headers: getHeaders(token)
        })
        responseData = request.data
    }
    catch{
        responseData = request.data
    }
    return responseData;
}

async function initializeTransaction(token, transactionData, type){
    let responseData;
    try{
        const request = await axios.post(`/transaction/${type}`, {
            headers: getHeaders(token),
            params: {
                symbol: transactionData.symbol,
                amount: transactionData.amount,
                sellAll: transactionData.sellAll
            }
        })
    }
    catch{
        responseData = request.data;
    }
    return responseData
}

async function getStockData(token, symbol){
    let responseData;
    try{
        const request = await axios.get(`/stockData/${symbol}`, {
            headers: getHeaders(token)
        })
        responseData = request.data
    }
    catch{
        responseData = request.data
    }
    return responseData;
}

export {addUser, deleteUser, getUserData, getUserTransactions, initializeTransaction, getStockData}