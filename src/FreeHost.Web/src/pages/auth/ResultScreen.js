import React, { useState, useEffect } from "react";

import Search from "../../components/Search";
import Category from "../../components/Category";
import Pagination from "../../components/Pagination";

import { useDispatch, useSelector } from "react-redux";
import { getApartments } from "../../store/actions/systemActions";
import { useNavigate, useSearchParams } from "react-router-dom";

import Apartment from "../../components/Apartment";
import "../../css/pages/ResultScreen.css";

import Loader from "../../components/Loader";
import Message from "../../components/Message";

function ResultScreen() {
  const dispatch = useDispatch();
  const [searchParams, setSearchParams] = useSearchParams();

  const navigate = useNavigate();
  const city = searchParams.get("city");
  const startDate = searchParams.get("startDate");
  const endDate = searchParams.get("endDate");
  const numberOfBeds = searchParams.get("numberOfBeds");
  const sortBy = searchParams.get("sortBy");
  const page = searchParams.get("page");

  const systemApartments = useSelector((state) => state.systemApartments);
  const { error, loading, apartments } = systemApartments;

  useEffect(() => {
    dispatch(
      getApartments(city, startDate, endDate, numberOfBeds, sortBy, page)
    );
  }, [city, startDate, endDate, sortBy, numberOfBeds, page, dispatch]);
  return (
    <div className="result-page">
      <div className="result-container">
        <div className="result-inner-container">
          {/* <button onClick={()=>{console.log(page,apartments)}}>dd</button> */}
          <Search />
          <Category />
          <div className="result-apartment">
            {loading ? (
              <Loader />
            ) : error ? (
              <Message>{error}</Message>
            ) : apartments &&
              Object.keys(apartments).length !== 0 &&
              apartments.apartments.length !== 0 ? (
              apartments.apartments.map((element) => {
                return (
                  <Apartment
                    key={element.id}
                    apartment={element}
                    booking={true}
                  />
                );
              })
            ) : (
              <Message>NO APARTMENTS</Message>
            )}
          </div>
          <Pagination
            page={apartments.currentPage}
            maxPage={apartments.maxPage}
          />
        </div>
      </div>
    </div>
  );
}

export default ResultScreen;
