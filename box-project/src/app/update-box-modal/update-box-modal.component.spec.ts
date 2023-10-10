import { ComponentFixture, TestBed } from '@angular/core/testing';

import { UpdateBoxModalComponent } from './update-box-modal.component';

describe('UpdateBoxModalComponent', () => {
  let component: UpdateBoxModalComponent;
  let fixture: ComponentFixture<UpdateBoxModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [UpdateBoxModalComponent]
    });
    fixture = TestBed.createComponent(UpdateBoxModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
