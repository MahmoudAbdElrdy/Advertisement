import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CountriesServiceProxy, CreateServiceCommand, EditServiceCommand, RegionManagementServiceProxy, ServiceProxy, ServicesServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-servicetype',
  templateUrl: './add-edit-servicetype.component.html',
  styleUrls: ['./add-edit-servicetype.component.scss']
})
export class AddEditservicetypeComponent implements OnInit {
  servicetypeForm: FormGroup;
  CreateservicetypeCommand=new CreateServiceCommand;
  dataModel:EditServiceCommand;
  submitted = false;
  id:number;
  constructor( private Service : ServicesServiceProxy, @Inject(MAT_DIALOG_DATA) public data:EditServiceCommand,
    public dialogRef: MatDialogRef<AddEditservicetypeComponent>,private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel=new EditServiceCommand();
    this.dataModel =data;
  }

  ngOnInit() {
    this.initForm();
    ;
    if(this.dataModel!==null){
      this.servicetypeForm = this.fb.group({
        nameAr: [this.dataModel?.name["ar"]],
        nameEn: [this.dataModel?.name["en"]],
        descriptionAr: [this.dataModel?.description!=undefined ?this.dataModel?.description["ar"]:""],
        descriptionEn:[this.dataModel?.description!=undefined ?this.dataModel?.description["en"]:""],
        id:[this.dataModel.id]
      });
    }
  }

  private initForm(): void {
    this.servicetypeForm = this.fb.group({
      nameAr: ['', [Validators.required]],
      nameEn: ['', [Validators.required]],
      descriptionAr: [''],
      descriptionEn: ['',],
      id:[]
    });
  }

  get fc() {
    return this.servicetypeForm.controls;
  }

  doSubmit() {
    ;
    this.submitted = true;
    console.log(this.servicetypeForm);
    if (this.servicetypeForm.invalid) {
      return;
    }
    let name={
      ar:this.servicetypeForm.get('nameAr').value,
      en:this.servicetypeForm.get('nameEn').value,
    }
    let description={
      ar:this.servicetypeForm.get('descriptionAr').value,
      en:this.servicetypeForm.get('descriptionEn').value,
    }
  
if(this.dataModel===null){
  debugger
  this.CreateservicetypeCommand.name=name;
  this.CreateservicetypeCommand.description=description;
  this.Service.addService(this.CreateservicetypeCommand).subscribe( res=>{
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
  this.dataModel.description=description;
  this.Service.editService(this.dataModel.id,this.dataModel).subscribe( res=>{
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
