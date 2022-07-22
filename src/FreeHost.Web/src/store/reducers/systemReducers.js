import {
    SYSTEM_CITIES_FAIL,
    SYSTEM_CITIES_REQUEST,
    SYSTEM_CITIES_SUCCESS,
    SYSTEM_AMENITIES_FAIL,
    SYSTEM_AMENITIES_REQUEST,
    SYSTEM_AMENITIES_SUCCESS
  } from "../constants/systemConstants";

  
export const systemCitiesReducer = (state = { cities: [] }, action) => {
    switch (action.type) {
      case  SYSTEM_CITIES_REQUEST:
        return { ...state, loading: true };
  
      case  SYSTEM_CITIES_SUCCESS:
        return { loading: false,cities: action.payload };
  
      case  SYSTEM_CITIES_FAIL:
        return { loading: false, error: action.payload };
  
      default:
        return state;
    }
  };

  export const systemAmenitiesReducer = (state = { amenities: [] }, action) => {
    switch (action.type) {
      case  SYSTEM_AMENITIES_REQUEST:
        return { ...state, loading: true };
  
      case  SYSTEM_AMENITIES_SUCCESS:
        return { loading: false,amenities: action.payload };
  
      case  SYSTEM_AMENITIES_FAIL:
        return { loading: false, error: action.payload };
  
      default:
        return state;
    }
  };