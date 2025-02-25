import { Router } from './Router.tsx'
import { BrowserRouter } from 'react-router-dom'

import './global.css'

export function App() {

  return (
    <>
      <BrowserRouter>
        <Router/>
      </BrowserRouter>  
    </>
  )
}