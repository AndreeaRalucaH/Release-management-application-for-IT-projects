import { TestBed } from '@angular/core/testing';

import { DetaliireleaseService } from './detaliirelease.service';

describe('DetaliireleaseService', () => {
  let service: DetaliireleaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DetaliireleaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
