import { createStore, combineReducers, applyMiddleware } from "redux";
import thunk from "redux-thunk";
import { composeWithDevTools } from "redux-devtools-extension";
import {
  userLoginReducer,
  userRegisterReducer,
  userApartmentsReducer,
  userCreateApartmentReducer,
  userDeleteApartmentReducer,
  userGetApartmentReducer,
  userUpdateApartmentReducer,
} from "./reducers/userReducers";
import {
  systemCitiesReducer,
  systemAmenitiesReducer,
  systemApartmentsReducer,
} from "./reducers/systemReducers";

const reducer = combineReducers({
  userLogin: userLoginReducer,
  userRegister: userRegisterReducer,
  userApartments: userApartmentsReducer,
  userGetApartment: userGetApartmentReducer,
  userCreateApartments: userCreateApartmentReducer,
  userUpdateApartments: userUpdateApartmentReducer,
  userDeleteApartments: userDeleteApartmentReducer,
  systemCities: systemCitiesReducer,
  systemAmenities: systemAmenitiesReducer,
  systemApartments: systemApartmentsReducer,
});

const userInfoFromStorage = localStorage.getItem("userInfo")
  ? JSON.parse(localStorage.getItem("userInfo"))
  : null;
const initialState = { userLogin: { userInfo: userInfoFromStorage } };

const middleware = [thunk];

const store = createStore(
  reducer,
  initialState,
  composeWithDevTools(applyMiddleware(...middleware))
);
export default store;
