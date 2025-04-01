import { Routes, Route } from 'react-router-dom';

import { Padrao } from '../src/layout/Padrao.tsx'
import  Casa  from '../src/pages/Home.tsx';
import { Divida } from '../src/pages/Divida.tsx';




export function Router(){
    return(

        <Routes>
            <Route path='/' element={<Padrao></Padrao>}>
                <Route path="/" element={<Casa></Casa>}></Route>
                <Route path="/Divida" element={<Divida></Divida>}></Route>
            </Route>
        </Routes>

    );
}