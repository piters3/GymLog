/* tslint:disable:no-unused-variable */

import { TestBed, async, inject } from '@angular/core/testing';
import { MusclesService } from './muscles.service';

describe('Service: Muscles', () => {
  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [MusclesService]
    });
  });

  it('should ...', inject([MusclesService], (service: MusclesService) => {
    expect(service).toBeTruthy();
  }));
});
