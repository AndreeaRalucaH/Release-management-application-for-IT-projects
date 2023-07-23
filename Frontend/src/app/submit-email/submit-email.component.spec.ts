import { ComponentFixture, TestBed } from '@angular/core/testing';

import { SubmitEmailComponent } from './submit-email.component';

describe('SubmitEmailComponent', () => {
  let component: SubmitEmailComponent;
  let fixture: ComponentFixture<SubmitEmailComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ SubmitEmailComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(SubmitEmailComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
