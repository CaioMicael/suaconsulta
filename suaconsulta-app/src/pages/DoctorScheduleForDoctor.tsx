import { use, useEffect, useState } from "react";
import Input from "../components/Input";
import ButtonDefault from "../components/ButtonDefault";
import api from "../services/api";

interface DoctorScheduleForDoctorProps {
    id: number;
    doctorId: number;
    startTime: string;
    endTime: string;
    active: string;   
}

const DoctorScheduleForDoctor = ({}) => {
    const [doctorSchedule, setDoctorSchedule] = useState<DoctorScheduleForDoctorProps[]>([]);
    const [isLoading, setIsLoading] = useState(false);

    const loadDoctorSchedule = async () => {
        setIsLoading(true);
        try {
            const response = await api.get('DoctorSchedule/ListDoctorSchedule?DoctorId=1'); // TODO : Replace with actual doctor ID
            const scheduleData = response.data.map((schedule: DoctorScheduleForDoctorProps) => ({
                id: schedule.id,
                doctorId: schedule.doctorId,
                startTime: schedule.startTime,
                endTime: schedule.endTime,
                active: schedule.active ? "Ativo" : "Inativo"
            }));
            setDoctorSchedule(scheduleData);
        } catch (error) {
            console.error("Erro ao buscar agendamentos:", error);
        } finally {
            setIsLoading(false);
        }
    }

    const updateDoctorSchedule = async (scheduleId: number, isActive: boolean) => {
        setIsLoading(true);
        try {
            const response = await api.put("DoctorSchedule/UpdateDoctorSchedule/", {
                id: scheduleId,
                active: isActive
            });
            if (response.status === 200) {
                // Reload the schedule after updating
                loadDoctorSchedule();
            } else {
                console.error("Erro ao atualizar agendamento:", response.data);
            }
        } catch (error) {
            console.error("Erro ao atualizar agendamento:", error);
        } finally {
            setIsLoading(false);
        }
    }

    useEffect(() => {
        loadDoctorSchedule();
    }, []);

    return (
        <div>
            {isLoading ? (
                        <ButtonDefault
                            Description="Carregando..."
                            Name="buscar-dados"
                            Type="button"
                            onClick={() => loadDoctorSchedule()}
                            disabled={isLoading}
                        />                        
                ) : (
                    <ButtonDefault
                        Description="Buscar Dados"
                        Name="buscar-dados"
                        Type="button"
                        onClick={() => loadDoctorSchedule()}
                        disabled={isLoading}
                    />
                )
            }
            <br></br>
            {doctorSchedule.map((schedule) => (
                <div className="inline-grid grid-cols-1 gap-2">
                    <div className="border-2 border-gray-700 focus:border-pink-600 rounded-md shadow-lg m-2 py-2 px-1 w-60">
                        <Input 
                            labelDescription="Id"
                            type="number"
                            disabled={true}
                            name="id"
                            key={"id"}
                            value={schedule.id}
                        />
                        <Input
                            labelDescription="Horário de Início"
                            type="text"
                            name="start-time"
                            key={"start-time"}
                            disabled={true}
                            value={schedule.startTime}
                        />
                        <Input
                            labelDescription="Horário de Término"
                            type="text"
                            name="end-time"
                            key={"end-time"}
                            disabled={true}
                            value={schedule.endTime}
                        />
                        <Input
                            labelDescription="Status"
                            type="text"
                            name="active"
                            key={"active"}
                            disabled={true}
                            value={schedule.active}
                        />
                        <div className="flex flex-row justify-center pt-2">
                            <ButtonDefault
                                Description={schedule.active === "Ativo" ? "Desativar" : "Ativar"}
                                Name="save-schedule"
                                Type="button"
                                onClick={() => updateDoctorSchedule(schedule.id, schedule.active === "Ativo" ? false : true)}
                            />
                        </div>
                    </div>

                </div>
            ))}
        </div>
    );
}

export default DoctorScheduleForDoctor;