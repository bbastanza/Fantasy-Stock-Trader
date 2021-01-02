import React from "react";

export default function Pagination({ transactionsPerPage, totalPages, changePage }) {
    const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalPages / transactionsPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <>
            {totalPages > transactionsPerPage ? (
                <div
                    className="dream-btn"
                    style={{
                        backgroundColor: "#2a3e49",
                        display: "flex",
                        justifyContent: "center",
                        width: totalPages * 20,
                        margin: "auto",
                        padding: 10,
                        borderRadius: 7,
                    }}
                >
                    {pageNumbers.map(number => (
                        <button key={number} className="btn btn-info m-1 dream-btn" onClick={() => changePage(number)}>
                            {number}
                        </button>
                    ))}
                </div>
            ) : null}
        </>
    );
}
