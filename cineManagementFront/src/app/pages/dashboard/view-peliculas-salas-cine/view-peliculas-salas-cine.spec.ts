import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewPeliculasSalasCine } from './view-peliculas-salas-cine';

describe('ViewPeliculasSalasCine', () => {
  let component: ViewPeliculasSalasCine;
  let fixture: ComponentFixture<ViewPeliculasSalasCine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewPeliculasSalasCine]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewPeliculasSalasCine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
