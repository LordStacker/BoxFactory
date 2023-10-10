import {Component, Input} from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ModalController} from "@ionic/angular";
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-update-box-modal',
  templateUrl: './update-box-modal.component.html',
  styleUrls: ['./update-box-modal.component.css']
})
export class UpdateBoxModalComponent {
  @Input() boxId: number;
  updateBoxForm: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private http: HttpClient,
    private modalController: ModalController
  ) {
    this.updateBoxForm = this.formBuilder.group({
      boxName: ['', Validators.required],
      material: ['', Validators.required],
      width: [null, [Validators.required, Validators.min(1)]],
      height: [null, [Validators.required, Validators.min(1)]],
      depth: [null, [Validators.required, Validators.min(1)]],
    });
  }

  onSubmit() {
    if (this.updateBoxForm.valid) {
      const formData = this.updateBoxForm.value;
      formData.boxId = this.boxId;

      this.http.put<any>(`http://localhost:5000/box/${this.boxId}`, formData).subscribe(
        (response) => {
          console.log('Box updated successfully', response);

          // Send the updated data back to the parent component
          this.modalController.dismiss({ updatedBox: response });
        },
        (error) => {
          console.error('Error updating box', error);
        }
      );
    }
  }


  closeModal() {
    this.modalController.dismiss();
  }
}

