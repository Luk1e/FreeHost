import React, { useEffect } from "react";

import { useDispatch, useSelector } from "react-redux";
import { getBookings } from "../../store/actions/systemActions";
import { useNavigate, useSearchParams } from "react-router-dom";

import ApartmentPro from "../../components/ApartmentPro";
import Message from "../../components/Message";
import Loader from "../../components/Loader";
import BookingsPagination from "../../components/BookingsPagination";

import "../../css/pages/MyBookingsScreen.css";

function MyBookingsScreen() {
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");

  const navigate = useNavigate();

  const systemBookings = useSelector((state) => state.systemBookings);
  const { error, loading, bookings } = systemBookings;

  useEffect(() => {
    dispatch(getBookings(page));
    console.log(bookings);
  }, [page, dispatch, navigate]);
  return (
    <div className="result-page">
      <div className="result-container">
        <div className="result-inner-container">
          <h1 className="myGuests-header">My Bookings</h1>
          <div className="result-apartment"></div>
          {loading ? (
            <Loader />
          ) : error ? (
            <Message>{error}</Message>
          ) : bookings &&
            Object.keys(bookings).length !== 0 &&
            bookings.data.length !== 0 ? (
            bookings.data.map((element, index) => {
              return (
                <ApartmentPro
                  key={index}
                  apartment={element.apartment}
                  user={element.user}
                />
              );
            })
          ) : (
            <Message>NO APARTMENTS</Message>
          )}
          {bookings && (
            <BookingsPagination
              page={bookings.page}
              maxPage={bookings.maxPage}
            />
          )}
        </div>
      </div>
    </div>
  );
}

export default MyBookingsScreen;
