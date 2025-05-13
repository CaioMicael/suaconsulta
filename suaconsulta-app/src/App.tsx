import React from 'react';
import { Routes, Route } from "react-router-dom";
import logo from './logo.svg';
import './App.css';
import Home from './pages/Home';
import NotFound from './pages/NotFound';
import MainNavbar from './components/MainNavbar';
import PatientProfile from './pages/PatientProfile';

function App() {
  return (
    <>
      <MainNavbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/PatientProfile" element={<PatientProfile />} />
      </Routes>
    </>
  );
}

export default App;
