import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-employee-actions',
  templateUrl: './employee-actions.component.html',
  styleUrls: ['./employee-actions.component.css']
})
export class EmployeeActionsComponent implements OnInit {

  numberOfEmployees: number;
  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {
  }

  onGenerateDataClick(){
    this.employeeService.generateChartData(this.numberOfEmployees);
  }

  onDeleteDataClick() {
    this.employeeService.deleteChartData();
  }
}
