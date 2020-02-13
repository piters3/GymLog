/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DaylogsComponent } from './daylogs.component';

describe('LogsComponent', () => {
  let component: DaylogsComponent;
  let fixture: ComponentFixture<DaylogsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaylogsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaylogsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
