//       REACT
import React, { useState, useEffect } from "react";
import { useNavigate, useSearchParams } from "react-router-dom";

//       REDUX
import { useDispatch, useSelector } from "react-redux";
import { getCities, getAmenities } from ".././store/actions/systemActions";

//       OTHER
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";

import "../css/components/Search.css";

//       COMPONENTS
import Message from "../components/Message";
import Loader from "../components/Loader";

function Search() {
  const [searchParams, setSearchParams] = useSearchParams();

  const cityParam = searchParams.get("city");
  const startDateParam = searchParams.get("startDate");
  const endDateParam = searchParams.get("endDate");

  const [city, setCity] = useState(cityParam ? cityParam : "");
  const [startDate, setStartDate] = useState(
    startDateParam ? startDateParam : ""
  );
  const [endDate, setEndDate] = useState(endDateParam ? endDateParam : "");
  const [message, setMessage] = useState("");
  const today = new Date();
  let tomorrow = new Date();
  (tomorrow.setDate(today.getDate() + 1));
  tomorrow=tomorrow.toJSON()
  .slice(0, 10);

  const [type, setType] = useState("text");

  const numberOfBedsParam = searchParams.get("numberOfBeds");
  const sortByParam = searchParams.get("sortBy");

  //                  GET CITY LIST
  const systemCities = useSelector((state) => state.systemCities);
  const { error, loading, cities: citiesList } = systemCities;

  const navigate = useNavigate();
  const dispatch = useDispatch();

  //                    USE EFFECT
  useEffect(() => {
    dispatch(getCities());
  }, [navigate, dispatch]);

  const Validation = () => {
    !city
      ? setMessage("Enter city")
      : !startDate
      ? setMessage("Enter start date")
      : !endDate
      ? setMessage("Enter end date")
      : setMessage("");
  };

  const submitHandler = (e) => {
    e.preventDefault();
    if (!message) {
      navigate(
        `/search?city=${city}&startDate=${startDate}&endDate=${endDate}${
          sortByParam ? "&sortBy=" + sortByParam : ""
        }${numberOfBedsParam ? "&numberOfBeds=" + numberOfBedsParam : ""}`
      );
    }
  };
  return (
    <>
      {message && <Message>{message}</Message>}

      <form onSubmit={submitHandler} className="search-container">
        {loading ? (
          <Loader />
        ) : error ? (
          <Message>{error}</Message>
        ) : (
          <Autocomplete
            id="combo-box"
            options={citiesList}
            getOptionSelected={(option, value) => option.id === value.id}
            value={city}
            className="search-city"
            sx={{ width: 300 }}
            onChange={(_event, city) => {
              setCity(city);
            }}
            renderInput={(params) => <TextField {...params} label="city" />}
          />
        )}

        <input
          type={type}
          placeholder="start date"
          value={startDate}
          onFocus={() => {
            setType("date");
          }}
          onBlur={() => setType("text")}
          className="search-input"
          min={tomorrow}
          max={endDate ? endDate : null}
          onChange={(e) => setStartDate(e.target.value)}
        />
        <input
          type={type}
          placeholder="end date"
          onFocus={() => {
            setType("date");
          }}
          value={endDate}
          onBlur={() => setType("text")}
          className="search-input"
          min={startDate ? startDate : tomorrow}
          onChange={(e) => setEndDate(e.target.value)}
        />
        <button
          type="submit"
          className="search-btn"
          onClick={() => Validation()}
        >
          Search
        </button>
      </form>
    </>
  );
}

export default Search;
