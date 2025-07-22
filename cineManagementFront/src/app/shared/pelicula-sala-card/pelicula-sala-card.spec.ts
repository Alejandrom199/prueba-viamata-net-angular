import { ComponentFixture, TestBed } from '@angular/core/testing';

import { PeliculaSalaCard } from './pelicula-sala-card';

describe('PeliculaSalaCard', () => {
  let component: PeliculaSalaCard;
  let fixture: ComponentFixture<PeliculaSalaCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [PeliculaSalaCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(PeliculaSalaCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
