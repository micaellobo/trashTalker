import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogAutomaticRouteComponent } from './dialog-automatic-route.component';

describe('DialogAutomaticRouteComponent', () => {
  let component: DialogAutomaticRouteComponent;
  let fixture: ComponentFixture<DialogAutomaticRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogAutomaticRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogAutomaticRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
