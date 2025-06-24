import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.css';
import App from './App';
import reportWebVitals from './reportWebVitals';
import { BrowserRouter } from "react-router-dom";
import { AlertProvider } from './providers/AlertProvider';


const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement).render(
    <BrowserRouter>
    <AlertProvider>
    <App />
    </AlertProvider>
  </BrowserRouter> );
reportWebVitals();
