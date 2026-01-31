import LayoutAutenticacao from './layouts/LayoutAutenticacao'
import LayoutPrincipal from './layouts/LayoutPrincipal'
import Auth from './pages/Auth'
import Home from './pages/Home'
import { Routes, Route } from 'react-router-dom'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import PrivateRoute from './routes/PrivateRoute'

export default function App() {
  return (
    <>
      <ToastContainer
        position="top-right"
        autoClose={5000}
      />

      <Routes>
        <Route
          element={
            <PrivateRoute>
              <LayoutPrincipal />
            </PrivateRoute>
          }
        >
          <Route path="/" element={<Home />} />
        </Route>

        <Route element={<LayoutAutenticacao />}>
          <Route path="/login" element={<Auth />} />
        </Route>
      </Routes>
    </>
  )
}
