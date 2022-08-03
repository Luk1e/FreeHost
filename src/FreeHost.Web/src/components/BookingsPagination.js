import React from "react";

import Pagination from "@mui/material/Pagination";
import Stack from "@mui/material/Stack";

import { useNavigate, useSearchParams } from "react-router-dom";

export default function BookingsPagination(props) {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const page = searchParams.get("page");

  return (
    <div className="pagination-container">
      {props.maxPage > 1 && (
        <Stack spacing={2}>
          <Pagination
            count={props.maxPage}
            onChange={(event, value) => {
              navigate(`/mybookings?page=${value}`);
            }}
            page={Number(page ? page : 1)}
            color="primary"
          />
        </Stack>
      )}
    </div>
  );
}
