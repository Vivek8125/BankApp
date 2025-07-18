import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SendMoneyComponent } from './Send-Money.component';

describe('SendMoneyComponent', () => {
  let component: SendMoneyComponent;
  let fixture: ComponentFixture<SendMoneyComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [SendMoneyComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SendMoneyComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
