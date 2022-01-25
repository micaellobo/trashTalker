import { TestBed } from '@angular/core/testing';

import { RecyclerBinService } from './recycler-bin.service';

describe('RecyclerBinService', () => {
  let service: RecyclerBinService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(RecyclerBinService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
