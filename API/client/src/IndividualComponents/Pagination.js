import React from "react";

export default function Pagination({ transactionsPerPage, totalPages, changePage }) {
    const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalPages / transactionsPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <>
            {totalPages > 6 ? (
                <div style={{ backgroundColor: "#ffdc91", 
                              width: totalPages * 12, 
                              margin: "auto", 
                              borderRadius: 7 }}>
                    {pageNumbers.map(number => (
                        <button key={number} className="btn btn-info m-1" onClick={() => changePage(number)}>
                            {number}
                        </button>
                    ))}
                </div>
            ) : null}
        </>
    );
}
