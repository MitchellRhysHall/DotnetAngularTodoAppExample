import { Component, OnInit } from '@angular/core';
import { TodoService } from '../../services/todo/todo';
import { FormsModule } from "@angular/forms";

@Component({
  selector: 'app-todo',
  standalone: true,
  imports: [
    FormsModule
  ],
  templateUrl: './todo.html',
  styleUrl: './todo.scss'
})
export class TodoComponent implements OnInit {
  todos: any[] = [];
  newTitle = '';
  newDescription = '';

  constructor(private todoService: TodoService) {}

  ngOnInit() {
    this.loadTodos();
  }

  loadTodos() {
    this.todoService.getTodos().subscribe((res: any) => {
      this.todos = res;
    });
  }

  addTodo() {
    if (!this.newTitle.trim()) return;
    this.todoService.createTodo(this.newTitle, this.newDescription).subscribe(() => {
      this.newTitle = '';
      this.newDescription = '';
      this.loadTodos();
    });
  }

  deleteTodo(title: string) {
    return this.todoService.deleteTodo(title).subscribe(() => this.loadTodos());
  }

  completeTodo(title: string) {
    return this.todoService.completeTodo(title).subscribe(() => this.loadTodos());
  }
}
