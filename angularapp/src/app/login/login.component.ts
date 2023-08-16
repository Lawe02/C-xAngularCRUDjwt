import { Component } from '@angular/core';
import { LoginService } from '../login.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  username: string = '';
  password: string = '';

  constructor(private loginService: LoginService) { }

  onSubmit() {
    this.loginService.login(this.username, this.password).subscribe(
      response => {
        // Assuming the API returns a JWT token upon successful login
        const token = response.token;
        localStorage.setItem('token', token);
        // You can navigate to a protected route here if needed
      },
      error => {
        console.error('Login failed', error);
      }
    );
  }
}
