import React, { createContext, useContext, useState, ReactNode } from 'react';
import SucessAlert from '../components/alerts/SucessAlert';

type AlertType = 'success' | 'error' | 'warning' | 'info';

interface AlertContextType {
  showAlert: (message: string, type: AlertType, duration?: number) => void;
  hideAlert: () => void;
}

const AlertContext = createContext<AlertContextType | undefined>(undefined);

export const useAlert = () => {
  const context = useContext(AlertContext);
  if (!context) {
    throw new Error('useAlert must be used within an AlertProvider');
  }
  return context;
};

interface AlertProviderProps {
  children: ReactNode;
}

export const AlertProvider: React.FC<AlertProviderProps> = ({ children }) => {
  const [alertConfig, setAlertConfig] = useState<{
    visible: boolean;
    message: string;
    type: AlertType;
  }>({
    visible: false,
    message: '',
    type: 'success',
  });

  // Timer reference to clear timeout if needed
  const timerRef = React.useRef<NodeJS.Timeout | null>(null);

  const showAlert = (message: string, type: AlertType = 'success', duration: number = 3000) => {
    // Clear any existing timer
    if (timerRef.current) {
      clearTimeout(timerRef.current);
    }
    
    // Show the alert
    setAlertConfig({
      visible: true,
      message,
      type,
    });
    
    // Set timer to hide it
    if (duration > 0) {
      timerRef.current = setTimeout(() => {
        hideAlert();
      }, duration);
    }
  };

  const hideAlert = () => {
    setAlertConfig((prev) => ({ ...prev, visible: false }));
  };

  return (
    <AlertContext.Provider value={{ showAlert, hideAlert }}>
      {alertConfig.visible && alertConfig.type === 'success' && (
        <SucessAlert message={alertConfig.message} />
      )}
      
      {children}
    </AlertContext.Provider>
  );
};