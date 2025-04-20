import { Link } from 'react-router-dom';

const MainNavbar = () => {
    return (
        <div className='flex flex-row transition delay-150 duration-300 ease-in-out hover:-translate-y-1 hover:scale-110 hover: w-10 pl-8'>
            <nav>
                <ul>
                    <li><Link to='/'>Inicio</Link></li>
                </ul>
            </nav>
        </div>
    );
}

export default MainNavbar;