import LayoutAutenticacao from './layouts/LayoutAutenticacao'
import LayoutPrincipal from './layouts/LayoutPrincipal'
import Auth from './pages/Auth'
import Dashboard from './pages/Dashboard'
import { Routes, Route } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'

export default function App() {
  return (
    <>
      <ToastContainer 
        position="top-right" 
        autoClose={5000} 
      />

      <Routes>
        <Route element={<LayoutPrincipal />}>
          <Route path="/" element={<Dashboard />} />
        </Route>

        <Route element={<LayoutAutenticacao />}>
          <Route path="/login" element={<Auth />} />
        </Route>
      </Routes>
    </>
  )
}
