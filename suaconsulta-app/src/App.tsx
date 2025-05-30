import React from 'react';
import { Routes, Route } from "react-router-dom";
import './Global.css';
import './App.css';
import MainNavbar from './components/MainNavbar';
import Home from './pages/Home';
import NotFound from './pages/NotFound';
import PatientProfile from './pages/PatientProfile';
import PatientConsultation from './pages/PatientConsultation';
import DoctorProfile from './pages/DoctorProfile';

function App() {
  return (
    <>
      <MainNavbar />
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/PatientProfile" element={<PatientProfile />} />
        <Route path="/PatientConsultation" element={<PatientConsultation />} />
        <Route path="/DoctorProfile" element={<DoctorProfile />} />
      </Routes>
    </>
  );
}

export default App;
