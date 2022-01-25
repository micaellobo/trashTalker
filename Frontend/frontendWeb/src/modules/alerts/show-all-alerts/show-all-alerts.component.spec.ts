import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAllAlertsComponent } from './show-all-alerts.component';

describe('ShowAllAlertsComponent', () => {
  let component: ShowAllAlertsComponent;
  let fixture: ComponentFixture<ShowAllAlertsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAllAlertsComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAllAlertsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
