import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.scss'
})
export class LoginComponent {
  form: FormGroup;
  error: string = '';
  showRegisterLink: boolean = false;

  constructor(private fb: FormBuilder, private auth: AuthService, private router: Router) {
    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });
  }

  submit() {
    if (this.form.invalid) {
      this.error = 'Please enter both email and password.';
      this.showRegisterLink = false;
      return;
    }

    const { email, password } = this.form.value;

    this.auth.login(email, password).subscribe({
      next: () => {
        this.router.navigate(['/todos']);
      },
      error: (err) => {
        console.log('Login failed', err);

        if (err.status === 401) {
          this.error = 'Incorrect email or password. Don’t have an account?';
          this.showRegisterLink = true;
        } else {
          this.error = 'Login failed. Please try again.';
          this.showRegisterLink = false;
        }
      }
    });
  }
}
