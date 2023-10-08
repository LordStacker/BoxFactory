import { Component } from '@angular/core';
import { ModalController } from '@ionic/angular';
import { HttpClient } from '@angular/common/http';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-create-box-modal',
  templateUrl: './create-box-modal.component.html',
  styleUrls: ['./create-box-modal.component.css'],
})
export class CreateBoxModalComponent {
  createBoxForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient, // Inject the HttpClient here
    private modalController: ModalController
  ) {
    this.createBoxForm = this.formBuilder.group({
      boxName: ['', Validators.required],
      material: ['', Validators.required],
      width: [null, [Validators.required, Validators.min(1)]],
      height: [null, [Validators.required, Validators.min(1)]],
      depth: [null, [Validators.required, Validators.min(1)]],
    });
  }

  onSubmit() {
    if (this.createBoxForm.valid) {
      const formData = this.createBoxForm.value;

      this.http.post<any>('http://localhost:5000/box', formData).subscribe(
        (response) => {
          console.log('Box created successfully', response);
          this.modalController.dismiss({ createdBox: response });

        },
        (error) => {
          console.error('Error creating box', error);
        }
      );
    }
  }

  closeModal() {
    this.modalController.dismiss();
  }
}
