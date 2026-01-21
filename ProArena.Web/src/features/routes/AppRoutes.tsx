import { BrowserRouter, Routes, Route } from "react-router-dom";
import { HomePage } from "../pages/HomePage";
import { CampeonatoPage} from '../pages/CampeonatoPage';
import { SobrePage} from '../pages/SobrePage';
import { LoginPage } from "../pages/LoginPage";

export function AppRoutes(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<HomePage/>} />
                <Route path="/login" element={<LoginPage/>} />
                <Route path="/campeonatos" element={<CampeonatoPage/>} />
                <Route path="/sobre" element={<SobrePage/>} />
            </Routes>
        </BrowserRouter>
    )
}