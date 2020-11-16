import React from "react";
import UserHolding from "../IndividualComponents/UserHolding";

export default function Portfolio({ allocatedFunds }) {
    const mockData = {
        asset: "Ethereum",
        abbr: "ETH",
        amount: 1.23456,
        value: 500,
    };

    return (
        <div className="portfolio-page">
            <h1 className="title">Portfolio</h1>
            <div className="user-holding-container">
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
            </div>
        </div>
    );
}
