import axios from "axios";

/**
 * função base para criar uma instância do axios com a URL base da API.
 * Já coloca automaticamente o token de autenticação no header Authorization.
 * @returns {AxiosInstance} - Instância do axios configurada com a URL base.
 */
const api = axios.create(
    {
        baseURL: "http://localhost:7156/api/"
    }
);

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem('token');
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

export default api