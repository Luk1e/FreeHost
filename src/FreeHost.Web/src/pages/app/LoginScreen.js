import React, { useState, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../../store/actions/userActions";
import { useNavigate } from "react-router-dom";

import Message from "../../components/Message"
import Loader from "../../components/Loader"

import "../../css/pages/LoginScreen.css";
function LoginScreen() {
  const [loginData, setLoginData] = useState("");
  const [password, setPassword] = useState("");

  const userLogin = useSelector((state) => state.userLogin);
  const { error, loading, userInfo } = userLogin;

  const navigate = useNavigate();

  const dispatch = useDispatch();
  useEffect(() => {
    if (userInfo) {
      navigate("/", { replace: true });
    }
  }, [userInfo, navigate]);

  const submitHandler = (e) => {
    e.preventDefault();
    dispatch(login(loginData, password));
  };
  const RegisterNavigator = () => {
    navigate("/register", { replace: true });
  };
  return (
    <div className="login-page">

      <div className="login-container">
        <form onSubmit={submitHandler} className="login-inner-container">
          <h1 className="login-header">Login</h1>
          {loading ? <Loader/> : error? <Message>{error}</Message> : <></>}
                
          <input
            type="text"
            name="login"
            placeholder="Login"
            className="login-input"
            value={loginData}
            onChange={(e) => setLoginData(e.target.value)}
            required
          />
          <input
            type="password"
            name="password"
            placeholder="Password"
            className="login-input"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            required
          />
          <div className="login-btn-container">
            <button className="login-btn" onClick={() => RegisterNavigator()}>
              Register
            </button>
            <button className="login-btn" type="submit">
              Login
            </button>
          </div>
        </form>
      </div>
    </div>
  );
}

export default LoginScreen;
