import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { OrderComplaintServiceProxy, CreateOrderComplaintCommand, EditOrderComplaintCommand, OrderComplaintDto, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-details-orderComplaints',
  templateUrl: './details-order-complaint.component.html',
  styleUrls: ['./details-order-complaint.component.scss']
})
export class detailsOrderComplaintComponent implements OnInit {
  OrderComplaintForm: FormGroup;
  CreateOrderComplaintCommand = new CreateOrderComplaintCommand;
  dataModel: OrderComplaintDto;
  submitted = false;
  id: number;
  constructor(private Service: OrderComplaintServiceProxy, @Inject(MAT_DIALOG_DATA) public data: OrderComplaintDto,
    public dialogRef: MatDialogRef<detailsOrderComplaintComponent>, private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel = data;
  }

  ngOnInit() {
    this.initForm();
    if (this.dataModel !== null) {
      this.OrderComplaintForm = this.fb.group({
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
    this.OrderComplaintForm = this.fb.group({
      complaintReason: [''],
      complaintReasonReplay: ['', [Validators.required]],
      isComplaintSeen: [''],
      clientName: [''],
      adOwnerName: [''],
      id: []
    });
  }

  get fc() {
    return this.OrderComplaintForm.controls;
  }

  doSubmit() {
    this.submitted = true;
    console.log(this.OrderComplaintForm);
    if (this.OrderComplaintForm.invalid) {
      return;
    }
    debugger
    var edit: EditOrderComplaintCommand = new EditOrderComplaintCommand();
    edit.complaintReason = this.dataModel.complaintReason;
    edit.complaintReasonReplay = this.OrderComplaintForm.controls['complaintReasonReplay'].value;
    edit.id = this.dataModel.id;
    edit.isComplaintSeen = this.dataModel.isComplaintSeen;
    this.Service.editOrderComplaint(edit).subscribe(res => {
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
  disableReplay() {
    return this.dataModel.complaintReasonReplay!=undefined
  }
}
  