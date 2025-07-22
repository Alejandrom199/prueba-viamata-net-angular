import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AsignarPeliculaSalaCine } from './asignar-pelicula-sala-cine';

describe('AsignarPeliculaSalaCine', () => {
  let component: AsignarPeliculaSalaCine;
  let fixture: ComponentFixture<AsignarPeliculaSalaCine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AsignarPeliculaSalaCine]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AsignarPeliculaSalaCine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
