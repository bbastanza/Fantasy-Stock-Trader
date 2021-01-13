export function parseError(errorResponse) {
    if (errorResponse.status === 401 && !!errorResponse.data.ClientMessage) {
        return { ClientMessage: "expired" };
    } else if (errorResponse.status === 418 || (errorResponse.status === 500 && !!errorResponse.data.ClientMessage)) {
        return errorResponse.data;
    } else {
        return { ClientMessage: "There was an unexpected error. Please try again." };
    }
}
