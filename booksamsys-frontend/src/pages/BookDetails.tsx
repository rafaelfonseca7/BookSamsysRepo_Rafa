import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Book } from "../models/Book";
import { getBook } from "../services/BookService";
import { Box, Typography, Button, Card, CardContent, CardActions, Divider } from "@mui/material";

function BookDetails() {
    const { isbn } = useParams<{ isbn: string }>();
    const [book, setBook] = useState<Book | null>(null);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchBookDetails = async () => {
            if (isbn) {
                const fetchedBook = await getBook(isbn);
                setBook(fetchedBook);
            }
        };

        fetchBookDetails();
    }, [isbn]);

    const handleBackClick = () => {
        navigate(-1); // Voltar para a página anterior
    };

    return (
        <Box sx={{ padding: 4, display: "flex", justifyContent: "center", alignItems: "center", minHeight: "100vh" }}>
            {book ? (
                <Card sx={{ maxWidth: 500, width: "100%", boxShadow: 3 }}>
                    <CardContent>
                        <Typography variant="h4" sx={{ marginBottom: 2, textAlign: "center" }}>
                            {book.title}
                        </Typography>
                        <Divider sx={{ marginBottom: 2 }} />
                        <Typography variant="body1" sx={{ marginBottom: 1 }}>
                            <strong>ISBN:</strong> {book.isbn}
                        </Typography>
                        <Typography variant="body1" sx={{ marginBottom: 1 }}>
                            <strong>Author:</strong> {book.authorName}
                        </Typography>
                        <Typography variant="body1" sx={{ marginBottom: 1 }}>
                            <strong>Price:</strong> €{book.price.toFixed(2)}
                        </Typography>
                    </CardContent>
                    <CardActions sx={{ justifyContent: "center" }}>
                        <Button variant="contained" color="primary" onClick={handleBackClick}>
                            Back to List
                        </Button>
                    </CardActions>
                </Card>
            ) : (
                <Typography variant="h6">Loading book details...</Typography>
            )}
        </Box>
    );
}

export default BookDetails;