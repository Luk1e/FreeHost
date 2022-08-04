import React, { useEffect } from "react";

import { useDispatch, useSelector } from "react-redux";
import { getGuests } from "../../store/actions/systemActions";
import { useNavigate, useSearchParams } from "react-router-dom";

import Guests from "../../components/Guests";
import Message from "../../components/Message";
import Loader from "../../components/Loader";
import GuestsPagination from "../../components/GuestsPagination";

import "../../css/pages/MyGuestsScreen.css";

function MyGuestsScreen() {
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");

  const navigate = useNavigate();

  const systemGuests = useSelector((state) => state.systemGuests);
  const { error, loading, guests } = systemGuests;

  useEffect(() => {
    dispatch(getGuests(page));
  }, [page, dispatch, navigate]);

  return (
    <div className="result-page">
      <div className="result-container">
        <div className="result-inner-container">
          <h1 className="myGuests-header">My Guests</h1>
          <div className="result-apartment"></div>
          {loading ? (
            <Loader />
          ) : error ? (
            <Message>{error}</Message>
          ) : guests &&
            Object.keys(guests).length !== 0 &&
            guests.data.length !== 0 ? (
            guests.data.map((element, index) => {
              return (
                <Guests
                  key={index}
                  apartment={element.apartment}
                  user={element.user}
                />
              );
            })
          ) : (
            <Message>NO GUESTS</Message>
          )}
          {guests && (
            <GuestsPagination page={guests.page} maxPage={guests.maxPage} />
          )}
        </div>
      </div>
    </div>
  );
}

export default MyGuestsScreen;
