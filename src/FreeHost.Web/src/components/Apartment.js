//       REACT
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

//       REDUX
import { deleteApartment } from "../store/actions/userActions";
import { useSelector, useDispatch } from "react-redux";

//       OTHER
import ".././css/components/Apartment.css";

//       COMPONENTS
import Message from "../components/Message";
import Loader from "../components/Loader";

function Apartment(props) {
  const [display, setDisplay] = useState(true);
  const navigate = useNavigate();
  const dispatch = useDispatch();

  const userDeleteApartments = useSelector(
    (state) => state.userDeleteApartments
  );
  const { loading, error, success } = userDeleteApartments;

  //                   Delete apartment
  const DeleteHandler = () => {
    if (window.confirm("Are you sure you want to delete this apartment?")) {
      dispatch(deleteApartment(props.apartment.id));
      setDisplay(!display);
    }
  };

  //                    USE EFFECT
  useEffect(() => {}, [navigate, dispatch]);
  return (
    <div
      className="apartment-container"
      style={{ display: display ? "flex" : "none" }}
    >
      <div className="apartment-inner-container">
        <div className="apartment-name">{props.apartment.name} </div>

        <div className="apartment-duo">
          <div className="apartment-duoL">
            <div className="apartment-inline">
              <b>city:</b> {props.apartment.city}
            </div>
            <div className="apartment-inline">
              <b>address:</b> {props.apartment.address}
            </div>
            <div className="apartment-inline">
              <b>number of beds:</b> {props.apartment.numberOfBeds}
            </div>
            <div className="apartment-inline">
              <b>amenities: </b>
              {props.apartment.amenities.length == 0
                ? "none"
                : props.apartment.amenities.map((element) =>
                    element !=
                    props.apartment.amenities[
                      props.apartment.amenities.length - 1
                    ]
                      ? element + ", "
                      : element
                  )}
            </div>
            <div className="apartment-inline">
              <b>distance from center:</b>{" "}
              {props.apartment.distanceFromTheCenter}{" "}
            </div>
            {props.apartment.bookedDates.length!==0 && (
              <div className="apartment-inline">
                <b>booked dates</b>
                {props.apartment.bookedDates.map(
                  (date) => date.startDate + "-" + date.endDate
                )}
              </div>
            )}
          </div>
          <div className="apartment-duoR">
            <img
              src={"data:image/png;base64," + props.apartment.photos[0]}
              className="apartment-img"
            />
          </div>
        </div>
        {props.booking ? (
          <button
            className={
              props.apartment.available
                ? "apartment-btn-booking"
                : "apartment-btn margin-top-5vh  margin-bottom-5vh"
            }
          >
            {props.apartment.available ? "Book now" : "not available"}
          </button>
        ) : (
          <div className="apartment-btn-container">
            <button
              className="apartment-btn"
              onClick={() => {
                navigate(`/apartments/${props.apartment.id}/edit`);
              }}
            >
              edit
            </button>
            <button className="apartment-btn" onClick={() => DeleteHandler()}>
              delete
            </button>
          </div>
        )}
      </div>
    </div>
  );
}

export default Apartment;
