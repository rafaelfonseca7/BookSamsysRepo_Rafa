import { useEffect, useState } from "react";
import { Book } from "../models/Book";
import { getBooks, deleteBook } from "../services/BookService";
import { GridColDef, DataGrid, GridRowSelectionModel } from '@mui/x-data-grid';
import { Box, Button, Stack, Typography } from "@mui/material";
import BookModal from "../components/BookModal";

function BooksList() {

    const [books, setBooks] = useState<Book[]>([]);
    const [selectedBooks, setSelectedBooks] = useState<GridRowSelectionModel>([]);
    const [openModal, setOpenModal] = useState(false);
    const [editBook, setEditBook] = useState<Book | null>(null);

    useEffect(() => {
        fetchBooks();
    }, []);

    const fetchBooks = async () => {
        const data = await getBooks();
        setBooks(data);
    };

    const handleDeleteBook = async () => {
        for (const isbn of selectedBooks) {
            await deleteBook(isbn as string);
        }
        setSelectedBooks([]);
        fetchBooks();
    };

    const handleOpenModal = (book?: Book) => {
        setEditBook(book || null);
        setOpenModal(true);
    };

    const handleCloseModal = () => {
        setEditBook(null);
        setOpenModal(false);
        fetchBooks();
    };

    const columns: GridColDef[] = [
        { field: 'isbn', headerName: 'ISBN', width: 150 },
        { field: 'title', headerName: 'Title', width: 250 },
        { field: 'authorName', headerName: 'Author', width: 200 },
        { field: 'price', headerName: 'Price (€)', width: 150 },
    ];

    return (
        <Box sx={{ height: 400, width: '100%' }}>
            <Typography variant="h4" sx={{ marginBottom: 2, textAlign: 'center' }}>
                Books Samsys List
            </Typography>

            <Stack direction="row" spacing={2} sx={{ marginBottom: 2 }}>
                <Button 
                    variant="contained" 
                    color="primary"
                    onClick={() => handleOpenModal()}
                >
                    Add Book
                </Button>
                <Button
                    variant="contained"
                    color="primary"
                    onClick={() => handleOpenModal(books.find(book => selectedBooks.includes(book.isbn)))}
                    disabled={selectedBooks.length !== 1}
                >
                    Edit Book
                </Button>
                <Button
                    variant="contained"
                    color="secondary"
                    onClick={handleDeleteBook}
                    disabled={selectedBooks.length === 0}
                >
                    Delete Books
                </Button>
            </Stack>

            <DataGrid
                rows={books}
                columns={columns}
                getRowId={(row) => row.isbn}
                pagination
                pageSizeOptions={[5]}
                checkboxSelection
                onRowSelectionModelChange={(newSelection: GridRowSelectionModel) => setSelectedBooks(newSelection)}
                disableRowSelectionOnClick
            />

            {openModal && (
                <BookModal
                    open={openModal}
                    onClose={handleCloseModal}
                    onSave={fetchBooks}
                    editBook={editBook}
                />
            )}
            
        </Box>
    );

}

export default BooksList;