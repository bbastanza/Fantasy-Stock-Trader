export function beautifyNumber(number) {
        return parseFloat(number)
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }