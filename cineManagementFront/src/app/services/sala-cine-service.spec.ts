import { TestBed } from '@angular/core/testing';

import { SalaCineService } from './sala-cine-service';

describe('SalaCineService', () => {
  let service: SalaCineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(SalaCineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
