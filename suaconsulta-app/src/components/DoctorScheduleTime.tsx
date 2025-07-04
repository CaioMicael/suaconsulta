import { useEffect, useState } from "react";
import api from "../services/api";

interface DoctorScheduleTimeProps {
    DoctorId: number;
    date: string;
}

interface TimeOption {
    hour: string;
    minute: string;
}

/**
 * Componente que contém os horários disponíveis na agenda do médico
 * @param {DoctorScheduleTimeProps} DoctorScheduleTimeProps
 * @returns JSX.Element
 */
const DoctorScheduleTime = ({ DoctorId, date }: DoctorScheduleTimeProps) => {
    const [selectedTime, setSelectedTime] = useState<string | null>(null);
    const [availableTimes, setAvailableTimes] = useState<TimeOption[]>([]);

    const loadAvailableTimes = async () => {
        try {
            const response = await api.get(`DoctorSchedule/ListAvailableTimes?DoctorId=${DoctorId}&date=${date}`);
            response.data.map((timeOption: TimeOption) => {
                timeOption.minute = timeOption.minute.toString().padStart(2, '0');
                setAvailableTimes(prev => [...prev, { hour: timeOption.hour, minute: timeOption.minute }]);
            });
        } catch (error) {
            console.error("Erro ao carregar horários disponíveis:", error);
        }
    };

    useEffect(() => {
        if (DoctorId && date) {
            loadAvailableTimes();
        }
    }, [DoctorId, date]);

    const handleTimeSelect = (time: TimeOption) => {
        setSelectedTime(`${time.hour}:${time.minute}`);
    };

    return (
        <div>
            {availableTimes.length > 0 ? (
                availableTimes.map((timeOption) => (
                    <button
                        key={timeOption.hour + ':' + timeOption.minute}
                        className="inline-flex items-center justify-center w-full p-2 text-sm font-medium text-center bg-white border rounded-lg cursor-pointer text-blue-600 border-blue-600 dark:hover:text-white dark:border-blue-500 dark:peer-checked:border-blue-500 peer-checked:border-blue-600 peer-checked:bg-blue-600 hover:text-white peer-checked:text-white  dark:peer-checked:text-white hover:bg-blue-500 dark:text-blue-500 dark:bg-gray-900 dark:hover:bg-blue-600 dark:hover:border-blue-600 dark:peer-checked:bg-blue-500"
                        onClick={() => handleTimeSelect(timeOption)}
                    >
                        {timeOption.hour}:{timeOption.minute}
                    </button>
                ))
            ) : (
                <p>Nenhum horário disponível.</p>
            )}
        </div>
    );
};

export default DoctorScheduleTime;