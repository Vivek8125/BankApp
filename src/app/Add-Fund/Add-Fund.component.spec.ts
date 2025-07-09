import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddFundComponent } from './Add-Fund.component';

describe('AddFundComponent', () => {
  let component: AddFundComponent;
  let fixture: ComponentFixture<AddFundComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AddFundComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddFundComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
