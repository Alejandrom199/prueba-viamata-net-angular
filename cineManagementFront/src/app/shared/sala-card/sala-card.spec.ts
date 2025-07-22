import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SalaCard } from './sala-card';

describe('SalaCard', () => {
  let component: SalaCard;
  let fixture: ComponentFixture<SalaCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SalaCard]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SalaCard);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
