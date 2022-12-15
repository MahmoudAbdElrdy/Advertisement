import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CountriesServiceProxy, CreateCountryCommand, EditCountryCommand, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-country',
  templateUrl: './add-edit-country.component.html',
  styleUrls: ['./add-edit-country.component.scss']
})
export class AddEditCountryComponent implements OnInit {
  countryForm: FormGroup;
  CreateCountryCommand=new CreateCountryCommand;
  dataModel:EditCountryCommand;
  submitted = false;
  id:number;
  constructor( private Service : CountriesServiceProxy, @Inject(MAT_DIALOG_DATA) public data:EditCountryCommand,
    public dialogRef: MatDialogRef<AddEditCountryComponent>,private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel =data;
  }

  ngOnInit() {
    this.initForm();
    ;
    if(this.dataModel!==null){
      this.countryForm = this.fb.group({
        nameAr: [this.dataModel?.name["ar"]],
        nameEn: [this.dataModel?.name["en"]],
        id:[this.dataModel.id]
      });
    }
  }

  private initForm(): void {
    this.countryForm = this.fb.group({
      nameAr: ['', [Validators.required]],
      nameEn: ['', [Validators.required]],
      id:[]
    });
  }

  get fc() {
    return this.countryForm.controls;
  }

  doSubmit() {
    ;
    this.submitted = true;
    console.log(this.countryForm);
    if (this.countryForm.invalid) {
      return;
    }
    let name={
      ar:this.countryForm.get('nameAr').value,
      en:this.countryForm.get('nameEn').value,
    }
  
if(this.dataModel===null){
  this.CreateCountryCommand.name=name;
  this.Service.addCountry(this.CreateCountryCommand).subscribe( res=>{
    if(res.items!==null)
    {
      this._snackBar.open("تم الاضافة بنجاح","اضافة" ,{
        duration: 2220,
        
      });
     
    }
    else
    {
      this._snackBar.open("حدث خطأ عند الاضافة","الاضافة" ,{
        duration: 2220,
        
      });
    }
    this.dialogRef.close();
  })

  
}
else{
  this.dataModel.name=name;
  this.Service.editCountry(this.dataModel.id,this.dataModel).subscribe( res=>{
    if(res.items!==null)
    {
      this._snackBar.open("تم التعديل بنجاح","تعديل" ,{
        duration: 2220,
        
      });
     
    }
    else
    {
      this._snackBar.open("حدث خطأ عند التعديل","تعديل" ,{
        duration: 2220,
        
      });
    }
    this.dialogRef.close();
  });}
}
}
