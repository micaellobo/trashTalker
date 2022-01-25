import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogOptionRouteComponent } from './dialog-option-route.component';

describe('DialogOptionRouteComponent', () => {
  let component: DialogOptionRouteComponent;
  let fixture: ComponentFixture<DialogOptionRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogOptionRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogOptionRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
