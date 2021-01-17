import React from "react";

export default function Pagination({ transactionsPerPage, totalPages, changePage, currentPage }) {
    const pageNumbers = [];

    for (let i = 1; i <= Math.ceil(totalPages / transactionsPerPage); i++) {
        pageNumbers.push(i);
    }

    return (
        <>
            {totalPages > transactionsPerPage ? (
                <div style={{ display: "flex", justifyContent: "center", margin: "auto", width: 400 }}>
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
                            <button
                                key={number}
                                className={
                                    number === currentPage
                                        ? "btn m-1 dream-btn btn-warning"
                                        : "btn m-1 dream-btn btn-info"
                                }
                                onClick={() => changePage(number)}
                            >
                                {number}
                            </button>
                        ))}
                    </div>
                </div>
            ) : null}
        </>
    );
}
