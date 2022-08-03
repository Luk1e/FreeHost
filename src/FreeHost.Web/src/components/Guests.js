import React from "react";


import { useDispatch, useSelector } from "react-redux";
import { bookingApprove } from "../store/actions/systemActions";
import { useSearchParams } from "react-router-dom";

import ".././css/components/Guests.css";

function Guests(props) {


  

  const BookingAccept = (id) =>{
      
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");
  dispatch(bookingApprove(id,page))
  }

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
          <div className="guests-duo-hor">
            <button className="guests-btn" onClick={()=>BookingAccept(props.apartment.bookingId)}>Accept</button>
            <button className="guests-btn">Reject</button>
          </div>
        </div>
        </div>
      </div>
    </div>
  );
}

export default Guests;
