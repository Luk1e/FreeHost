import {
  USER_LOGIN_REQUEST,
  USER_LOGIN_FAIL,
  USER_LOGIN_SUCCESS,
  USER_LOGOUT,
  USER_REGISTER_REQUEST,
  USER_REGISTER_FAIL,
  USER_REGISTER_SUCCESS,
  USER_APARTMENTS_REQUEST,
  USER_APARTMENTS_FAIL,
  USER_APARTMENTS_SUCCESS,
  USER_CREATE_APARTMENTS_FAIL,
  USER_CREATE_APARTMENTS_REQUEST,
  USER_CREATE_APARTMENTS_SUCCESS,
  USER_DELETE_APARTMENTS_FAIL,
  USER_DELETE_APARTMENTS_REQUEST,
  USER_DELETE_APARTMENTS_SUCCESS,
  USER_GET_APARTMENT_FAIL,
  USER_GET_APARTMENT_REQUEST,
  USER_GET_APARTMENT_SUCCESS,
  USER_UPDATE_APARTMENTS_FAIL,
  USER_UPDATE_APARTMENTS_REQUEST,
  USER_UPDATE_APARTMENTS_RESET,
  USER_UPDATE_APARTMENTS_SUCCESS,
} from "../constants/userConstants";

import { refresh } from "./systemActions";

import axios from "axios";

export const login = (login, password) => async (dispatch) => {
  try {
    dispatch({
      type: USER_LOGIN_REQUEST,
    });

    const config = {
      headers: {
        "Content-type": "application/json",
      },
    };

    const { data } = await axios.post(
      "/api/authorization/authorize",
      { login: login, password: password },
      config
    );

    dispatch({
      type: USER_LOGIN_SUCCESS,
      payload: data,
    });

    localStorage.setItem("userInfo", JSON.stringify(data));
  } catch (error) {
    dispatch({
      type: USER_LOGIN_FAIL,
      payload:
        error.response && error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const logout = () => (dispatch) => {
  localStorage.removeItem("userInfo");
  dispatch({ type: USER_LOGOUT });
};

export const register =
  (firstName, lastName, email, password, login, photo) => async (dispatch) => {
    try {
      dispatch({
        type: USER_REGISTER_REQUEST,
      });

      const config = {
        headers: {
          "Content-type": "application/json",
        },
      };

      const { data } = await axios.post(
        "/api/authorization/register",
        {
          firstname: firstName,
          lastname: lastName,
          email: email,
          password: password,
          login: login,
          photo: photo,
        },
        config
      );

      dispatch({
        type: USER_REGISTER_SUCCESS,
        payload: data,
      });
      dispatch({
        type: USER_LOGIN_SUCCESS,
        payload: data,
      });

      localStorage.setItem("userInfo", JSON.stringify(data));
    } catch (error) {
      dispatch({
        type: USER_REGISTER_FAIL,
        payload:
          error.response && error.response.data[0].description
            ? error.response.data[0].description
            : error.message,
      });
    }
  };

export const getUserApartments = () => async (dispatch, getState) => {
  try {
    dispatch({
      type: USER_APARTMENTS_REQUEST,
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

    const { data } = await axios.get("api/hosting/get", config);

    dispatch({
      type: USER_APARTMENTS_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      dispatch(getUserApartments());
    }
    dispatch({
      type: USER_APARTMENTS_FAIL,
      payload:
        error.response && error.response.data[0].description
          ? error.response.data.data[0].description
          : error.message,
    });
  }
};

export const createApartment =
  (name, city, address, amenities, numberOfBeds, photo, distance) =>
  async (dispatch, getState) => {
    try {
      dispatch({
        type: USER_CREATE_APARTMENTS_REQUEST,
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

      const { data } = await axios.post(
        "/api/hosting/add",
        {
          name: name,
          city: city,
          address: address,
          numberOfBeds: numberOfBeds,
          amenities: amenities,
          photos: [photo],
          distanceFromTheCenter: distance,
        },
        config
      );

      dispatch({
        type: USER_CREATE_APARTMENTS_SUCCESS,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();

        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        dispatch(
          createApartment(
            name,
            city,
            address,
            amenities,
            numberOfBeds,
            photo,
            distance
          )
        );
      }
      dispatch({
        type: USER_CREATE_APARTMENTS_FAIL,
        payload:
          error.response &&
          error.response.data &&
          error.response.data[0].description
            ? error.response.data[0].description
            : error.message,
      });
    }
  };

export const updateApartment =
  (id, name, city, address, amenities, numberOfBeds, photo, distance) =>
  async (dispatch, getState) => {
    try {
      dispatch({
        type: USER_UPDATE_APARTMENTS_REQUEST,
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
        "/api/hosting/edit",
        {
          id: id,
          name: name,
          city: city,
          address: address,
          numberOfBeds: numberOfBeds,
          amenities: amenities,
          photos: [photo],
          distanceFromTheCenter: distance,
        },
        config
      );

      dispatch({
        type: USER_UPDATE_APARTMENTS_SUCCESS,
      });
    } catch (error) {
      if (error.response.status == 401) {
        const {
          userLogin: { userInfo },
        } = getState();
  
        dispatch(refresh(userInfo.token, userInfo.refreshToken));
        updateApartment(id, name, city, address, amenities, numberOfBeds, photo, distance)
      }
      dispatch({
        type: USER_UPDATE_APARTMENTS_FAIL,
        payload:
          error.response &&
          error.response.data &&
          error.response.data[0].description
            ? error.response.data[0].description
            : error.message,
      });
    }
  };

export const deleteApartment = (id) => async (dispatch, getState) => {
  try {
    dispatch({
      type: USER_DELETE_APARTMENTS_REQUEST,
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

    const { data } = await axios.delete(`/api/hosting/delete/${id}`, config);
    dispatch({
      type: USER_DELETE_APARTMENTS_SUCCESS,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
        dispatch(deleteApartment(id))
    }
    
    dispatch({
      type: USER_DELETE_APARTMENTS_FAIL,
      payload:
        error.response &&
        error.response.data &&
        error.response.data[0].description
          ? error.response.data[0].description
          : error.message,
    });
  }
};

export const getApartment = (id) => async (dispatch, getState) => {
  try {
    dispatch({
      type: USER_GET_APARTMENT_REQUEST,
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

    const { data } = await axios.get(`/api/hosting/get/${id}`, config);
    dispatch({
      type: USER_GET_APARTMENT_SUCCESS,
      payload: data,
    });
  } catch (error) {
    if (error.response.status == 401) {
      const {
        userLogin: { userInfo },
      } = getState();

      dispatch(refresh(userInfo.token, userInfo.refreshToken));
      getApartment(id)
    }
    dispatch({
      type: USER_GET_APARTMENT_FAIL,
      payload:
        error.response && error.response.data[0].description
          ? error.response.data.data[0].description
          : error.message,
    });
  }
};
