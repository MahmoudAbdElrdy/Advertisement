import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddCityCommand, CitiesServiceProxy, ContactUsServiceProxy, CountriesServiceProxy, CountryDto, EditCityCommand, EditContactUsCommand, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-contact',
  templateUrl: './add-edit-contact.component.html',
  styleUrls: ['./add-edit-contact.component.scss']
})
export class AddEditContactComponent implements OnInit {
  ContactForm: FormGroup;
  AddCityCommand=new AddCityCommand;
  dataModel:EditContactUsCommand;
  submitted = false;
  id:number;
  constructor( private Service : ContactUsServiceProxy, @Inject(MAT_DIALOG_DATA) public data:EditContactUsCommand,
    public dialogRef: MatDialogRef<AddEditContactComponent>,private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel =data;
  }

  ngOnInit() {
    this.initForm();
    ;
    if(this.dataModel!==null){
      this.ContactForm = this.fb.group({
        id: [this.dataModel.id],
        name: [this.dataModel.name],
        email: [this.dataModel.email],
        title: [this.dataModel.title],
        content: [this.dataModel.content],
        responesAdmin:[this.dataModel.responesAdmin]
      }
      );
    }
  
  }

  private initForm(): void {
    this.ContactForm = this.fb.group({
      id: [''],
      name: [''],
      email: ['' ],
      title: [''],
      content: [''],
      responesAdmin:['']
    })
  }

  get fc() {
    return this.ContactForm.controls;
  }
  get fcname() {
    return this.ContactForm.get("name").value;
  }

  doSubmit() {
    ;
    this.submitted = true;
    console.log(this.ContactForm);
    if (this.ContactForm.invalid) {
      return;
    }
   debugger
  this.dataModel=this.ContactForm.value;
  this.Service.editContactUs(this.dataModel.id,this.dataModel).subscribe( res=>{
    if(res.items!==null)
    {
      this._snackBar.open("تم الرد بنجاح","تعديل" ,{
        duration: 2220,
        
      });
     
    }
    else
    {
      this._snackBar.open("حدث خطأ عند الرد","تعديل" ,{
        duration: 2220,
        
      });
    }
    this.dialogRef.close();
  });}
}


