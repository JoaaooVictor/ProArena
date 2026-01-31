import LayoutAutenticacao from './layouts/LayoutAutenticacao'
import LayoutPrincipal from './layouts/LayoutPrincipal'
import LayoutPublico from './layouts/LayoutPublico'
import Auth from './pages/Auth'
import Home from './pages/Home'
import Dashboard from './pages/Dashboard'
import { Routes, Route } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import PrivateRoute from './routes/PrivateRoute'

export default function App() {
  return (
    <>
      <ToastContainer position="top-right" autoClose={5000} />

      <Routes>
        <Route element={<LayoutPublico />}>
          <Route path="/" element={<Home />} />
        </Route>
        <Route path="/login" element={<Auth />} />

        <Route
          element={
            <PrivateRoute>
              <LayoutPrincipal />
            </PrivateRoute>
          }
        >
          <Route path="/dashboard" element={<Dashboard />} />
        </Route>

      </Routes>
    </>
  )
}
