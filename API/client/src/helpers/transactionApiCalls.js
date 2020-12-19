import axios from "axios";

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
