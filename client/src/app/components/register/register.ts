import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth/auth';
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './register.html',
  styleUrl: './register.scss'
})
export class RegisterComponent {
  form: FormGroup;
  error: string = '';

  constructor(
    private fb: FormBuilder,
    private auth: AuthService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required]
    });

    this.route.queryParams.subscribe(params => {
      if (params['reason'] === 'noaccount') {
        this.error = 'You must register before logging in.';
      }
    });
  }

  submit() {
    const { email, password } = this.form.value;
    this.auth.register(email, password).subscribe({
      next: (res) => {
        console.log('Register success', res);
        this.router.navigate(['/login']);
      },
      error: (err) => {
        console.log('Register failed', err);
        this.error = 'Registration failed. Ensure your password contains at least one number and one uppercase letter and is at least 8 characters long.';
      }
    });
  }
}
