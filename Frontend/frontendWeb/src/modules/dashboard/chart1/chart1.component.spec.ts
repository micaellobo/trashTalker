import { ComponentFixture, TestBed } from '@angular/core/testing';

import { Chart1Component } from './chart1.component';

describe('Chart1Component', () => {
  let component: Chart1Component;
  let fixture: ComponentFixture<Chart1Component>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ Chart1Component ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(Chart1Component);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
