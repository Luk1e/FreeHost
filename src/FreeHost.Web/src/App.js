import React from "react";
import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import RegisterScreen from "./pages/app/RegisterScreen";
import LoginScreen from "./pages/app/LoginScreen";
import ProfileScreen from "./pages/auth/ProfileScreen";
import SearchScreen from "./pages/auth/SearchScreen";

import AuthorizedLayout from "./layouts/AuthorizedLayout";
import HomeLayout from "./layouts/HomeLayout";

import "./App.css";

export default function App() {
  return (
    <Router>
      <Routes>
        <Route element={<HomeLayout />}>
          <Route path="/Register" element={<RegisterScreen />} />
          <Route path="/login" element={<LoginScreen />} />
        </Route>

        <Route element={<AuthorizedLayout />}>
          <Route path="profile" element={<ProfileScreen />} />
          <Route path="/" element={<SearchScreen />} />
        </Route>
      </Routes>
    </Router>
  );
}