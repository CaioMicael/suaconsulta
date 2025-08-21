import { use, useEffect, useState } from "react";
import Input from '../components/Input';
import Select from "../components/Select";
import SelectMonth from "../components/SelectMonth";
import api from "../services/api";
import { DateOption, DoctorApi } from "@/interfaces";

/**
 * Componente para agendamento de consultas com o médico.
 * @param doctor Objeto do tipo Doctor contendo informações do médico.
 * @property {Doctor} doctor - Informações do médico.
 * @returns JSX.Element
 */
const DoctorSchedule = ({doctor}: {doctor: DoctorApi}) => {
    const [monthValue, setMonthValue] = useState('');
    const [yearValue, setYearValue] = useState(new Date().getFullYear().toString());
    const [availableTimes, setAvailableTimes] = useState<{ value: string|number; label: string }[]>([]);

    useEffect(() => {
        if (yearValue !== '' && monthValue !== '') {
            loadDates();
        }
    }, [monthValue, yearValue]);
    
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
                    doctorId: doctor.id,
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
                <Input labelDescription="Nome Médico" name="doctor-name" type="text" disabled={true} value={doctor.name}/>
                <Input labelDescription="Especialidade" name="doctor-especiality" type="text" disabled={true} value={doctor.specialty}/>
                <Input labelDescription="CRM" name="doctor-crm" type="text" disabled={true} value={doctor.crm}/>
                <Input labelDescription="Telefone" name="doctor-phone" type="phone" disabled={true} value={doctor.phone}/>
                <Input labelDescription="Email" name="doctor-email" type="text" disabled={true} value={doctor.email}/>
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