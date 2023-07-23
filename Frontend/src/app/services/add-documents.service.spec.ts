import { TestBed } from '@angular/core/testing';

import { AddDocumentsService } from './add-documents.service';

describe('AddDocumentsService', () => {
  let service: AddDocumentsService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(AddDocumentsService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
