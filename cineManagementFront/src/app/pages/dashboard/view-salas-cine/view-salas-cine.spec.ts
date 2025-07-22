import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ViewSalasCine } from './view-salas-cine';

describe('ViewSalasCine', () => {
  let component: ViewSalasCine;
  let fixture: ComponentFixture<ViewSalasCine>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [ViewSalasCine]
    })
    .compileComponents();

    fixture = TestBed.createComponent(ViewSalasCine);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
