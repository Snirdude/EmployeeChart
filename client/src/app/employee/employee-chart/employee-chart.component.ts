import { Component, OnInit } from '@angular/core';
import { GoogleChartsModule } from 'angular-google-charts';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-employee-chart',
  templateUrl: './employee-chart.component.html',
  styleUrls: ['./employee-chart.component.css']
})
export class EmployeeChartComponent implements OnInit {

  readonly title = 'Employees Per Month Per Year';
  readonly type = 'ComboChart';
  data: any[][];
  columns: string[];

  constructor(private employeeService: EmployeeService) { }

  ngOnInit(): void {

    this.employeeService.getChartData().subscribe(employeesPerMonthPerYear => {
      // Init data and columns
      this.data = [
        ['Jan'],
        ['Feb'],
        ['Mar'],
        ['Apr'],
        ['May'],
        ['Jun'],
        ['Jul'],
        ['Aug'],
        ['Sep'],
        ['Oct'],
        ['Nov'],
        ['Dec']
      ];
      this.columns = ['Month'];

      // Get unique years from response
      const uniqueYears = employeesPerMonthPerYear.map(x => x.year).filter((value, index, self) => self.indexOf(value) === index).sort((a, b) => a < b ? a : b);
      uniqueYears.forEach(year => {
        this.columns.push(year.toString());
      });

      // Populate data
      for (let i = 0; i < 12; i++) {
        const employeesPerMonth = employeesPerMonthPerYear.filter(x => x.month === i + 1);
        for (let j = Math.min(...uniqueYears); j <= Math.max(...uniqueYears); j++) {
          this.data[i].push(employeesPerMonth.find(x => x.year === j).count);
        }
      }
    });
  }

}
