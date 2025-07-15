import { ComponentFixture, TestBed } from '@angular/core/testing';

import { AllRequestComponent } from '../../../../src/app/components/all-request/all-request.component';

describe('AllRequestComponent', () => {
  let component: AllRequestComponent;
  let fixture: ComponentFixture<AllRequestComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AllRequestComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(AllRequestComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
