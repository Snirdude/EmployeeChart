import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { environment } from 'src/environments/environment';
import { EmployeesPerMonthPerYear } from '../_models/employeesPerMonthPerYear';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  baseUrl = environment.apiUrl;
  private employeesPerMonthPerYearSource = new BehaviorSubject<EmployeesPerMonthPerYear[]>([]);
  employeesPerMonthPerYear$ = this.employeesPerMonthPerYearSource.asObservable();

  constructor(private http: HttpClient) { }

  getChartData() {
    this.http.get<EmployeesPerMonthPerYear[]>(this.baseUrl + 'employees').subscribe(response => {
      this.employeesPerMonthPerYearSource.next(response);
    });
    return this.employeesPerMonthPerYear$;
  }

  generateChartData(numberOfEmployees: number){
    this.http.post<EmployeesPerMonthPerYear[]>(this.baseUrl + 'employees/generate', {numberOfEmployees: numberOfEmployees}).subscribe(response => {
      this.employeesPerMonthPerYearSource.next(response);
    });
  }

  deleteChartData(){
    this.http.delete<EmployeesPerMonthPerYear[]>(this.baseUrl + 'employees').subscribe(response => {
      this.employeesPerMonthPerYearSource.next(response);
    });
  }
}
