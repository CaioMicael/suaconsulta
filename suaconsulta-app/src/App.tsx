import React from 'react';
import { Routes, Route, useLocation } from "react-router-dom";
import './Global.css';
import './App.css';
import MainNavbar from './components/MainNavbar';
import Home from './pages/Home';
import NotFound from './pages/NotFound';
import PatientProfile from './pages/PatientProfile';
import PatientConsultation from './pages/PatientConsultation';
import DoctorProfile from './pages/DoctorProfile';
import DoctorScheduleForDoctor from './pages/DoctorScheduleForDoctor';
import LoginForm from './pages/LoginForm';

function App() {
  const location = useLocation();
  const isLoginPage = location.pathname === "/LoginPage";

  return (
    <>
      {!isLoginPage && <MainNavbar />}
      <Routes>
        <Route path="/LoginPage" element={<LoginForm />} />
        <Route path="/" element={<Home />} />
        <Route path="*" element={<NotFound />} />
        <Route path="/PatientProfile" element={<PatientProfile />} />
        <Route path="/PatientConsultation" element={<PatientConsultation />} />
        <Route path="/DoctorProfile" element={<DoctorProfile />} />
        <Route path="/DoctorScheduleForDoctor" element={<DoctorScheduleForDoctor />} />
      </Routes>
    </>
  );
}

export default App;
