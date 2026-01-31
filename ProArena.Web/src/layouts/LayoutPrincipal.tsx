import '../styles/layout.css'
import { Outlet } from 'react-router-dom'
import Sidebar from '../components/Sidebar'
import Header from '../components/Header'

export default function LayoutPrincipal() {
  return (
    <div className="app-layout">
      <Sidebar />
      <div className="main">
        <Header />
        <div className="page">
          <Outlet />
        </div>
      </div>
    </div>
  )
}
