import { BrowserRouter, Routes, Route } from "react-router-dom";
import { HomePage } from "../pages/HomePage";
import { CampeonatoPage} from '../pages/CampeonatoPage';

export function AppRoutes(){
    return(
        <BrowserRouter>
            <Routes>
                <Route path="/" element={<HomePage/>} />
                <Route path="/campeonatos" element={<CampeonatoPage/>} />
            </Routes>
        </BrowserRouter>
    )
}