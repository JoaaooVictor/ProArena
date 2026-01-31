import Navbar from '../components/Navbar'
import Footer from '../components/Footer'
import { Outlet } from 'react-router-dom'
import '../styles/main.css'

export default function MainLayout() {
  return (
    <>
      <Navbar />
      <main className="content">
        <Outlet />
      </main>
      <Footer />
    </>
  )
}
