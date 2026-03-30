import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { BookList } from './pages/book-list/book-list';
import { authGuard } from './guards/auth.guard';
import { Register } from './pages/register/register';
import { BookForm } from './pages/book-form/book-form';
import { BookDetails } from './pages/book-details/book-details';




export const routes: Routes = [
  { 
    path: 'login', 
    component: Login
  },
  { path: 'register', component: Register },

  { path: 'books/new', component: BookForm, canActivate: [authGuard] }, // Захищений роут
  { 
    path: 'books', 
    component: BookList, 
    canActivate: [authGuard] // Захищений маршрут: без токена сюди не пустить
  },

{ path: 'books/:id', component: BookDetails, canActivate: [authGuard] },
  { path: 'books/edit/:id', component: BookForm, canActivate: [authGuard] },

  { 
    path: '', 
    redirectTo: '/books', 
    pathMatch: 'full' 
  },
  { 
    path: '**', 
    redirectTo: '/login' // Якщо користувач введе неіснуючу адресу, його кине на логін
  }
];