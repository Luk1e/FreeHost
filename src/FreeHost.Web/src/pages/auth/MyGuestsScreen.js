import React, { useEffect } from "react";

import { useDispatch, useSelector } from "react-redux";
import { getGuests } from "../../store/actions/systemActions";
import { useNavigate, useSearchParams } from "react-router-dom";

import Pagination from "../../components/Pagination";
import "../../css/pages/MyGuestsScreen.css";

function MyGuestsScreen() {
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");

  const navigate = useNavigate();

  const systemGuests = useSelector((state) => state.systemGuests);
  const { error, loading, guests } = systemGuests;

  useEffect(() => {}, [page, dispatch, navigate]);

  return (
    <div className="result-page">
      <div className="result-container">
        <div className="result-inner-container">
          <h1 className="myGuests-header">My Guests</h1>
          <div className="result-apartment"></div>
          <Pagination page={1} maxPage={3} />
        </div>
      </div>
    </div>
  );
}

export default MyGuestsScreen;
