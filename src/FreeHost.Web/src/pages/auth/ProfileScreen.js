import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getUserApartments } from "../../store/actions/userActions";
import Message from "../../components/Message";
import { useNavigate } from "react-router-dom";
import Apartment from "../../components/Apartment";

import "../../css/pages/ProfileScreen.css";

function ProfileScreen() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const userApartments = useSelector((state) => state.userApartments);
  const { error, loading, apartments } = userApartments;

  useEffect(() => {
    dispatch(getUserApartments());

  }, [dispatch]);

  return (
    <div className="profile-page">
      <h1 className="profile-header">Profile</h1>

      <div className="profile-container">
        <div className="profile-duo">
          <div className="profile-img">
            <img
              src="https://dl.memuplay.com/new_market/img/com.vicman.newprofilepic.icon.2022-06-07-21-33-07.png"
              alt="Girl in a jacket"
              width="100%"
              height="100%"
            />
          </div>

          <div className="profile-inputs">ddd</div>
        </div>
      </div>
      <div className="profile-apartments-container">
        <div className="profile-apartments-duo">
          <h1 className="profile-header">My Apartments</h1>
          <button
            className="profile-btn"
            onClick={() => navigate("/apartments/create")}
          >
            CREATE
          </button>
        </div>
        {Object.keys(apartments).length !== 0 ? 
         apartments.map(element => {
           return <Apartment key={element.id} apartment={element} className="profile-apartment"/>
         })
         : (
          <Message>NO APARTMENTS</Message>
        )}
      </div>
    </div>
  );
}

export default ProfileScreen;
