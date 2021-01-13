import React, { useEffect, useState } from "react";
import { useHistory } from "react-router-dom";
import { Link } from "react-router-dom";
import { getUserData } from "../helpers/userApiCalls";
import { beautifyNumber } from "../helpers/beautifyNumber";
import UserHolding from "../IndividualComponents/UserHolding";
import DashImage from "../Images/dashboard.png";
import CircleAnimation from "./../IndividualComponents/CircleAnimation";

export default function Dashboard() {
    const history = useHistory();
    const [userData, setUserData] = useState();
    const [holdings, setHoldings] = useState([]);
    const [errorMessage, setErrorMessage] = useState("");

    useEffect(() => {
        (async function () {
            setErrorMessage("");
            const data = await getUserData();
            if (!data.ClientMessage) {
                setUserData(data);
                setHoldings(data.holdings.reverse());
            } else if (data.ClientMessage !== "expired") setErrorMessage(data.ClientMessage);
            else history.push("/expired");
        })();
    }, [history]);

    return (
        <div className="portfolio-page">
            <h1 className="title">Dashboard</h1>
            {!!errorMessage ? <p>{errorMessage}</p> : null}
            {!!userData ? (
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
                        <Link
                            to={{
                                pathname: "/purchase",
                                state: {
                                    availableFunds: userData.balance,
                                },
                            }}
                        >
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
                        {!!holdings
                            ? holdings.map(holding => {
                                  return holding.totalShares > 0 ? (
                                      <UserHolding
                                          holdingData={holding}
                                          key={holding.totalShares * Math.random()}
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
