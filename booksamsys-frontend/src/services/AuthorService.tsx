import api from "./Api";
import { Author } from "../models/Author";
import { Book } from "../models/Book";

export const getAuthors = async (): Promise<Author[]> => {
    const response = await api.get("/Author");
    return response.data;
}

export const getAuthor = async (id: number): Promise<Author> => {
    const response = await api.get(`/Author/id`, { params: { id } });
    return response.data;
}

export const getBooksFromAuthor = async (id: number): Promise<Book[]> => {
    const response = await api.get(`/Author/books`, { params: { id } });
    return response.data;
}