import { BrowserRouter, Routes, Route } from 'react-router-dom';

import Compartilhado from './Layout/Compartilhado';
import Home from './pages/Home';

import './global.css';

export default function App() {

  return (
    <BrowserRouter>
      <Routes>
        <Route path="/" element={<Compartilhado />}>
          <Route index element={<Home />} />
          {/* Exemplo comentado de rotas adicionais: */}
          {/* <Route path="alunos" element={<ListaAlunos a={0} texto="TEXT" seila={true} />} /> */}
          {/* <Route path="alunos/criar" element={<FormAluno />} /> */}
          {/* <Route path="alunos/editar/:codigo" element={<FormAluno />} /> */}
          {/* <Route path="cursos" element={<CursosList />} /> */}
          <Route path="*" element={<h1>404 - NOT FOUND</h1>} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}

