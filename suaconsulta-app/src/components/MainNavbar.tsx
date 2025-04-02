import { Link } from 'react-router-dom';

const MainNavbar = () => {
    return (
        <nav>
            <ul>
                <li><Link to='/'>Inicio</Link></li>
            </ul>
        </nav>
    );
}

export default MainNavbar;