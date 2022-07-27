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
  USER_CREATE_APARTMENTS_RESET,
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

export const userLoginReducer = (state = {}, action) => {
  switch (action.type) {
    case USER_LOGIN_REQUEST:
      return { loading: true };

    case USER_LOGIN_SUCCESS:
      return { loading: false, userInfo: action.payload };

    case USER_LOGIN_FAIL:
      return { loading: false, error: action.payload };

    case USER_LOGOUT:
      return {};

    default:
      return state;
  }
};

export const userRegisterReducer = (state = {}, action) => {
  switch (action.type) {
    case USER_REGISTER_REQUEST:
      return { loading: true };

    case USER_REGISTER_SUCCESS:
      return { loading: false, userInfo: action.payload };

    case USER_REGISTER_FAIL:
      return { loading: false, error: action.payload };

    case USER_LOGOUT:
      return {};

    default:
      return state;
  }
};

export const userApartmentsReducer = (state = { apartments: [] }, action) => {
  switch (action.type) {
    case USER_APARTMENTS_REQUEST:
      return { ...state, loading: true };

    case USER_APARTMENTS_SUCCESS:
      return { loading: false, apartments: action.payload };

    case USER_APARTMENTS_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const userCreateApartmentReducer = (state = {}, action) => {
  switch (action.type) {
    case USER_CREATE_APARTMENTS_REQUEST:
      return { loading: true };

    case USER_CREATE_APARTMENTS_SUCCESS:
      return { loading: false, success: true };

    case USER_CREATE_APARTMENTS_FAIL:
      return { loading: false, error: action.payload };

    case USER_CREATE_APARTMENTS_RESET:
      return {};

    default:
      return state;
  }
};

export const userUpdateApartmentReducer = (state = {}, action) => {
  switch (action.type) {
    case USER_UPDATE_APARTMENTS_REQUEST:
      return { loading: true };

    case USER_UPDATE_APARTMENTS_SUCCESS:
      return { loading: false, success: true };

    case USER_UPDATE_APARTMENTS_FAIL:
      return { loading: false, error: action.payload };

    case USER_UPDATE_APARTMENTS_RESET:
      return {};

    default:
      return state;
  }
};

export const userDeleteApartmentReducer = (state = {}, action) => {
  switch (action.type) {
    case USER_DELETE_APARTMENTS_REQUEST:
      return { loading: true };

    case USER_DELETE_APARTMENTS_SUCCESS:
      return { loading: false };

    case USER_DELETE_APARTMENTS_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const userGetApartmentReducer = (state = { apartment: {} }, action) => {
  switch (action.type) {
    case USER_GET_APARTMENT_REQUEST:
      return { ...state, loading: true };

    case USER_GET_APARTMENT_SUCCESS:
      return { loading: false, apartment: action.payload };

    case USER_GET_APARTMENT_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};
