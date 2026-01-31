import { Routes, Route } from 'react-router-dom'
import LayoutPrincipal from './layouts/LayoutPrincipal'
import Dashboard from './pages/Dashboard'
import Login from './pages/Login'
import LayoutAutenticacao from './layouts/LayoutAutenticacao'

export default function App() {
  return (
    <Routes>
      <Route element={<LayoutPrincipal />}>
        <Route path="/" element={<Dashboard />} />
        <Route path="/login" element={<Login />} />
      </Route>

      <Route element={<LayoutAutenticacao />}>
        <Route path="/login" element={<Login />} />
      </Route>
    </Routes>
  )
}
