import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AplicatiiComponent } from './aplicatii.component';

describe('AplicatiiComponent', () => {
  let component: AplicatiiComponent;
  let fixture: ComponentFixture<AplicatiiComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AplicatiiComponent ]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AplicatiiComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
