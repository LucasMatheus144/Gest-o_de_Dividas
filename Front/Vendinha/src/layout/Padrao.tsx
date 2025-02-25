import { Outlet } from "react-router-dom";

import { Header } from '../Components/Header/Header';

export function Padrao(){
    return(
        <div>
            <Header />
            <Outlet />
        </div>
    );
}