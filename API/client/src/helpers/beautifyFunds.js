export function beautifyNumber(balance) {
        return parseFloat(balance)
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }