import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ResolveAlertComponent } from './resolve-alert.component';

describe('ResolveAlertComponent', () => {
  let component: ResolveAlertComponent;
  let fixture: ComponentFixture<ResolveAlertComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ResolveAlertComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ResolveAlertComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
