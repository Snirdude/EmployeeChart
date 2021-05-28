import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { EmployeesPerMonthPerYear } from '../_models/employeesPerMonthPerYear';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  getChartData(){
    return this.http.get<EmployeesPerMonthPerYear[]>(this.baseUrl + 'employees');
  }
}
