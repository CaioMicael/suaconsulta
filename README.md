# SuaConsulta

Sistema completo para agendamento, gerenciamento e acompanhamento de consultas médicas. Inclui API backend em ASP.NET Core e frontend em React com TypeScript.

## 📦 Estrutura do Projeto

```
suaconsulta/
├── suaconsulta-api/      # Backend ASP.NET Core (API REST)
├── suaconsulta-app/      # Frontend React + TypeScript
└── README.md             # Este arquivo
```

---

## 🚀 Tecnologias Utilizadas

- **Backend:** ASP.NET Core, Entity Framework Core, PostgreSQL, JWT Auth
- **Frontend:** React, TypeScript, TailwindCSS, Axios
- **Infra:** Docker, Docker Compose

---

## 🏥 Funcionalidades Principais

- Cadastro e autenticação de usuários (paciente, médico)
- Agendamento, edição e cancelamento de consultas
- Visualização de consultas agendadas, concluídas e histórico
- Cadastro de médicos, pacientes e horários disponíveis
- Validação de dados e máscaras de input
- Interface moderna e responsiva

---

## ⚙️ Como rodar o projeto

### 1. Clonar o repositório

```bash
git clone https://github.com/CaioMicael/suaconsulta.git
cd suaconsulta
```

### 2. Backend (.NET API)

```bash
cd suaconsulta-api
# Configure o appsettings.json conforme seu ambiente
# Rode as migrations e o servidor:
dotnet ef database update
dotnet run
```

- A API estará disponível em: `https://localhost:5001` ou `http://localhost:5000`

### 3. Frontend (React)

```bash
cd suaconsulta-app
npm install
npm start
```

- O app estará disponível em: `http://localhost:3000`

### 4. Usando Docker (opcional)

```bash
# Backend
cd suaconsulta-api
docker build -t suaconsulta-api .
docker run -p 5000:80 suaconsulta-api

# Frontend
cd suaconsulta-app
docker build -t suaconsulta-app .
docker run -p 3000:80 suaconsulta-app
```

---

## 📂 Estrutura de Pastas

- `suaconsulta-api/Controllers` — Endpoints da API
- `suaconsulta-api/Model` — Modelos de dados (Consultation, Doctor, Patient, etc)
- `suaconsulta-api/DTO` — Data Transfer Objects
- `suaconsulta-api/Repositories` — Lógica de acesso a dados
- `suaconsulta-app/src/pages` — Telas principais do frontend
- `suaconsulta-app/src/components` — Componentes reutilizáveis
- `suaconsulta-app/src/services` — Serviços de API (Axios)

---

## 🔑 Exemplos de Endpoints

- `POST /Auth/SignIn` — Login
- `GET /Consultation/PatientConsultations` — Consultas do paciente
- `POST /Consultation` — Agendar consulta
- `PUT /Consultation/{id}` — Editar consulta
- `DELETE /Consultation/{id}` — Cancelar consulta

---

## 🖥️ Telas do Sistema

- Login e cadastro
- Listagem de médicos e pacientes
- Agendamento de consulta
- Visualização de consultas (agendadas, concluídas, canceladas)
- Histórico do paciente

---

## 👨‍💻 Contribuição

Pull requests são bem-vindos! Sinta-se à vontade para abrir issues ou sugerir melhorias.

---

## 📧 Contato

- Autor: Caio Micael
- Email: caio.micael@gmail.com
- GitHub: [CaioMicael](https://github.com/CaioMicael)

---

> Projeto desenvolvido para fins acadêmicos e demonstração de arquitetura fullstack moderna.
