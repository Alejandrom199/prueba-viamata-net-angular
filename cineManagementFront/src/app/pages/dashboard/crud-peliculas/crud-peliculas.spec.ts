import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CrudPeliculas } from './crud-peliculas';

describe('CrudPeliculas', () => {
  let component: CrudPeliculas;
  let fixture: ComponentFixture<CrudPeliculas>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [CrudPeliculas]
    })
    .compileComponents();

    fixture = TestBed.createComponent(CrudPeliculas);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
