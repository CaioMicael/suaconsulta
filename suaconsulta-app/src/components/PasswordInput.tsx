import { useState } from "react";
import Input from "./Input";

interface PasswordInputProps {
    onChange?: (event: React.ChangeEvent<HTMLInputElement>) => void;
}

const PasswordInput = ({ onChange }: PasswordInputProps) => {
    const [showPassword, setShowPassword] = useState(false);
    return (
        <div>
            <Input
                labelDescription="Senha"
                type={showPassword ? "text" : "password"}
                name="password"
                onChange={() => onChange}
                required
            />
            <button type="button" onClick={() => setShowPassword(!showPassword)}>{showPassword ? "Ocultar" : "Mostrar"}</button>
        </div>
    )
}

export default PasswordInput;