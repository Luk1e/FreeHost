import React, { useState, useEffect } from "react";

import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";

import { useNavigate,useSearchParams } from "react-router-dom";

import "./../css/components/Category.css";

function Category() {
  const [searchParams, setSearchParams] = useSearchParams();
  
  const options = [
    { label: "Beds ascending", id: 0 },
    { label: "Beds descending", id: 1 },
    { label: "Distance ascending", id: 2 },
    { label: "Distance descending", id: 3 },
  ];

  const optionsBed = [
    { label: "1", id: 1 },
    { label: "2", id: 2 },
    { label: "3+", id: 3 },
  ];

  const navigate = useNavigate();
  const city=searchParams.get("city")
  const startDate=searchParams.get("startDate")
  const endDate=searchParams.get("endDate")
  const numberOfBedsParam=searchParams.get("numberOfBeds");
  const sortByParam=searchParams.get("sortBy")

  const [sortBy, setSortBy] = useState(sortByParam ? (options.filter((element)=> element.id==sortByParam))[0]  : null);

  const [numberOfBeds, SetNumberOfBeds] = useState(numberOfBedsParam ? (optionsBed.filter((element)=> element.id==numberOfBedsParam))[0]  : null);

  return (
    <div className="category-container">
        <h1 className="category-header">Filters</h1>
    <div className="category-inner-container">
      <Autocomplete
        id="combo-box"
        options={options}
        getOptionSelected={(option, value) => option.id === value.id}
        getOptionLabel={(option) => option.label}
        value={sortBy}
        className="category-input"
        sx={{ width: 300 }}
        onChange={(_event, option) => {
          setSortBy(option);
          navigate(`/search?city=${city}&startDate=${startDate}&endDate=${endDate}${option? "&sortBy="+option.id: ""}${numberOfBedsParam? "&numberOfBeds="+numberOfBedsParam: ""}`)

        }}
        renderInput={(params) => <TextField {...params} label="sort by" />}
      />
      <Autocomplete
        id="combo-box"
        options={optionsBed}
        getOptionSelected={(option, value) => option.id === value.id}
        getOptionLabel={(option) => option.label}
        value={numberOfBeds}
        className="category-input"
        sx={{ width: 300 }}
        onChange={(_event, option) => {
          SetNumberOfBeds(option);
          navigate(`/search?city=${city}&startDate=${startDate}&endDate=${endDate}${sortByParam? "&sortBy="+sortByParam: ""}${option? "&numberOfBeds="+option.id: ""}`)
        }}
        renderInput={(params) => <TextField {...params} label="number of beds" />}
      />
      </div>
    </div>
  );
}

export default Category;
