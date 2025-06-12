import { useState } from "react";
import Input from "./Input";

const PasswordInput = () => {
    const [showPassword, setShowPassword] = useState(false);
    return (
        <div>
            <Input
                labelDescription="Senha"
                type={showPassword ? "text" : "password"}
                name="password"
                required
            />
            <button type="button" onClick={() => setShowPassword(!showPassword)}>{showPassword ? "Ocultar" : "Mostrar"}</button>
        </div>
    )
}

export default PasswordInput;