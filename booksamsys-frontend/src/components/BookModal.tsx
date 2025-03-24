import { useEffect, useState } from "react";
import { Book } from "../models/Book";
import { CreateEditBook } from "../models/CreateEditBook";
import { Author } from "../models/Author";
import { createBook, updateBook } from "../services/BookService";
import { getAuthors } from "../services/AuthorService";
import { Box, Button, FormControl, InputLabel, MenuItem, Modal, Select, TextField, Typography } from "@mui/material";

const modalStyle = {
    position: 'absolute',
    top: '50%',
    left: '50%',
    transform: 'translate(-50%, -50%)',
    width: 400,
    bgcolor: 'background.paper',
    borderRadius: 2,
    boxShadow: 24,
    p: 4,
};

interface BookModalProps {
    open: boolean;
    onClose: () => void;
    onSave: () => void;
    editBook?: Book | null;
}

const BookModal: React.FC<BookModalProps> = ({ open, onClose, onSave, editBook }) => {
    const [authors, setAuthors] = useState<Author[]>([]);
    const [isbn, setIsbn] = useState(editBook?.isbn || '');
    const [title, setTitle] = useState(editBook?.title || '');
    const [authorId, setAuthorId] = useState<number>(0);
    const [price, setPrice] = useState(editBook?.price || 0);

    useEffect(() => {
        const fetchAuthors = async () => {
            const data = await getAuthors();
            setAuthors(data);
        };
        fetchAuthors();

        if(editBook && authors.length > 0) {
            const author = authors.find((author) => author.name === editBook.authorName);
            if (author) {
                setAuthorId(author.id);
            }
        }
    }, [editBook, authors]);

    const handleSubmit = async () => {
        const bookData: CreateEditBook = { isbn, title, authorId, price };

        if (editBook) {
            await updateBook(editBook.isbn, bookData);
        } else {
            await createBook(bookData);
        }

        onSave();
        onClose();
    };

    return (
        <Modal open={open} onClose={onClose}>
            <Box sx={modalStyle}>
                <Typography variant="h6" component="h2">
                    {editBook ? 'Edit Book' : 'Add New Book'}
                </Typography>
                <TextField
                    label="ISBN"
                    value={isbn}
                    onChange={(e) => setIsbn(e.target.value)}
                    fullWidth
                    margin="normal"
                    disabled={!!editBook}
                />
                <TextField
                    label="Title"
                    value={title}
                    onChange={(e) => setTitle(e.target.value)}
                    fullWidth
                    margin="normal"
                />
                <TextField
                    label="Price (€)"
                    value={price}
                    onChange={(e) => setPrice(parseFloat(e.target.value))}
                    type="number"
                    fullWidth
                    margin="normal"
                />
                <FormControl fullWidth margin="normal">
                    <InputLabel>Author</InputLabel>
                    <Select
                        value={authorId}
                        onChange={(e) => setAuthorId(Number(e.target.value))}
                    >
                        {authors.map((author) => (
                            <MenuItem key={author.id} value={author.id}>
                                {author.name}
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>
                <Button onClick={handleSubmit} variant="contained" color="primary" sx={{ mt: 2 }}>
                    {editBook ? 'Update' : 'Create'}
                </Button>
            </Box>
        </Modal>
    );
};

export default BookModal;