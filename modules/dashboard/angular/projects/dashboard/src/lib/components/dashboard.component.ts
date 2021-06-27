import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services/dashboard.service';

@Component({
  selector: 'lib-dashboard',
  template: ` <p>dashboard works!</p> `,
  styles: [],
})
export class DashboardComponent implements OnInit {
  constructor(private service: DashboardService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
