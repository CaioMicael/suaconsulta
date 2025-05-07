import { useEffect, useState } from "react";
import ButtonAgendar from "./ButtonAgendar";
import Input from './Input';

interface DoctorScheduleProps {
    DoctorId: number;
    nome: string;
    especialidade: string;
    crm: string;
    telefone: string;
    email: string;
}


const DoctorSchedule = ({DoctorId, nome, especialidade, crm, telefone, email}: DoctorScheduleProps) => {
    const [showTime, setShowTime] = useState(false);
    const [inputValue, setInputValue] = useState('');
    
    const toggleShowTime = () => {
        setShowTime(!showTime);
    }
    
    const handleInputChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setInputValue(event.target.value);
        toggleShowTime();
    }

    return (
        <div>
            <h2>Agendar Consulta</h2>
            <div className="grid gap-6 mb-6 md:grid-cols-2">
                <Input labelDescription="ID" name="doctor-id" type="text" disabled={true} value={DoctorId}/>
                <Input labelDescription="Nome Médico" name="doctor-name" type="text" disabled={true} value={nome}/>
                <Input labelDescription="Especialidade" name="doctor-especiality" type="text" disabled={true} value={especialidade}/>
                <Input labelDescription="CRM" name="doctor-crm" type="text" disabled={true} value={crm}/>
                <Input labelDescription="Telefone" name="doctor-phone" type="phone" disabled={true} value={telefone}/>
                <Input labelDescription="Email" name="doctor-email" type="text" disabled={true} value={email}/>
                <div>
                    <Input labelDescription='Escolher Horário' name='Data' type='date' onChange={handleInputChange} />
                    {showTime ? (
                    <div className="grid grid-cols-3 gap-4"> 
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">08:00</button>
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">09:00</button>
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">10:00</button>
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">11:00</button>
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">12:00</button>
                        <button className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500">13:00</button>
                    </div>
                    ) : null}
                </div>      
            </div>
        </div>
    )
}

export default DoctorSchedule;