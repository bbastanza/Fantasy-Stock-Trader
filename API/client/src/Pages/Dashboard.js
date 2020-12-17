import React from "react";
import UserHolding from "../IndividualComponents/UserHolding";
import { Link } from "react-router-dom";
import DashImage from "../Images/dashboard.png";

export default function Dashboard({ allocatedFunds }) {
    const mockData = {
        asset: "Ethereum",
        abbr: "ETH",
        shares: 1.23456,
        value: 500,
    };

    return (
        <div className="portfolio-page">
            <div
                style={{
                    backgroundImage: `url(${DashImage})`,
                    backgroundRepeat: "no-repeat",
                    backgroundPosition: "center",
                    backgroundPositionY: 100,
                    backgroundSize: 420
                }}
            >
                <h1 className="title">Dashboard</h1>
                <Link to="purchase">
                    <button className="btn btn-lg btn-info dream-btn" style={{ margin: 40}}>
                        Purchase Stocks
                    </button>
                </Link>
            </div>

            <div className="user-holding-container dream-shadow">
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
                <UserHolding holdingData={mockData} allocatedFunds={allocatedFunds} />
            </div>
        </div>
    );
}
