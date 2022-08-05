import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { getUserApartments } from "../../store/actions/userActions";
import { useNavigate } from "react-router-dom";
import Apartment from "../../components/Apartment";

import "../../css/pages/ProfileScreen.css";

import Loader from "../../components/Loader";
import Message from "../../components/Message";
import { getUser } from "../../store/actions/systemActions";

function ProfileScreen() {
  const dispatch = useDispatch();
  const navigate = useNavigate();

  const userApartments = useSelector((state) => state.userApartments);
  const { error, loading, apartments } = userApartments;

  const systemUser = useSelector((state) => state.systemUser);
  const { error: errorUser, loading: loadingUser, user } = systemUser;

  useEffect(() => {
    dispatch(getUserApartments());
    dispatch(getUser());
  }, [dispatch]);

  return (
    <div className="profile-page">
      <h1 className="profile-header">Profile</h1>
      {user && (
        <div className="profile-container">
          <div className="profile-duo">
            <div className="profile-img-container">
              <img
                className="profile-img"
                src={"data:image/png;base64," + user.photo}
              />
            </div>

            <div className="profile-name">
              <h1>{user.firstName + " " + user.lastName}</h1>
              <h3>
                <b>E-mail: </b>
                {user.email}
              </h3>
            </div>
          </div>
        </div>
      )}
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

        {loading ? (
          <Loader />
        ) : error ? (
          <Message>{error}</Message>
        ) : Object.keys(apartments).length !== 0 ? (
          <div className="profile-apartments-list">
            {apartments.map((element) => {
              return (
                <Apartment
                  key={element.id}
                  apartment={element}
                  className="profile-apartment"
                  profileScreen
                />
              );
            })}
          </div>
        ) : (
          <Message>NO APARTMENTS</Message>
        )}
      </div>
    </div>
  );
}

export default ProfileScreen;
