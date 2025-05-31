import { useEffect, useState } from "react";
import Input from './Input';
import DoctorScheduleTime from "./DoctorScheduleTime";
import Select from "./Select";

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
    
    const handleInputChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
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
                    {/* <Input labelDescription='Escolher Horário' name='Data' type='date' onSelect={handleInputChange} /> */}
                    <Select 
                        labelDescription="Escolher Data" 
                        name="data" 
                        options={[]}
                        onSelect={handleInputChange}  />
                    {showTime ? (
                    <div className="grid grid-cols-3 gap-4"> 
                        <DoctorScheduleTime DoctorId={DoctorId} date={inputValue} />
                    </div>
                    ) : null}
                </div>      
            </div>
        </div>
    )
}

export default DoctorSchedule;