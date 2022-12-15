import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { AddCityCommand, CitiesServiceProxy, CountriesServiceProxy, CountryDto, EditCityCommand, RegionManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-city',
  templateUrl: './add-edit-city.component.html',
  styleUrls: ['./add-edit-city.component.scss'],
})
export class AddEditCityComponent implements OnInit {
  cityForm: FormGroup;
  AddCityCommand=new AddCityCommand;
  dataModel:EditCityCommand;
  CountryDDL: CountryDto[] = [];
  submitted = false;
  id:number;
  constructor( private Service : CitiesServiceProxy,private CountriesService : CountriesServiceProxy, @Inject(MAT_DIALOG_DATA) public data:EditCityCommand,
    public dialogRef: MatDialogRef<AddEditCityComponent>,private _snackBar: MatSnackBar,
    private RegionManagementServiceProxy:RegionManagementServiceProxy,
    private fb: FormBuilder
  ) {
    this.dataModel =data;
  }

  ngOnInit() {
    this.initForm();
    ;
    if(this.dataModel!==null){
      this.cityForm = this.fb.group({
        name:this.fb.group(({
          ar: [this.dataModel?.name["ar"]],
          en: [this.dataModel?.name["en"]],
        })),
       
        id:[this.dataModel.id],
        countryId:[this.dataModel.countryId]
      });
    }
    this.LoadCountries();
  }
  LoadCountries(){
 
    //  this.CountriesService.getDdl().subscribe(res=>{
    //   ;
    //   this.CountryDDL=res.items;
    //   console.log("CountryDDL:",this.CountryDDL)
    // })

    return this.RegionManagementServiceProxy.countries(1,100,"","","").subscribe(res=>{
      ;
      this.CountryDDL=res.items;
      console.log("CountryDDL:",this.CountryDDL)
    })

  }
  private initForm(): void {
    this.cityForm = this.fb.group({
      name:this.fb.group(({
        ar: ['', [Validators.required]],
        en: ['', [Validators.required]],
      })),
      id:[],
      countryId:[],
    });
  }

  get fc() {
    return this.cityForm.controls;
  }
  get fcname() {
    return this.cityForm.get("name").value;
  }

  doSubmit() {
    ;
    this.submitted = true;
    console.log(this.cityForm);
    if (this.cityForm.invalid) {
      return;
    }
   
  
if(this.dataModel===null){
  this.AddCityCommand=this.cityForm.value;
  this.Service.addCity(this.AddCityCommand).subscribe( res=>{
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
  this.dataModel=this.cityForm.value;
  this.Service.editCity(this.dataModel.id,this.dataModel).subscribe( res=>{
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

