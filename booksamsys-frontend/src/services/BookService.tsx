import api from "./Api";
import { Book } from "../models/Book";
import { CreateEditBook } from "../models/CreateEditBook";

export const getBooks = async (): Promise<Book[]> => {
    const response = await api.get("/Book");
    return response.data;
}

export const getBook = async (isbn: string): Promise<Book> => {
    const response = await api.get(`/Book/${isbn}`);
    return response.data;
}

export const createBook = async (book: CreateEditBook): Promise<void> => {
    await api.post("/Book", book);
}

export const updateBook = async (isbn: string, book: CreateEditBook): Promise<void> => {
    await api.put(`/Book/${isbn}`, book);
}

export const deleteBook = async (isbn: string): Promise<void> => {
    await api.delete(`/Book/${isbn}`);
}