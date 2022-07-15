import React from "react";
import { Navigate, Outlet } from "react-router-dom";

function AuthorizedLayout() {
    
    if (true) {
        return <Navigate to="/" />;
      }
  return <Outlet />;
}

export default AuthorizedLayout;
