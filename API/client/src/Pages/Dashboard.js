import React from "react";
import UserHolding from "../IndividualComponents/UserHolding";
import { Link } from "react-router-dom";

export default function Dashboard({ allocatedFunds }) {
    const mockData = {
        asset: "Ethereum",
        abbr: "ETH",
        shares: 1.23456,
        value: 500,
    };

    return (
        <div className="portfolio-page">
            <h1 className="title">Dashboard</h1>
                <Link to="purchase">
                    <button className="btn btn-lg btn-warning">Purchase Stocks</button>
                </Link>
            <div className="user-holding-container">
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
            </div>
        </div>
    );
}
