import { TestBed } from '@angular/core/testing';

import { PickingService } from './picking.service';

describe('PickingService', () => {
  let service: PickingService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(PickingService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
