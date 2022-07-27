import {
  SYSTEM_CITIES_FAIL,
  SYSTEM_CITIES_REQUEST,
  SYSTEM_CITIES_SUCCESS,
  
  SYSTEM_AMENITIES_FAIL,
  SYSTEM_AMENITIES_REQUEST,
  SYSTEM_AMENITIES_SUCCESS
} from "../constants/systemConstants";

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
