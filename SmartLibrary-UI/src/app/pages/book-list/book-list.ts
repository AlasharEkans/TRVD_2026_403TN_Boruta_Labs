import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { BookService } from '../../services/book.service';
import { AuthService } from '../../services/auth.service';
import { Book } from '../../models/book.model';
import { NgFor, NgIf } from '@angular/common';
import { RouterLink } from '@angular/router';
import { NotificationService } from '../../services/notification.service';

@Component({
  selector: 'app-book-list',
  standalone: true,
  imports: [NgFor, NgIf, RouterLink],
  templateUrl: './book-list.html'
})
export class BookList implements OnInit {
  books: Book[] = [];
  isAdmin = false;
  isLoading = true;

  constructor(
    private bookService: BookService,
    private authService: AuthService,
    private cdr: ChangeDetectorRef,
    private notify: NotificationService
  ) {}

  ngOnInit() {
    this.isAdmin = this.authService.getUserRole() === 'Admin';
    this.loadBooks();
  }

  loadBooks() {
    this.isLoading = true;
    this.bookService.getBooks().subscribe({
      next: (data) => {
        this.books = data;
        this.isLoading = false;
        this.cdr.detectChanges(); // Залишаємо для стабільного рендерингу
      },
      error: () => {
        this.isLoading = false;
        this.cdr.detectChanges();
      }
    });
  }

  deleteBook(id: number) {
    if (confirm('Ви впевнені, що хочете видалити цю книгу? Цю дію неможливо скасувати.')) {
      this.bookService.deleteBook(id).subscribe({
        next: () => {
          this.notify.show('Книгу успішно видалено!', 'success');
          this.loadBooks(); 
        },
        error: () => this.notify.show('Помилка при видаленні', 'error')
      });
    }
}
}