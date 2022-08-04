import React from "react";
import {useNavigate } from "react-router-dom";

import "./../css/components/Back.css";

function Back() {
    const navigate = useNavigate();

  return (
    <div className="back-container">
      <button className="back-inner-container" onClick={()=> navigate("/")}><i className="fa-solid fa-magnifying-glass"></i></button>
    </div>
  );
}

export default Back;
