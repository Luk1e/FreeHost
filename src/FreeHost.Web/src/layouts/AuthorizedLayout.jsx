import React, { useEffect } from "react";
import { useNavigate, Outlet } from "react-router-dom";
import { useSelector } from "react-redux";

import Header from "../components/Header";

function AuthorizedLayout() {
  const navigate = useNavigate();
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  useEffect(() => {
    if (!userInfo) {
      navigate("/login");
    }
  }, []);
  return (
    <>
      {userInfo && (
        <>
          <Header />
          <Outlet />
        </>
      )}
    </>
  );
}

export default AuthorizedLayout;
