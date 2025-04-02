import { useState } from "react";

const PasswordInput = () => {
    const [showPassword, setShowPassword] = useState(false);
    return (
        <div>
            <label htmlFor="password">Senha</label>
            <input type={showPassword ? "text" : "password"} id="password" name="password" required />
            <button type="button" onClick={() => setShowPassword(!showPassword)}>{showPassword ? "Ocultar" : "Mostrar"}</button>
        </div>
    )
}

export default PasswordInput;