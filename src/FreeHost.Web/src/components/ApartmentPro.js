import React from "react";

import "./../css/components/ApartmentPro.css";
function ApartmentPro(props) {
  return (
    <div className="apartmentsPro-container">
      <div className="apartmentsPro-inner-container">
        <div className="apartmentsPro-duo-hor">
          <div className="apartmentsPro-duo">
            <img
              className="apartmentsPro-img"
              src={"data:image/png;base64," + props.apartment.photos[0]}
            />
          </div>

          <div className="apartmentsPro-description">
            <h1 className="apartmentsPro-name">{props.apartment.name}</h1>
            <div>
              <h3>
                <b>owner: </b>
                {props.user.firstName + " " + props.user.lastName}
              </h3>
              <h3>
                <b>location: </b>
                {props.apartment.city + ", " + props.apartment.address}
              </h3>
              <h3>
                <b>distance from center: </b>
                {props.apartment.distance} m
              </h3>
              <h3>
                <b>booked dates: </b>
            
                {props.apartment.startDate.substring(0, 10).replace(/-/g, ".") +
                  "  - " +
                  props.apartment.endDate.substring(0, 10).replace(/-/g, ".")}
              </h3>
            </div>
            <div className="apartmentsPro-status">
              <h2>
                <b>STATUS:</b>
              </h2>
              <h2
                style={{
                  textDecoration: "underline",
                  marginLeft: "10px",
                  color:
                    props.apartment.status == 0
                      ? "blue"
                      : props.apartment.status == 1
                      ? "green"
                      : "red",
                }}
              >
                {props.apartment.status == 0
                  ? "PENDING"
                  : props.apartment.status == 1
                  ? "ACCEPTED"
                  : "DECLINED"}{" "}
              </h2>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default ApartmentPro;
