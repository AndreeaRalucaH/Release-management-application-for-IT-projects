import { TestBed } from '@angular/core/testing';

import { AplicatiiService } from './aplicatii.service';

describe('AplicatiiService', () => {
  let service: AplicatiiService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AplicatiiService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
