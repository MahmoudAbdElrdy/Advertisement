import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { GeneralConfigurationServiceProxy, CreateGeneralConfigurationCommand, EditGeneralConfigurationCommand, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-GeneralConfigration',
  templateUrl: './add-edit-general-configration.component.html',
  styleUrls: ['./add-edit-general-configration.component.scss']
})
export class AddEditGeneralConfigrationComponent implements OnInit {
  settingForm: FormGroup;
  CreateGeneralConfigrationCommand = new CreateGeneralConfigurationCommand;
  dataModel: EditGeneralConfigurationCommand;
  submitted = false;
  id: number;
  action: string="Add";
  constructor(private Service: GeneralConfigurationServiceProxy, @Inject(MAT_DIALOG_DATA) public data: EditGeneralConfigurationCommand,
    public dialogRef: MatDialogRef<AddEditGeneralConfigrationComponent>, private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel = data;
  }

  ngOnInit() {
    this.initForm();
    debugger
    if (this.dataModel !== null) {
      this.settingForm = this.fb.group({
        value: [this.dataModel?.value],
        valueType: [this.dataModel?.valueType],
        name: [this.dataModel?.name],
        id: [this.dataModel.id]
      });
      this.action="Edit";
    }
  }

  private initForm(): void {
    this.settingForm = this.fb.group({
      value: ['', [Validators.required]],
      valueType: ['', [Validators.required]],
      name: ['', [Validators.required]],
      id: []
    });
  }

  get fc() {
    return this.settingForm.controls;
  }

  doSubmit() {
    this.submitted = true;
    console.log(this.settingForm);
    if (this.settingForm.invalid) {
      return;
    }

    if (this.dataModel === null) {
      this.CreateGeneralConfigrationCommand = this.settingForm.value;
      this.Service.addGeneralConfiguration(this.CreateGeneralConfigrationCommand).subscribe(res => {
        if (res !== null) {
          this._snackBar.open("تم الاضافة بنجاح", "اضافة", {
            duration: 2220,

          });

        }
        else {
          this._snackBar.open("حدث خطأ عند الاضافة", "الاضافة", {
            duration: 2220,

          });
        }
        this.dialogRef.close();
      })


    }
    else {
      this.dataModel.name = this.settingForm.value;
      this.Service.editGeneralConfiguration(this.dataModel).subscribe(res => {
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
  hasError = (controlName: string, errorName: string) => {
		return this.settingForm.controls[controlName].hasError(errorName);
	};
}
