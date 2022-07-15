import React from "react";
import { Routes, Route } from "react-router-dom";
import HomeScreen from "./components/HomeLayout/screens/HomeScreen";
import LoginScreen from "./components/HomeLayout/screens/LoginScreen";
import ProfileScreen from "./components/AuthorizedLayout/screens/ProfileScreen";
import AuthorizedLayout from "./components/AuthorizedLayout/AuthorizedLayout";
import HomeLayout from "./components/HomeLayout/HomeLayout";

export default function App() {
  return (
    <Routes>
      <Route element={<HomeLayout />}>
        <Route path="/" element={<HomeScreen />} />
        <Route path="/login" element={<LoginScreen />} />
      </Route>

      <Route  element={<AuthorizedLayout />}>
        <Route path="profile" element={<ProfileScreen />} />
      </Route>
    </Routes>
  );
}
