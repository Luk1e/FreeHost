import React, { useEffect } from "react";
import { useNavigate, Outlet } from "react-router-dom";

import { useDispatch, useSelector } from "react-redux";

function HomeLayout() {
  const navigate = useNavigate();
  const userLogin = useSelector((state) => state.userLogin);
  const { userInfo } = userLogin;

  useEffect(() => {
    if (userInfo) {
      navigate(-1, { replace: true });
    }
  }, []);
  return <Outlet />;
}

export default HomeLayout;
