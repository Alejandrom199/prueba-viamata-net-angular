import { ComponentFixture, TestBed } from '@angular/core/testing';

import { MiDialogo } from './mi-dialogo';

describe('MiDialogo', () => {
  let component: MiDialogo;
  let fixture: ComponentFixture<MiDialogo>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [MiDialogo]
    })
    .compileComponents();

    fixture = TestBed.createComponent(MiDialogo);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
