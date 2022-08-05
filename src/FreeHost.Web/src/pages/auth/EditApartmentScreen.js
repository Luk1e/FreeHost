//       REACT
import React, { useState, useEffect } from "react";
import { useNavigate, useParams } from "react-router-dom";

//       REDUX
import { useDispatch, useSelector } from "react-redux";
import { getCities, getAmenities } from "../../store/actions/systemActions";
import { getApartment, updateApartment } from "../../store/actions/userActions";
import { USER_UPDATE_APARTMENTS_RESET } from "../../store/constants/userConstants";

//       OTHER
import { imageToBase64 } from "../../functions/Functions";
import TextField from "@material-ui/core/TextField";
import Autocomplete from "@material-ui/lab/Autocomplete";

import "../../css/pages/CreateApartmentScreen.css";
//       COMPONENTS
import Message from "../../components/Message";
import Loader from "../../components/Loader";

function EditApartmentScreen() {
  const [name, setName] = useState("");
  const [city, setCity] = useState("");
  const [address, setAddress] = useState("");
  const [amenities, setAmenities] = useState([]);
  const [numberOfBeds, setNumberOfBeds] = useState("");
  const [photo, setPhoto] = useState("");
  const [distance, setDistance] = useState("");

  const [firstTime, setFirstTime] = useState(true);

  // :D
  const [reallyFirstTime ,setReallyFirstTime]=useState(true)

  const [message, setMessage] = useState("");
  const { id } = useParams();
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

  const userGetApartment = useSelector((state) => state.userGetApartment);
  const {
    error: apartmentError,
    loading: apartmentLoading,
    apartment,
  } = userGetApartment;

  const userUpdateApartments = useSelector(
    (state) => state.userUpdateApartments
  );
  const {
    error: updateError,
    loading: updateLoading,
    success,
  } = userUpdateApartments;
  const navigate = useNavigate();
  const dispatch = useDispatch();

  //                    USE EFFECT
  useEffect(() => {
    dispatch(getCities());
    dispatch(getAmenities());

    if (success) {
      dispatch({ type: USER_UPDATE_APARTMENTS_RESET });
      navigate("/profile");
    }

    if (reallyFirstTime) {
      setReallyFirstTime(!reallyFirstTime)
      dispatch(getApartment(id));
    } else {
      setName(apartment.name);
      setAddress(apartment.address);
      setCity(apartment.city);
      setAmenities(apartment.amenities);
      setPhoto(apartment.photos[0]);
      setNumberOfBeds(apartment.numberOfBeds);
      setDistance(apartment.distanceFromTheCenter);
    }
  }, [apartment, navigate, dispatch,success]);

  if (firstTime && photo) {
    document.getElementById("image").src = "data:image/png;base64," + photo;
  }
  //                PHOTO
  const UploadPhoto = (e) => {
    setFirstTime(false);
    document.getElementById("image").src = URL.createObjectURL(
      e.target.files[0]
    );

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
      firstTime
        ? dispatch(
            updateApartment(
              apartment.id,
              name,
              city,
              address,
              amenities,
              numberOfBeds,
              photo,
              distance
            )
          )
        : imageToBase64(photo).then((base64String) => {
            dispatch(
              updateApartment(
                apartment.id,
                name,
                city,
                address,
                amenities,
                numberOfBeds,
                base64String,
                distance
              )
            );
          });
    }
  };

  return (
    <div className="create-app-page">
      {apartmentLoading && <Loader />}
      {apartmentError && <Message>{apartmentError}</Message>}
      <h1 className="create-app-header">Update an apartment</h1>
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
            min="0"
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
            min="0"
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
            update
          </button>
        </div>
      </form>
    </div>
  );
}

export default EditApartmentScreen;
