import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AddRecycleBinComponent } from './add-recycle-bin.component';

describe('AddRecycleBinComponent', () => {
  let component: AddRecycleBinComponent;
  let fixture: ComponentFixture<AddRecycleBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ AddRecycleBinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(AddRecycleBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
