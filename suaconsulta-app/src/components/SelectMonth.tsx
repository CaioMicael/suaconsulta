import React from "react";
import Select from "./Select";

interface SelectMonthProps {
    excludeMonths?: number[];
    months?: { value: number; label: string }[];
    onChange?: (event: React.ChangeEvent<HTMLSelectElement>) => void;
    onSelect?: (event: React.ChangeEvent<HTMLSelectElement>) => void;
}

const SelectMonth = ({
    excludeMonths = [] as number[],
    months = [
        { value: 1, label: "Janeiro" },
        { value: 2, label: "Fevereiro" },
        { value: 3, label: "Março" },
        { value: 4, label: "Abril" },
        { value: 5, label: "Maio" },
        { value: 6, label: "Junho" },
        { value: 7, label: "Julho" },
        { value: 8, label: "Agosto" },
        { value: 9, label: "Setembro" },
        { value: 10, label: "Outubro" },
        { value: 11, label: "Novembro" },
        { value: 12, label: "Dezembro" }
    ],
    onChange,
    onSelect
}:SelectMonthProps) => {
    return (
        <Select
            labelDescription="Mês"
            name="mes"
            id="mes"
            onChange={onChange}
            onSelect={onSelect}
            options={[months.filter((month) => !excludeMonths.includes(month.value))].flat()}
        />
    )
}

export default SelectMonth;