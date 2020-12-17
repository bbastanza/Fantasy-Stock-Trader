import React from "react";
import Button from "react-bootstrap/Button";

export default function LogoutBtn({logOut}) {
    return (
        <Button variant="secondary" size="lg" style={{ margin: 40, alignSelf: "center" }} onClick={logOut}>
            Log Out
        </Button>
    );
}
