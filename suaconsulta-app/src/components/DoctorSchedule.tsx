import { useEffect, useState } from "react";
import ButtonAgendar from "./ButtonAgendar";
import Input from './Input';

interface DoctorScheduleProps {
    DoctorId: number;
    nome: string;
}


const DoctorSchedule = ({DoctorId}: DoctorScheduleProps) => {
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
        <div className="medico-disponivel-container">
            <h2>Agendar Consulta</h2>
            <p>Id: {DoctorId}</p>
            <h2>Escolher Horário</h2>
            <div>
                <Input labelDescription='Data' name='Data' type='date' onChange={handleInputChange} className='border border-gray-300 p-2 m-2 rounded' />
                {showTime ? (
                <div className="grid grid-cols-3 gap-4"> 
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">08:00</button>
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">09:00</button>
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">10:00</button>
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">11:00</button>
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">12:00</button>
                    <button className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg">13:00</button>
                </div>
                ) : null}
            </div>
            <ButtonAgendar labelDescription="Agendar" name="button-agendar" type="button" DoctorId={DoctorId} />
        </div>
    )
}

export default DoctorSchedule;