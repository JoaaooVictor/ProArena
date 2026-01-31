import Navbar from '../components/Navbar'
import { Outlet } from 'react-router-dom'

export default function LayoutPublico() {
  return (
    <>
      <Navbar />
      <Outlet />
    </>
  )
}
