import axios from "axios";

/**
 * função base para criar uma instância do axios com a URL base da API para autenticações.
 * @returns {AxiosInstance} - Instância do axios configurada com a URL base.
 */
const apiAuth = axios.create(
    {
        baseURL: "http://localhost:7156/api/Auth"
    }
);

export default apiAuth;