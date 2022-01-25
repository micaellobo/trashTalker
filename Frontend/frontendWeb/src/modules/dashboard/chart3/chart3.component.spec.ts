import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chart3Component } from './chart3.component';

describe('Chart3Component', () => {
  let component: Chart3Component;
  let fixture: ComponentFixture<Chart3Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Chart3Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Chart3Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
