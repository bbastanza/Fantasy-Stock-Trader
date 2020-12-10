import React from "react";

export default function Footer() {
    return (
        <footer
            className="page-footer font-small footer-fixed splash-footer"
            style={{ marginTop: 100, width: "100vw" }}
        >
            <div className="footer-copyright text-center py-2" style={{color: "#313131", fontSize: 20}}>
                © 2020 Copyright:
                <a className="dream-link" href="https://www.brianbastanza.me"> brianbastanza.me</a>
            </div>
        </footer>
    );
}
