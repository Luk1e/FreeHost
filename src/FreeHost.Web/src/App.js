import React from "react";
import {BrowserRouter as Router, Routes, Route } from "react-router-dom";
import HomeScreen from "./pages/app/HomeScreen";
import LoginScreen from "./pages/app/LoginScreen";
import ProfileScreen from "./pages/auth/ProfileScreen";
import AuthorizedLayout from "./layouts/AuthorizedLayout";
import HomeLayout from "./layouts/HomeLayout";
import './App.css'

export default function App() {
  return (  <Router>
    <Routes>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<HomeScreen />} />
        <Route path="/login" element={<LoginScreen />} />
      </Route>

      <Route  element={<AuthorizedLayout />}>
        <Route path="profile" element={<ProfileScreen />} />
      </Route>
    </Routes>
    </Router>
  );
}
