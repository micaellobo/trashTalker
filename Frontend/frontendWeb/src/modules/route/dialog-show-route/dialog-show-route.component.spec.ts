import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogShowRouteComponent } from './dialog-show-route.component';

describe('DialogShowRouteComponent', () => {
  let component: DialogShowRouteComponent;
  let fixture: ComponentFixture<DialogShowRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogShowRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogShowRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
