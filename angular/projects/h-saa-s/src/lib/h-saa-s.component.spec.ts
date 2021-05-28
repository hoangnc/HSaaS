import { ComponentFixture, TestBed, waitForAsync } from '@angular/core/testing';

import { HSaaSComponent } from './h-saa-s.component';

describe('HSaaSComponent', () => {
  let component: HSaaSComponent;
  let fixture: ComponentFixture<HSaaSComponent>;

  beforeEach(waitForAsync(() => {
    TestBed.configureTestingModule({
      declarations: [ HSaaSComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(HSaaSComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
