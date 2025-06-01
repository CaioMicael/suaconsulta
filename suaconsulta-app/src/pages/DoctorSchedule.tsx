import { use, useEffect, useState } from "react";
import Input from '../components/Input';
import DoctorScheduleTime from "../components/DoctorScheduleTime";
import Select from "../components/Select";
import SelectMonth from "../components/SelectMonth";
import api from "../services/api";

interface DoctorScheduleProps {
    DoctorId: number;
    nome: string;
    especialidade: string;
    crm: string;
    telefone: string;
    email: string;
}

interface DateOption {
    startTime: string;
    shortDate: string;
    hour: string;
    minute: string;
}


const DoctorSchedule = ({DoctorId, nome, especialidade, crm, telefone, email}: DoctorScheduleProps) => {
    const [monthValue, setMonthValue] = useState('');
    const [yearValue, setYearValue] = useState(new Date().getFullYear().toString());
    const [availableTimes, setAvailableTimes] = useState<{ value: string|number; label: string }[]>([]);

    useEffect(() => {
        console.log("monthValue", monthValue);
        console.log("yearValue", yearValue);
        if (yearValue !== '' && monthValue !== '') {
            loadDates();
        }
    }, [monthValue, yearValue]);

    useEffect(() => {
        console.log("Novo availableTimes:", availableTimes);
    }, [availableTimes]);
    
    const HandleMonthChange = (event: React.ChangeEvent<HTMLSelectElement>) => {
        setMonthValue(event.target.value);
    }

    const handleYearChange = (event: React.ChangeEvent<HTMLInputElement>) => {
        setYearValue(event.target.value);
    }

    const loadDates = async () => {
        try {
            const response = await api.get("DoctorSchedule/ListAvailableTimes", {
                  params: {
                    DoctorId,
                    month: Number(monthValue),
                    year: Number(yearValue)
                }
            });

            const options = response.data.map((dateOption: DateOption) => {
              const date = `${dateOption.shortDate} ${dateOption.hour}:${dateOption.minute.toString().padStart(2, '0')}`;
              return {
                value: date,
                label: date
              };
            });

            setAvailableTimes(options);
        } catch (error) {
            console.error("Erro ao buscar horários:", error);   
        }
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
                <Input labelDescription="Ano" name="ano" type="number" onChange={handleYearChange} defaultValue={new Date().getFullYear()}/>
                <SelectMonth onChange={HandleMonthChange} />
                <div>
                    <Select 
                        labelDescription="Escolher Data" 
                        name="data" 
                        options={availableTimes}
                    />
                </div>      
            </div>
        </div>
    )
}

export default DoctorSchedule;