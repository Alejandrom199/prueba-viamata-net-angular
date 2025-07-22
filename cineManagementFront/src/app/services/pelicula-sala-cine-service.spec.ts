import { TestBed } from '@angular/core/testing';

import { PeliculaSalaCineService } from './pelicula-sala-cine-service';

describe('PeliculaSalaCineService', () => {
  let service: PeliculaSalaCineService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PeliculaSalaCineService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
