import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { BookService } from '../../services/book.service';
import { NotificationService } from '../../services/notification.service';
import { Router, ActivatedRoute, RouterLink } from '@angular/router';
import { NgIf } from '@angular/common';

@Component({
  selector: 'app-book-form',
  standalone: true,
  imports: [ReactiveFormsModule, NgIf, RouterLink],
  templateUrl: './book-form.html'
})
export class BookForm implements OnInit {
  bookForm: FormGroup;
  isEditMode = false;
  currentBookId!: number;
  isLoading = false;

  constructor(
    private fb: FormBuilder, 
    private bookService: BookService, 
    private router: Router,
    private route: ActivatedRoute,
    private notify: NotificationService
  ) {
    this.bookForm = this.fb.group({
      id: [0], // Приховане поле
      title: ['', Validators.required],
      isbn: ['', Validators.required],
      publicationYear: [new Date().getFullYear(), [Validators.required, Validators.min(1000)]],
      description: ['']
    });
  }

  ngOnInit() {
    
    const idParam = this.route.snapshot.paramMap.get('id');
    if (idParam) {
      this.isEditMode = true;
      this.currentBookId = +idParam;
      this.loadBookData();
    }
  }

  loadBookData() {
    this.isLoading = true;
    this.bookService.getBookById(this.currentBookId).subscribe({
      next: (book) => {
        this.bookForm.patchValue(book); // Автоматично заповнює форму даними
        this.isLoading = false;
      },
      error: () => {
        this.notify.show('Помилка завантаження даних книги', 'error');
        this.router.navigate(['/books']);
      }
    });
  }

  onSubmit() {
    if (this.bookForm.invalid) return;

    if (this.isEditMode) {
      this.bookService.updateBook(this.currentBookId, this.bookForm.value).subscribe({
        next: () => {
          this.notify.show('Книгу успішно оновлено!', 'success');
          this.router.navigate(['/books']);
        },
        error: () => this.notify.show('Помилка при оновленні', 'error')
      });
    } else {
      // Видаляємо id перед створенням, щоб бекенд сам його згенерував
      const { id, ...newBook } = this.bookForm.value; 
      this.bookService.createBook(newBook).subscribe({
        next: () => {
          this.notify.show('Нову книгу додано!', 'success');
          this.router.navigate(['/books']);
        },
        error: () => this.notify.show('Помилка при створенні', 'error')
      });
    }
  }
}