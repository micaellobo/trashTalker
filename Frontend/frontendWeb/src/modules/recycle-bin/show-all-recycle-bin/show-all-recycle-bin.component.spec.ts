import { ComponentFixture, TestBed } from '@angular/core/testing';

import { ShowAllRecycleBinComponent } from './show-all-recycle-bin.component';

describe('ShowAllRecycleBinComponent', () => {
  let component: ShowAllRecycleBinComponent;
  let fixture: ComponentFixture<ShowAllRecycleBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ ShowAllRecycleBinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(ShowAllRecycleBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
