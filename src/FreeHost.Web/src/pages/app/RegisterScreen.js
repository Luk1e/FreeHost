import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { register } from "../../store/actions/userActions";
import { useNavigate } from "react-router-dom";

import Message from "../../components/Message";
import Loader from "../../components/Loader";
import { imageToBase64 } from "../../functions/Functions";
import "../../css/pages/RegisterScreen.css";

function RegisterScreen() {
  const [firstName, setFirstName] = useState("");
  const [lastName, setLastName] = useState("");
  const [loginData, setLoginData] = useState("");
  const [email, setEmail] = useState("");
  const [photo, setPhoto] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");

  const [message, setMessage] = useState("");

  const userRegister = useSelector((state) => state.userRegister);
  const { error, loading, userInfo } = userRegister;

  const navigate = useNavigate();

  const dispatch = useDispatch();
  useEffect(() => {
    if (userInfo) {
      navigate("/", { replace: true });
    }
  }, [userInfo, navigate]);

  const LoginNavigator = () => {
    navigate("/login", { replace: true });
  };

  const UploadPhoto = (e) => {
    setPhoto(e.target.files[0]);
    if (message == "Please add a photo") {
      setMessage("");
    }
  };

  const Validation = () => {
    /[^A-Za-z0-9]+/.test(firstName) || !firstName
      ? setMessage("First name must include only digits and letters")
      : /[^A-Za-z0-9]+/.test(lastName) || !lastName
      ? setMessage("Last name must include only digits and letters")
      : !photo
      ? setMessage("Please add a photo")
      : password != confirmPassword
      ? setMessage("Passwords do not match")
      : !/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/.test(password)?
      setMessage("Password must contain at least eight characters, at least one number and both lower and uppercase letters and special characters")
      : setMessage("");
  };

  const submitHandler = (e) => {
    e.preventDefault();
    if (!message) {
      imageToBase64(photo).then((base64String) => {
        dispatch(register(firstName,lastName, email, password, loginData, base64String));
      });
    }
  };

  

  return (
    <div className="register-page">
      <div className="register-container">
        <form onSubmit={submitHandler} className="register-inner-container">
          <h1 className="register-header">Register</h1>
          {loading ? <Loader /> : error ? <Message>{error}</Message> : <></>}
          {message && <Message>{message}</Message>}
            <input
              type="text"
              name="firstName"
              placeholder="First Name"
              className="register-input"
              pattern="[a-zA-Z0-9-]+"
              value={firstName}
              onChange={(e) => setFirstName(e.target.value)}
              required
            />
            <input
              type="text"
              name="lastName"
              placeholder="Last Name"
              className="register-input"
              pattern="[a-zA-Z0-9-]+"
              value={lastName}
              onChange={(e) => setLastName(e.target.value)}
              required
            />

          <input
            type="email"
            name="email"
            placeholder="E-mail"
            className="register-input"
            value={email}
            onChange={(e) => setEmail(e.target.value)}
            required
          />

          <input
            type="text"
            name="login"
            placeholder="Login"
            className="register-input"
            value={loginData}
            onChange={(e) => setLoginData(e.target.value)}
            required
          />

          <label className="custom-file-upload register-photo register-input">
            <input
              type="file"
              name="photo"
              placeholder="Photo"
              onChange={(e) => UploadPhoto(e)}
              required
            />
            <i className="fa fa-cloud-upload"></i> Photo{" "}
          </label>

            <input
              type="password"
              name="password"
              placeholder="Password"
              className="register-input"
              value={password}
              onChange={(e) => setPassword(e.target.value)}
              pattern="^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$"
              required
            />
            <input
              type="password"
              name="confirmPassword"
              placeholder="Confirm Password"
              className="register-input"
              value={confirmPassword}
              onChange={(e) => setConfirmPassword(e.target.value)}
              required
            />

          <div className="register-btn-container">
            <button className="register-btn" onClick={() => LoginNavigator()}>
              Login
            </button>

            <button
              className="register-btn"
              type="submit"
              onClick={() => Validation()}
            >
              Register
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default RegisterScreen;
