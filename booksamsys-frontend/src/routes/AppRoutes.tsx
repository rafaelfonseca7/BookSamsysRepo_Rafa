import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import BooksList from "../pages/BooksList";

function AppRoutes() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<BooksList />} />
            </Routes>
        </Router>
    );
}

export default AppRoutes;