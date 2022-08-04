import {
  SYSTEM_CITIES_FAIL,
  SYSTEM_CITIES_REQUEST,
  SYSTEM_CITIES_SUCCESS,
  SYSTEM_AMENITIES_FAIL,
  SYSTEM_AMENITIES_REQUEST,
  SYSTEM_AMENITIES_SUCCESS,
  SYSTEM_REFRESH_FAIL,
  SYSTEM_REFRESH_REQUEST,
  SYSTEM_REFRESH_SUCCESS,
  SYSTEM_APARTMENTS_FAIL,
  SYSTEM_APARTMENTS_REQUEST,
  SYSTEM_APARTMENTS_SUCCESS,
  SYSTEM_MY_GUESTS_FAIL,
  SYSTEM_MY_GUESTS_REQUEST,
  SYSTEM_MY_GUESTS_SUCCESS,
  SYSTEM_BOOKING_APPROVE_FAIL,
  SYSTEM_BOOKING_APPROVE_REQUEST,
  SYSTEM_BOOKING_APPROVE_SUCCESS,
  SYSTEM_BOOKING_REJECT_FAIL,
  SYSTEM_BOOKING_REJECT_REQUEST,
  SYSTEM_BOOKING_REJECT_SUCCESS,
  SYSTEM_MY_BOOKINGS_FAIL,
  SYSTEM_MY_BOOKINGS_REQUEST,
  SYSTEM_MY_BOOKINGS_SUCCESS,
  SYSTEM_GET_USER_FAIL,
  SYSTEM_GET_USER_REQUEST,
  SYSTEM_GET_USER_SUCCESS,
} from "../constants/systemConstants";

import { USER_LOGIN_SUCCESS } from "../constants/userConstants";
import axios from "axios";

