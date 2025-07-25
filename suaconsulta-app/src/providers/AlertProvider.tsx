import React, { createContext, useContext, useState, ReactNode } from 'react';
import SucessAlert from '../components/alerts/SucessAlert';
import ErrorAlert from '../components/alerts/ErrorAlert';
import WarningAlert from '../components/alerts/WarningAlert';

type AlertType = 'success' | 'error' | 'warning';

interface AlertContextType {
  showAlert: (message: string, type: AlertType, duration?: number) => void;
  hideAlert: () => void;
}

const AlertContext = createContext<AlertContextType | undefined>(undefined);

/**
 * Dispara um alerta com uma mensagem e tipo específicos.
 * @returns {AlertContextType}
 * @throws Error se o hook for usado fora do AlertProvider
 */
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

  const timerRef = React.useRef<NodeJS.Timeout | null>(null);

  /**
   * 
   * @param message Mensagem que será exibida no alerta
   * @param type Tipo da alerta (success, error, warning, info)
   * @param duration Duração em milissegundos que o alerta ficará visível. Se 0, o alerta não desaparecerá automaticamente.
   * @return void
   */
  const showAlert = (message: string, type: AlertType = 'success', duration: number = 3000) => {
    if (timerRef.current) {
      clearTimeout(timerRef.current);
    }
    
    setAlertConfig({
      visible: true,
      message,
      type,
    });
    
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
      {alertConfig.visible && alertConfig.type === 'error' && (
        <ErrorAlert message={alertConfig.message} />
      )}
      {alertConfig.visible && alertConfig.type === 'warning' && (
        <WarningAlert message={alertConfig.message} />
      )}
      
      {children}
    </AlertContext.Provider>
  );
};