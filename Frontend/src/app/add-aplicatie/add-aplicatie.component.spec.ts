import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddAplicatieComponent } from './add-aplicatie.component';

describe('AddAplicatieComponent', () => {
  let component: AddAplicatieComponent;
  let fixture: ComponentFixture<AddAplicatieComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddAplicatieComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AddAplicatieComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