export const getCities = () => async (dispatch, getState) => {
  try {
    dispatch({
      type: SYSTEM_CITIES_REQUEST,
    });

    const {
      userLogin: { userInfo },
    } = getState();

    const config = {
      headers: {
        "Content-type": "application/json",
        Authorization: `Bearer ${userInfo.token}`,
      },
    };

    const { data } = await axios.get("/api/hosting/cities", config);

    dispatch({
      type: SYSTEM_CITIES_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      dispatch(getCities());
    }
    dispatch({
      type: SYSTEM_CITIES_FAIL,
      payload:
        error.response &&
        error.response.data &&
        error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const getAmenities = () => async (dispatch, getState) => {
  try {
    dispatch({
      type: SYSTEM_AMENITIES_REQUEST,
    });

    const {
      userLogin: { userInfo },
    } = getState();

    const config = {
      headers: {
        "Content-type": "application/json",
        Authorization: `Bearer ${userInfo.token}`,
      },
    };

    const { data } = await axios.get("/api/hosting/amenities", config);

    dispatch({
      type: SYSTEM_AMENITIES_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      dispatch(getAmenities());
    }
    dispatch({
      type: SYSTEM_AMENITIES_FAIL,
      payload:
        error.response &&
        error.response.data &&
        error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const refresh = (token, refreshToken) => async (dispatch) => {
  try {
    dispatch({
      type: SYSTEM_REFRESH_REQUEST,
    });

    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = await axios.post("/api/authorization/refreshtoken", {
      accessToken: token,
      refreshToken: refreshToken,
    });

    dispatch({
      type: SYSTEM_REFRESH_REQUEST,
      payload: data,
    });
    dispatch({
      type: USER_LOGIN_SUCCESS,
      payload: data,
    });
    localStorage.setItem("userInfo", JSON.stringify(data));
  } catch (error) {
    dispatch({
      type: SYSTEM_REFRESH_FAIL,
      payload:
        error.response && error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const getApartments =
  (city, startDate, endDate, numberOfBeds, sortBy, page) =>
  async (dispatch, getState) => {
    try {
      dispatch({
        type: SYSTEM_APARTMENTS_REQUEST,
      });

      const {
        userLogin: { userInfo },
      } = getState();

      const config = {
        headers: {
          "Content-type": "application/json",
          Authorization: `Bearer ${userInfo.token}`,
        },
      };

      const { data } = await axios.get(
        `/api/search?city=${city}&startDate=${startDate}&endDate=${endDate}${
          numberOfBeds ? "&numberOfBeds=" + numberOfBeds : ""
        }${sortBy ? "&sortBy=" + sortBy : ""}${page ? "&page=" + page : ""}`,
        config
      );

      dispatch({
        type: SYSTEM_APARTMENTS_SUCCESS,
        payload: data,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();

        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        getApartments(city, startDate, endDate, numberOfBeds, sortBy, page);
      }
      dispatch({
        type: SYSTEM_APARTMENTS_FAIL,
        payload:
          error.response &&
          error.response.data &&
          error.response.data[0].description
            ? error.response.data[0].description
            : error.message,
      });
    }
  };

export const getGuests = (page) => async (dispatch, getState) => {
  try {
    dispatch({
      type: SYSTEM_MY_GUESTS_REQUEST,
    });

    const {
      userLogin: { userInfo },
    } = getState();

    const config = {
      headers: {
        "Content-type": "application/json",
        Authorization: `Bearer ${userInfo.token}`,
      },
    };

    const { data } = await axios.get(
      `/api/Booking/guests${page ? "?page=" + page : ""}`,
      config
    );

    dispatch({
      type: SYSTEM_MY_GUESTS_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      dispatch(getGuests(page));
    }
    dispatch({
      type: SYSTEM_MY_GUESTS_FAIL,
      payload:
        error.response &&
        error.response.data &&
        error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const bookingApprove =
  (bookingId, page) => async (dispatch, getState) => {
    try {
      dispatch({
        type: SYSTEM_BOOKING_APPROVE_REQUEST,
      });

      const {
        userLogin: { userInfo },
      } = getState();

      const config = {
        headers: {
          "Content-type": "application/json",
          Authorization: `Bearer ${userInfo.token}`,
        },
      };

      const { data } = await axios.put(
        `/api/booking/approve?bookingId=${bookingId}${
          page ? "&page=" + page : ""
        }`,
        {
          nothing: "nothing",
        },
        config
      );

      dispatch({
        type: SYSTEM_BOOKING_APPROVE_SUCCESS,
        payload: data,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();

        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        dispatch(bookingApprove(bookingId, page));
      }
      dispatch({
        type: SYSTEM_BOOKING_APPROVE_FAIL,
        payload:
          error.description && error.response.description
            ? error.response.description
            : error.message,
      });
    }
  };

export const bookingReject =
  (bookingId, page) => async (dispatch, getState) => {
    try {
      dispatch({
        type: SYSTEM_BOOKING_REJECT_REQUEST,
      });

      const {
        userLogin: { userInfo },
      } = getState();

      const config = {
        headers: {
          "Content-type": "application/json",
          Authorization: `Bearer ${userInfo.token}`,
        },
      };

      const { data } = await axios.put(
        `/api/booking/reject?bookingId=${bookingId}${
          page ? "&page=" + page : ""
        }`,
        {
          nothing: "nothing",
        },
        config
      );

      dispatch({
        type: SYSTEM_BOOKING_REJECT_SUCCESS,
        payload: data,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();

        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        dispatch(bookingReject(bookingId, page));
      }
      dispatch({
        type: SYSTEM_BOOKING_REJECT_FAIL,
        payload:
          error.description && error.response.description
            ? error.response.description
            : error.message,
      });
    }
  };

export const getBookings = (page) => async (dispatch, getState) => {
  try {
    dispatch({
      type: SYSTEM_MY_BOOKINGS_REQUEST,
    });

    const {
      userLogin: { userInfo },
    } = getState();

    const config = {
      headers: {
        "Content-type": "application/json",
        Authorization: `Bearer ${userInfo.token}`,
      },
    };

    const { data } = await axios.get(
      `/api/Booking/bookings${page ? "?page=" + page : ""}`,
      config
    );

    dispatch({
      type: SYSTEM_MY_BOOKINGS_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      dispatch(getBookings(page));
    }
    dispatch({
      type: SYSTEM_MY_BOOKINGS_FAIL,
      payload:
        error.response && error.response.data && error.response.data.description
          ? error.response.data.description
          : error.message,
    });
  }
};



export const getUser=
  () =>
  async (dispatch, getState) => {
    try {
      dispatch({
        type: SYSTEM_GET_USER_REQUEST,
      });

      const {
        userLogin: { userInfo },
      } = getState();

      const config = {
        headers: {
          "Content-type": "application/json",
          Authorization: `Bearer ${userInfo.token}`,
        },
      };

      const { data } = await axios.get(
       "api/User/get",
        config
      );

      dispatch({
        type: SYSTEM_GET_USER_SUCCESS,
        payload: data,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();

        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        getUser();
      }
      dispatch({
        type: SYSTEM_GET_USER_FAIL,
        payload:
          error.response &&
          error.response.data &&
          error.response.data[0].description
            ? error.response.data[0].description
            : error.message,
      });
    }
  };
