import DoctorsList from "../components/DoctorsList";

const Home = () => {
    return (
        <div>
            <h1>Médicos Disponíveis</h1>
            <p>Aqui você pode verificar médicos disponíveis</p>
            <DoctorsList />
        </div>

    );
}

export default Home;