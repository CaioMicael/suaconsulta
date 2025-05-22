import axios from "axios";

const api = axios.create(
    {
        baseURL: "https://localhost:7156/api/"
    }
)

export default api