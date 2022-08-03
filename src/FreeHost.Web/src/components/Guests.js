import React from "react";

import { useDispatch, useSelector } from "react-redux";
import { bookingApprove,bookingReject } from "../store/actions/systemActions";
import { useSearchParams } from "react-router-dom";

import ".././css/components/Guests.css";

function Guests(props) {
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();

  const BookingAccept = (id) => {
    const page = searchParams.get("page");
    dispatch(bookingReject(id, page));
  };

  
  const BookingReject = (id) => {
    const page = searchParams.get("page");
    dispatch(bookingReject(id, page));
  };


  return (
    <div className="guests-container">
      <div className="guests-inner-container">
        <img
          className="guests-img"
          src={"data:image/png;base64," + props.apartment.photos[0]}
        />
        <div className="guests-duo">
          <div className="guests-inner-duo">
            <h1 className="guests-apartment-name">{props.apartment.name}</h1>

            <h3>
              <b>location: </b>
              {props.apartment.city + ", " + props.apartment.address}
            </h3>
            <h3>
              <b>user: </b>
              {props.user.firstName} {props.user.lastName}
            </h3>
            <h3>
              <b>booking dates: </b>
              {props.apartment.startDate.substring(0, 10).replace(/-/g, ".") +
                "  - " +
                props.apartment.endDate.substring(0, 10).replace(/-/g, ".")}
            </h3>
            {props.apartment.status == 0 ? (
              <div className="guests-duo-hor">
                <button
                  className="guests-btn"
                  onClick={() => BookingAccept(props.apartment.apartmentId)}
                >
                  Accept
                </button>
                <button className="guests-btn"    onClick={() => BookingReject(props.apartment.apartmentId)}>Reject</button>
              </div>
            ) : (
              <h3 className="guests-status" style={{color: props.apartment.status===1? "green" : "red"}}>{props.apartment.status == 1 ? "Accepted" : "Rejected"}</h3>
            )}
          </div>
        </div>
      </div>
    </div>
  );
}

export default Guests;
