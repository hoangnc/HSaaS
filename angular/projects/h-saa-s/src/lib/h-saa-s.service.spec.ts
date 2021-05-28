import { TestBed } from '@angular/core/testing';

import { HSaaSService } from './h-saa-s.service';

describe('HSaaSService', () => {
  let service: HSaaSService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(HSaaSService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
