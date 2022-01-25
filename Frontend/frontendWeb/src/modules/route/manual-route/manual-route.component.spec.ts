import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ManualRouteComponent } from './manual-route.component';

describe('ManualRouteComponent', () => {
  let component: ManualRouteComponent;
  let fixture: ComponentFixture<ManualRouteComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ManualRouteComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ManualRouteComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
