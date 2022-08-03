import {
  SYSTEM_CITIES_FAIL,
  SYSTEM_CITIES_REQUEST,
  SYSTEM_CITIES_SUCCESS,
  SYSTEM_AMENITIES_FAIL,
  SYSTEM_AMENITIES_REQUEST,
  SYSTEM_AMENITIES_SUCCESS,
  SYSTEM_APARTMENTS_FAIL,
  SYSTEM_APARTMENTS_REQUEST,
  SYSTEM_APARTMENTS_SUCCESS,
  SYSTEM_MY_GUESTS_FAIL,
  SYSTEM_MY_GUESTS_REQUEST,
  SYSTEM_MY_GUESTS_SUCCESS,
  SYSTEM_BOOKING_APPROVE_FAIL,
  SYSTEM_BOOKING_APPROVE_REQUEST,
  SYSTEM_BOOKING_APPROVE_SUCCESS,
} from "../constants/systemConstants";

export const systemCitiesReducer = (state = { cities: [] }, action) => {
  switch (action.type) {
    case SYSTEM_CITIES_REQUEST:
      return { ...state, loading: true };

    case SYSTEM_CITIES_SUCCESS:
      return { loading: false, cities: action.payload };

    case SYSTEM_CITIES_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const systemAmenitiesReducer = (state = { amenities: [] }, action) => {
  switch (action.type) {
    case SYSTEM_AMENITIES_REQUEST:
      return { ...state, loading: true };

    case SYSTEM_AMENITIES_SUCCESS:
      return { loading: false, amenities: action.payload };

    case SYSTEM_AMENITIES_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const systemApartmentsReducer = (state = { apartments: [] }, action) => {
  switch (action.type) {
    case SYSTEM_APARTMENTS_REQUEST:
      return { ...state, loading: true };

    case SYSTEM_APARTMENTS_SUCCESS:
      return { loading: false, apartments: action.payload };

    case SYSTEM_APARTMENTS_FAIL:
      return { loading: false, error: action.payload };

    default:
      return state;
  }
};

export const systemGuestsReducer = (state = { guests: [] }, action) => {
  switch (action.type) {
    case SYSTEM_MY_GUESTS_REQUEST:
      return { ...state, loading: true };

    case SYSTEM_MY_GUESTS_SUCCESS:
      return { loading: false, guests: action.payload };

    case SYSTEM_MY_GUESTS_FAIL:
      return { loading: false, error: action.payload };

    case SYSTEM_BOOKING_APPROVE_REQUEST:
      return { ...state, loading: true };

    case SYSTEM_BOOKING_APPROVE_SUCCESS:
      return { loading: false, guests: action.payload };

    case SYSTEM_BOOKING_APPROVE_FAIL:
      return { loading: false, error: action.payload };
    default:
      return state;
  }
};
