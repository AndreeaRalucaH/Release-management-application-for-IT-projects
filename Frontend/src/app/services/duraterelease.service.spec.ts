import { TestBed } from '@angular/core/testing';

import { DuratereleaseService } from './duraterelease.service';

describe('DuratereleaseService', () => {
  let service: DuratereleaseService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(DuratereleaseService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
