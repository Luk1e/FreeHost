//       REACT
import React, { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

//       REDUX
import { useDispatch, useSelector } from "react-redux";
import { getCities, getAmenities } from "../../store/actions/systemActions";
import { createApartment } from "../../store/actions/userActions";
import { USER_CREATE_APARTMENTS_RESET } from "../../store/constants/userConstants";

//       OTHER
import { imageToBase64 } from "../../functions/Functions";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";

import "../../css/pages/CreateApartmentScreen.css";
//       COMPONENTS
import Message from "../../components/Message";
import Loader from "../../components/Loader";

function CreateApartmentScreen() {
  const [name, setName] = useState("");
  const [city, setCity] = useState("");
  const [address, setAddress] = useState("");
  const [amenities, setAmenities] = useState([]);
  const [numberOfBeds, setNumberOfBeds] = useState("");
  const [photo, setPhoto] = useState("");
  const [distance, setDistance] = useState("");

  const [message, setMessage] = useState("");

  //                  GET CITY LIST
  const systemCities = useSelector((state) => state.systemCities);
  const { error, loading, cities: citiesList } = systemCities;

  //                  GET AMENITY LIST
  const systemAmenities = useSelector((state) => state.systemAmenities);
  const {
    error: amenitiesError,
    loading: amenitiesLoading,
    amenities: amenitiesList,
  } = systemAmenities;


  const  userCreateApartments = useSelector((state) => state.userCreateApartments);
  const {
    error: createError,
    loading: createLoading,
    success,
  } = userCreateApartments;

  const navigate = useNavigate();
  const dispatch = useDispatch();

  //                    USE EFFECT
  useEffect(() => {
    dispatch(getCities());
    dispatch(getAmenities());
    if(success){
      
      dispatch({type:USER_CREATE_APARTMENTS_RESET})
      navigate("/profile")
    }
  }, [navigate, dispatch,success]);

  //                PHOTO
  const UploadPhoto = (e) => {
    setPhoto(e.target.files[0]);
 
  };

  //                LIMIT AMENITIES
  const OptionLimitation = () => {
    return amenities.length >= 5;
  };

  //                VALIDATION

  const Validation = () => {
    !name
      ? setMessage("Enter apartment name")
      : !city
      ? setMessage("Enter city")
      : !address
      ? setMessage("Enter address")
      : !numberOfBeds
      ? setMessage("Enter number of beds")
      : !photo
      ? setMessage("Please add a photo of apartment")
      : !distance
      ? setMessage("Enter distance from center")
      : setMessage("");
  };
  //              FORM SUBMIT
  const submitHandler = (e) => {
    e.preventDefault();
    if (!message) {
      imageToBase64(photo).then((base64String) => {
        dispatch(createApartment(
          name,
          city,
          address,
          amenities,
          numberOfBeds,
          base64String,
          distance
        ));
      });
    }
   
  };

  return (
    <div className="create-app-page">
       {createLoading && (<Loader />)}
       {createError && <Message>{createError}</Message>}
      <h1 className="create-app-header">Add an apartment</h1>
      <form onSubmit={submitHandler} className="create-app-container">
        <div className="create-app-duoL">
          {message && <Message>{message}</Message>}
          <input
            type="text"
            name="name"
            placeholder="name"
            className="create-app-input"
            value={name}
            onChange={(e) => setName(e.target.value)}
          />

          {loading ? (
            <Loader />
          ) : error ? (
            <Message>{error}</Message>
          ) : (
            <Autocomplete
              id="combo-box"
              options={citiesList}
              getOptionSelected={(option, value) => option.id === value.id}
              value={city}
              className="create-app-city"
              sx={{ width: 300 }}
              onChange={(_event, city) => {
                setCity(city);
              }}
              renderInput={(params) => <TextField {...params} label="city" />}
            />
          )}

          <input
            type="text"
            name="address"
            placeholder="Address"
            className="create-app-input"
            value={address}
            onChange={(e) => setAddress(e.target.value)}
          />

          {amenitiesLoading ? (
            <Loader />
          ) : amenitiesError ? (
            <Message>{amenitiesError}</Message>
          ) : (
            <Autocomplete
              multiple
              id="tags-standard"
              options={amenitiesList}
              getOptionDisabled={() => OptionLimitation()}
              value={amenities}
              className="create-app-city"
              renderInput={(params) => (
                <TextField {...params} label="amenities" variant="outlined" />
              )}
              onChange={(_event, amenity) => {
                setAmenities(amenity);
              }}
            />
          )}
          <input
            type="number"
            name="numberOfBeds"
            placeholder="Number of beds"
            className="create-app-input"
            value={numberOfBeds}
            onChange={(e) => setNumberOfBeds(e.target.value)}
          />
          <div className="create-app-photo-container">
            <label className="custom-file-upload create-app-photo">
              <input
                type="file"
                name="photo"
                placeholder="Photo"
                onChange={(e) => UploadPhoto(e)}
              />
              <i className="fa fa-cloud-upload"></i> Photo{" "}
            </label>
          </div>
          <input
            type="number"
            name="distance"
            placeholder="Distance"
            className="create-app-input"
            value={distance}
            onChange={(e) => setDistance(e.target.value)}
          />
        </div>
        <div className="create-app-duoR">
          <img
            id="image"
            className="create-app-image-preview"
            alt="add an image"
          />
          <button
            type="submit"
            className="create-app-btn"
            onClick={() => Validation()}
          >
            create
          </button>
        </div>
      </form>
    </div>
  );
}

export default CreateApartmentScreen;
