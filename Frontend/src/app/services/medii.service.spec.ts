import { TestBed } from '@angular/core/testing';

import { MediiService } from './medii.service';

describe('MediiService', () => {
  let service: MediiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(MediiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
