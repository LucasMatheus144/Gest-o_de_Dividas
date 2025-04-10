import { Outlet } from 'react-router-dom';
import Navbar  from '../Components/Nav/Navbar'; 

function Layout() {
    return (<>

        <Navbar/>
        <Outlet />
    </>)
}

export default Layout;