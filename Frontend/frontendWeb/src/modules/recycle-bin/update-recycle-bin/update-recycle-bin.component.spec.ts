import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateRecycleBinComponent } from './update-recycle-bin.component';

describe('UpdateRecycleBinComponent', () => {
  let component: UpdateRecycleBinComponent;
  let fixture: ComponentFixture<UpdateRecycleBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ UpdateRecycleBinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(UpdateRecycleBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
