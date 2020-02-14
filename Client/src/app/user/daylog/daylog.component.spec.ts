/* tslint:disable:no-unused-variable */
import { async, ComponentFixture, TestBed } from '@angular/core/testing';
import { By } from '@angular/platform-browser';
import { DebugElement } from '@angular/core';

import { DaylogComponent } from './daylog.component';

describe('DaylogComponent', () => {
  let component: DaylogComponent;
  let fixture: ComponentFixture<DaylogComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ DaylogComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(DaylogComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
