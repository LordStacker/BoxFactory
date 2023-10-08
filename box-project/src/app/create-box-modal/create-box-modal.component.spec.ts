import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CreateBoxModalComponent } from './create-box-modal.component';

describe('CreateBoxModalComponent', () => {
  let component: CreateBoxModalComponent;
  let fixture: ComponentFixture<CreateBoxModalComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [CreateBoxModalComponent]
    });
    fixture = TestBed.createComponent(CreateBoxModalComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
