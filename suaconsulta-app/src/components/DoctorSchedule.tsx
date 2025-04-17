import React from 'react';
import { useEffect, useState } from "react";
import ButtonAgendar from "./ButtonAgendar";

interface DoctorScheduleProps {
    DoctorId: number;
}

const DoctorSchedule = ({DoctorId}: DoctorScheduleProps) => {
    return (
        <div className="medico-disponivel-container">
            <h2>Agendar Consulta</h2>
            <p>Id: {DoctorId}</p>
            <ButtonAgendar labelDescription="Agendar" name="button-agendar" type="button" DoctorId={DoctorId} />
        </div>
    )
}

export default DoctorSchedule;