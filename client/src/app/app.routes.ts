import { Routes } from '@angular/router';
import { authGuard } from './guards/auth.guard';
import { LoginComponent } from './components/login/login';
import { TodoComponent } from './components/todo/todo';
import { RegisterComponent } from './components/register/register';

export const routes: Routes = [
  { path: '', redirectTo: 'todos', pathMatch: 'full' },
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  {
    path: 'todos',
    canActivate: [authGuard],
    component: TodoComponent
  }
];

