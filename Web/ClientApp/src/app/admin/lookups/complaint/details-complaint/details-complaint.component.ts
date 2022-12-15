import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AdComplaintServiceProxy, CreateAdComplaintCommand, EditAdComplaintCommand, AdComplaintDto, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-details-adComplaint',
  templateUrl: './details-complaint.component.html',
  styleUrls: ['./details-complaint.component.scss']
})
export class detailsComplaintComponent implements OnInit {
  adComplaintForm: FormGroup;
  CreateadComplaintCommand = new CreateAdComplaintCommand;
  dataModel: AdComplaintDto;
  submitted = false;
  id: number;
  constructor(private Service: AdComplaintServiceProxy, @Inject(MAT_DIALOG_DATA) public data: AdComplaintDto,
    public dialogRef: MatDialogRef<detailsComplaintComponent>, private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel = data;
  }

  ngOnInit() {
    this.initForm();
    if (this.dataModel !== null) {
      this.adComplaintForm = this.fb.group({
        complaintReason: [this.dataModel?.complaintReason],
        complaintReasonReplay: [this.dataModel?.complaintReasonReplay],
        isComplaintSeen: [this.dataModel?.isComplaintSeen], 
        clientName: [this.dataModel?.clientName],
        adOwnerName: [this.dataModel?.adOwnerName],      
        id: [this.dataModel?.id]        
      });
    }
  }

  private initForm(): void {
    this.adComplaintForm = this.fb.group({
      complaintReason: [''],
      complaintReasonReplay: ['', [Validators.required]],
      isComplaintSeen: [''],
      clientName: [''],
      adOwnerName: [''],
      id: []
    });
  }

  get fc() {
    return this.adComplaintForm.controls;
  }

  doSubmit() {
    this.submitted = true;
    console.log(this.adComplaintForm);
    if (this.adComplaintForm.invalid) {
      return;
    }
    debugger
    var edit: EditAdComplaintCommand = new EditAdComplaintCommand();
    edit.complaintReason = this.dataModel.complaintReason;
    edit.complaintReasonReplay = this.adComplaintForm.controls['complaintReasonReplay'].value;
    edit.id = this.dataModel.id;
    edit.isComplaintSeen = this.dataModel.isComplaintSeen;

    
      this.Service.editAdComplaint(edit).subscribe(res => {
        if (res !== null) {
          this._snackBar.open("تم التعديل بنجاح", "تعديل", {
            duration: 2220,
          });
        }
        else {
          this._snackBar.open("حدث خطأ عند التعديل", "تعديل", {
            duration: 2220,
          });
        }
        this.dialogRef.close();
      });
    
  }
}
