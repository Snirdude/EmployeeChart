import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-employee-actions',
  templateUrl: './employee-actions.component.html',
  styleUrls: ['./employee-actions.component.css']
})
export class EmployeeActionsComponent implements OnInit {

  numberOfEmployees: number = 0;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
  }

  onNumberOfEmployeesChange() {
    if (this.numberOfEmployees > 100) {
      this.numberOfEmployees = 100;
    }
    if (this.numberOfEmployees < 0) {
      this.numberOfEmployees = 0;
    }
  }

  onGenerateDataClick() {
    if (this.numberOfEmployees > 0) {
      this.employeeService.generateChartData(this.numberOfEmployees);
    }
  }

  onDeleteDataClick() {
    this.employeeService.deleteChartData();
  }
}
