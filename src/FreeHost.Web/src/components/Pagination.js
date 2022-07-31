import React, { useState } from "react";

import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";

import { useNavigate, useSearchParams } from "react-router-dom";

import ".././css/components/Pagination.css";

export default function Paginations(props) {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");

  const city = searchParams.get("city");
  const startDate = searchParams.get("startDate");
  const endDate = searchParams.get("endDate");
  const sortBy = searchParams.get("sortBy");
  const numberOfBeds = searchParams.get("numberOfBeds");

  return (
    <div className="pagination-container">
      {props.maxPage > 1 && (
        <Stack spacing={2}>
          <Pagination
            count={props.maxPage}
            onChange={(event, value) => {
              navigate(
                `/search?city=${city}&startDate=${startDate}&endDate=${endDate}${
                  sortBy ? "&sortBy=" + sortBy : ""
                }${
                  numberOfBeds ? "&numberOfBeds=" + numberOfBeds : ""
                }&page=${value}`
              );
            }}
            page={Number(page ? page : 1)}
            color="primary"
          />
        </Stack>
      )}
    </div>
  );
}
