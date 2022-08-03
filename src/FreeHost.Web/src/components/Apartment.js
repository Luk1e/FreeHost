//       REACT
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

//       REDUX
import { deleteApartment, book } from "../store/actions/userActions";
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

  //                   Delete apartment
  const DeleteHandler = () => {
    if (window.confirm("Are you sure you want to delete this apartment?")) {
      dispatch(deleteApartment(props.apartment.id));
      setDisplay(!display);
    }
  };
  const BookHandler = (id) => {
    if (window.confirm("Are you sure you want to book this apartment?")) {
      dispatch(book(id, props.startDate, props.endDate));
    }
  };

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
            <h1 className="apartmentsPro-name apartment-name">{props.apartment.name}</h1>
            <div>
              <h3>
                <b>location: </b>
                {props.apartment.city + ", " + props.apartment.address}
              </h3>
              <h3>
                <b>distance from center: </b>
                {props.apartment.distanceFromTheCenter}
              </h3>
              <h3>
                <b>amenities: </b>
                {props.apartment.amenities.length == 0
                  ? "none"
                  : props.apartment.amenities.map((element, index) =>
                      index == props.apartment.amenities.length - 1
                        ? element
                        : element + ", "
                    )}
              </h3>
              {props.apartment.bookedDates.length !== 0 && (
                <h3>
                  <b>booking dates: </b>
                  <ul>
                    {props.apartment.bookedDates.map((element, index) => (
                      <li key={index}>
                        {element.startDate.substring(0, 10).replace(/-/g, ".") +
                          "  - " +
                          element.endDate.substring(0, 10).replace(/-/g, ".")}
                      </li>
                    ))}
                  </ul>
                </h3>
              )}
              {props.booking ? props.apartment.available ? (
                <div className="apartment-btn-container">
                  <button
                    className="apartment-btn-booking apartment-btn"
                    
                    onClick={() =>
                      props.apartment.available &&
                      BookHandler(props.apartment.id)
                    }
                  >
                   Book now 
                  </button>
                </div>
              ) :  <h2  className="apartment-status-unavailable">not available</h2>
               : (
                <div className="apartment-btn-container">
                  <button
                    className="apartment-btn-edit apartment-btn"
                    onClick={() => {
                      navigate(`/apartments/${props.apartment.id}/edit`);
                    }}
                  >
                    edit
                  </button>
                  <button
                    className="apartment-btn-delete apartment-btn"
                    onClick={() => DeleteHandler()}
                  >
                    delete
                  </button>
                </div>
              )}
            </div>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Apartment;
