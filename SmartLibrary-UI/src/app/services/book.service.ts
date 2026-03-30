import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Book } from '../models/book.model';

@Injectable({ providedIn: 'root' })
export class BookService {
  private apiUrl = 'http://localhost:5222/api/books'; 

  constructor(private http: HttpClient) {}

 
  getBookById(id: number) {
    return this.http.get<Book>(`${this.apiUrl}/${id}`);
  }

  
  updateBook(id: number, bookData: any) {
    return this.http.put(`${this.apiUrl}/${id}`, bookData);
  }

  createBook(bookData: any) {
    return this.http.post<Book>(this.apiUrl, bookData);
  }
  
  getBooks() {
    return this.http.get<Book[]>(this.apiUrl);
  }

  deleteBook(id: number) {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
}