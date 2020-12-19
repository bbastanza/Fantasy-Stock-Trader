import React, { useEffect, useState } from "react"

export default function Pagination({transactionCount, changePage}){
    const [buttonCount, setButtonCount] = useState(1)

    useEffect(() => {
        let count = 0;
        for (let i = 0; i < trancationCount; i + 5){
            count++
        }
        setButtonCount(count)
    }, [])

    return(
        <>
            {buttonCount > 1 ?
             for (let i = 0; i <= buttonCount; i++){
                return <button onClick={() => changePage(i)}>{i}</button>
             } : null}
        </>
    )
}
