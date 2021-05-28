import { Component, OnInit } from '@angular/core';
import { HSaaSService } from '../services/h-saa-s.service';

@Component({
  selector: 'lib-h-saa-s',
  template: ` <p>h-saa-s works!</p> `,
  styles: [],
})
export class HSaaSComponent implements OnInit {
  constructor(private service: HSaaSService) {}

  ngOnInit(): void {
    this.service.sample().subscribe(console.log);
  }
}
