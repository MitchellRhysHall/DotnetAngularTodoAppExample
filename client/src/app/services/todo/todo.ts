import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class TodoService {
  constructor(private http: HttpClient) { }

  getTodos() {
    return this.http.post('/api/todo/read-all', {}, { withCredentials: true });
  }
  getTodo(title: string) {
    return this.http.post('/api/todo/read', { title }, { withCredentials: true });
  }

  createTodo(title: string, description: string) {
    return this.http.post('/api/todo/create', { title, description }, { withCredentials: true });
  }

  updateTodo(title: string, description: string) {
    return this.http.post('/api/todo/update', { title, description }, { withCredentials: true });
  }

  deleteTodo(title: string) {
    return this.http.post('/api/todo/delete', { title }, { withCredentials: true });
  }

  completeTodo(title: string) {
    return this.http.post('/api/todo/complete', { title }, { withCredentials: true });
  }
}

