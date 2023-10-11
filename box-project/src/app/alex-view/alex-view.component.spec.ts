import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AlexViewComponent } from './alex-view.component';

describe('AlexViewComponent', () => {
  let component: AlexViewComponent;
  let fixture: ComponentFixture<AlexViewComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [AlexViewComponent]
    });
    fixture = TestBed.createComponent(AlexViewComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
