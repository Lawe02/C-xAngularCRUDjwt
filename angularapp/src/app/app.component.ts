import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
/*  public forecasts?: WeatherForecast[];*/
  public isAuthenticated: boolean = false;

  constructor(private http: HttpClient, private router: Router) { }

  ngOnInit() {
    // Check if the user is authenticated
    const token = localStorage.getItem('token');
    const headers = { Authorization: `Bearer ${token}` };
    if (token) {
      this.isAuthenticated = true;
    }
  }

  login() {
    // Call your login service here and handle successful login
    // After successful login, set isAuthenticated to true
    this.isAuthenticated = true;
/*    this.loadForecasts();*/
  }

  //loadForecasts() {
  //  const token = localStorage.getItem('token');
  //  const headers = { Authorization: `Bearer ${token}` };
  //  this.http.get<WeatherForecast[]>('/weatherforecast', { headers }).subscribe(
  //    result => {
  //      this.forecasts = result;
  //    },
  //    error => console.error(error)
  //  );
  //}
}

//interface WeatherForecast {
//  date: string;
//  temperatureC: number;
//  temperatureF: number;
//  summary: string;
//}
