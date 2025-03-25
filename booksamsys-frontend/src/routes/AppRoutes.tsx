import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import BooksList from "../pages/BooksList";
import AuthorDetails from "../pages/AuthorDetails";
import BookDetails from "../pages/BookDetails";

function AppRoutes() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<BooksList />} />
                <Route path="/author/:authorId" element={<AuthorDetails />} />
                <Route path="/book/:isbn" element={<BookDetails />} />
            </Routes>
        </Router>
    );
}

export default AppRoutes;