import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPeliculas } from './view-peliculas';

describe('ViewPeliculas', () => {
  let component: ViewPeliculas;
  let fixture: ComponentFixture<ViewPeliculas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewPeliculas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewPeliculas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
