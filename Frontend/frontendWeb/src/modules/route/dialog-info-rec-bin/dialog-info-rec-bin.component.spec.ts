import { ComponentFixture, TestBed } from '@angular/core/testing';

import { DialogInfoRecBinComponent } from './dialog-info-rec-bin.component';

describe('DialogInfoRecBinComponent', () => {
  let component: DialogInfoRecBinComponent;
  let fixture: ComponentFixture<DialogInfoRecBinComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [ DialogInfoRecBinComponent ]
    })
    .compileComponents();
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(DialogInfoRecBinComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
