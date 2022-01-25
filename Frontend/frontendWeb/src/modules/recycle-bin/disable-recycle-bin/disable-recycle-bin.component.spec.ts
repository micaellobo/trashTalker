import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DisableRecycleBinComponent } from './disable-recycle-bin.component';

describe('DisableRecycleBinComponent', () => {
  let component: DisableRecycleBinComponent;
  let fixture: ComponentFixture<DisableRecycleBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DisableRecycleBinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DisableRecycleBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
