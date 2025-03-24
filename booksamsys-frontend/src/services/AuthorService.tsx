import api from "./Api";
import { Author } from "../models/Author";

export const getAuthors = async (): Promise<Author[]> => {
    const response = await api.get("/Author");
    return response.data;
}