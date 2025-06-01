import { useEffect, useState } from "react";
import Input from '../components/Input';
import DoctorScheduleTime from "../components/DoctorScheduleTime";
import Select from "../components/Select";
import SelectMonth from "../components/SelectMonth";

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
                <Input labelDescription="Nome Médico" name="doctor-name" type="text" disabled={true} value={nome}/>
                <Input labelDescription="Especialidade" name="doctor-especiality" type="text" disabled={true} value={especialidade}/>
                <Input labelDescription="CRM" name="doctor-crm" type="text" disabled={true} value={crm}/>
                <Input labelDescription="Telefone" name="doctor-phone" type="phone" disabled={true} value={telefone}/>
                <Input labelDescription="Email" name="doctor-email" type="text" disabled={true} value={email}/>
                <Input labelDescription="Ano" name="ano" type="number" defaultValue={new Date().getFullYear()}/>
                <SelectMonth />
                <div>
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