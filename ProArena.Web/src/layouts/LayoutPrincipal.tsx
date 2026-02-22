import '../styles/layout.css'
import { Outlet } from 'react-router-dom'
import Sidebar from '../components/Sidebar/Sidebar'
import Footer from '../components/Footer/Footer'

export default function LayoutPrincipal() {
  return (
    <div className="app-layout">
      <Sidebar />
      <div className="main">
        <div className="page">
          <Outlet />
        </div>
        <Footer/>
      </div>
    </div>
  )
}
