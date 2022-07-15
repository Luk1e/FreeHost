import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import { login } from "../../store/actions/userActions";
import { useNavigate } from "react-router-dom";
function LoginScreen() {
  const [loginData, setLoginData] = useState("");
  const [password, setPassword] = useState("");

  const userLogin = useSelector((state) => state.userLogin);
  const { error, loading, userInfo } = userLogin;

  const navigate = useNavigate();

  const dispatch = useDispatch();
  useEffect(() => {
    if (userInfo) {
      navigate(-1);
    }
  }, [userInfo]);

  const submitHandler = (e) => {
    e.preventDefault();
    dispatch(login("Login", "Password_1"));
  };

  return (
    <div>
      LoginScreen <button onClick={(e) => submitHandler(e)}>dd</button>
    </div>
  );
}

export default LoginScreen;
