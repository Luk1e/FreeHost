import React, { useState } from "react";
import { Link,useNavigate } from "react-router-dom";
import { useDispatch } from "react-redux";
import {logout} from '../store/actions/userActions'


import "../css/components/Header.css";

function Header() {
  const [isVisible, setIsVisible] = useState(false);
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const logoutHandler = () => {
    dispatch(logout());
    navigate("/login");
  };
  return (
    <div className="header-container">
      <div className="header-inner-container">
        <h1 className="header-h1">username</h1>
        <button
          className="header-btn"
          onClick={() => {
            setIsVisible(!isVisible);
          }}
        >
          cabinet <i className="fa-solid fa-caret-down"></i>
          {isVisible && (
            <div class="dropdown-content">
              <Link to='/profile'>Profile</Link>
              <Link to='/myguests'>My guests</Link>
              <Link to='/mybookings'>My bookings</Link>
            </div>
          )}
        </button>
        <button className="header-btn" onClick={logoutHandler}>Log out</button>
      </div>
    </div>
  );
}

export default Header;
