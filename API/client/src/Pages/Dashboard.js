import React, { useEffect, useState, useContext } from "react";
import { getUserData } from "./../helpers/axios";
import { LoginContext } from "./../contexts/LoginContext";
import { Link } from "react-router-dom";
import UserHolding from "../IndividualComponents/UserHolding";
import DashImage from "../Images/dashboard.png";
import CircleAnimation from "./../IndividualComponents/CircleAnimation";

export default function Dashboard() {
    const [userData, setUserData] = useState();
    const [holdings, setHoldings] = useState([]);
    const loginContext = useContext(LoginContext);

    useEffect(async () => {
        loginContext.setIsLoggedIn(true);
        const data = await getUserData();
        setUserData(data);
        setHoldings(data.holdings);
    }, []);

    function beautifyNumber(balance) {
        return parseFloat(balance)
            .toFixed(2)
            .toString()
            .replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    }

    return (
        <div className="portfolio-page">
            <h1 className="title">Dashboard</h1>
            {userData ? (
                <>
                    <div
                        style={{
                            backgroundImage: `url(${DashImage})`,
                            backgroundRepeat: "no-repeat",
                            backgroundPosition: "center",
                            backgroundPositionY: 0,
                            backgroundSize: 420,
                        }}
                    >
                        <Link to="purchase">
                            <button className="btn btn-lg btn-info dream-btn" style={{ margin: 40 }}>
                                Purchase Stocks
                            </button>
                        </Link>
                    </div>
                    <div className="user-holding-container dream-shadow">
                        <h1>Balance: ${beautifyNumber(userData.balance)}</h1>
                        {userData.allocatedFunds > 0 ? (
                            <h1>Allocated Funds: ${beautifyNumber(userData.allocatedFunds)}</h1>
                        ) : null}
                        {holdings
                            ? holdings.map(holding => {
                                  return holding.totalShares > 0 ? (
                                      <UserHolding
                                          holdingData={holding}
                                          key={Math.random() * new Date().now}
                                          allocatedFunds={userData.allocatedFunds}
                                      />
                                  ) : null;
                              })
                            : null}{" "}
                    </div>
                </>
            ) : (
                <CircleAnimation />
            )}
        </div>
    );
}
