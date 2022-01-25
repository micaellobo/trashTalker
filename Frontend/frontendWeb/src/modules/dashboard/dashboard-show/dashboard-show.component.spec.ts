import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DashboardShowComponent } from './dashboard-show.component';

describe('DashboardShowComponent', () => {
  let component: DashboardShowComponent;
  let fixture: ComponentFixture<DashboardShowComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DashboardShowComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DashboardShowComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
