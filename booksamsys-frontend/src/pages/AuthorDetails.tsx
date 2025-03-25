import { useEffect, useState } from "react";
import { useParams, useNavigate } from "react-router-dom";
import { Author } from "../models/Author";
import { Book } from "../models/Book";
import { getAuthor, getBooksFromAuthor } from "../services/AuthorService";
import { Typography, Box, IconButton } from "@mui/material";
import { GridColDef, DataGrid } from "@mui/x-data-grid";
import BookIcon from '@mui/icons-material/Book';
import ArrowBackIcon from '@mui/icons-material/ArrowBack';

function AuthorDetails() {
    const { authorId } = useParams<{ authorId: string }>();
    const [author, setAuthor] = useState<Author | null>(null);
    const [books, setBooks] = useState<Book[]>([]);
    const navigate = useNavigate();

    useEffect(() => {
        const fetchAuthorData = async () => {
            const fetchedAuthor = await getAuthor(Number(authorId));
            setAuthor(fetchedAuthor);

            const fetchedBooks = await getBooksFromAuthor(Number(authorId));
            setBooks(fetchedBooks);
        };

        fetchAuthorData();
    }, [authorId]);

    const handleBookClick = (isbn: string) => {
        navigate(`/book/${isbn}`);
    };

    const handleBackClick = () => {
        navigate(-1);
    };
    const columns: GridColDef[] = [
        {
            field: "actions",
            headerName: "Actions",
            width: 100,
            renderCell: (params) => (
                <>
                    <IconButton onClick={handleBackClick}>
                        <ArrowBackIcon />
                    </IconButton>
                    <IconButton onClick={() => handleBookClick(params.row.isbn)}>
                        <BookIcon />
                    </IconButton>
                </>
            ),
        },
        { field: "isbn", headerName: "ISBN", width: 150 },
        { field: "title", headerName: "Title", width: 250 },
        { field: "authorName", headerName: "Author", width: 200 },
        { field: "price", headerName: "Price (€)", width: 150 }
    ];

    return (
        <Box sx={{ padding: 4 }}>
            {author ? (
                <>
                    <Typography variant="h4">Books by {author.name}:</Typography>
                    <Box sx={{ height: 400, width: "100%", marginTop: 2 }}>
                        <DataGrid
                            rows={books}
                            columns={columns}
                            getRowId={(row) => row.isbn}
                            pagination
                            pageSizeOptions={[5, 10, 20]}
                        />
                    </Box>
                </>
            ) : (
                <Typography>Loading author details...</Typography>
            )}
        </Box>
    );
}

export default AuthorDetails;
