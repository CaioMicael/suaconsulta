import { Link } from 'react-router-dom';

const MainNavbar = () => {
    return (
        <div className="mx-auto max-w-[2024px] px-0 md:px-12 lg:px-11 xl:px-16">
            <div className='grid h-24 grid-cols-header items-center justify-between gap-3'>
                <nav className='flex items-center space-x-8 lg:space-x-5 xl:space-x-8 2xl:space-x-12'>
                    <ul>
                        <li className='group relative font-medium text-blurple'><Link to='/'>Inicio</Link></li>
                    </ul>
                </nav>
            </div>
        </div>
    );
}

export default MainNavbar;