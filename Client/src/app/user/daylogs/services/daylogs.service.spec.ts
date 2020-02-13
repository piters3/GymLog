/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { DaylogsService } from './daylogs.service';

describe('Service: LogsService', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [DaylogsService]
    });
  });

  it('should ...', inject([DaylogsService], (service: DaylogsService) => {
    expect(service).toBeTruthy();
  }));
});
