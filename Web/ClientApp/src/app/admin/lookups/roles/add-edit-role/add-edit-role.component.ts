import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, FormControl } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { MatDialogRef, MAT_DIALOG_DATA} from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { CountriesServiceProxy, AddRoleCommand, EditCountryCommand, RegionManagementServiceProxy, EditRoleCommand, UserManagementServiceProxy, RolesServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-add-edit-role',
  templateUrl: './add-edit-role.component.html',
  styleUrls: ['./add-edit-role.component.scss']
})
export class AddEditRoleComponent implements OnInit {
  roleForm: FormGroup;
  AddRoleCommand=new AddRoleCommand;
  dataModel:EditRoleCommand;
  submitted = false;
  id:number;
  constructor( private Service : RolesServiceProxy, @Inject(MAT_DIALOG_DATA) public data:EditRoleCommand,
    public dialogRef: MatDialogRef<AddEditRoleComponent>,private _snackBar: MatSnackBar,
    private fb: FormBuilder
  ) {
    this.dataModel =data;
  }

  ngOnInit() {
    this.initForm();
  
  }

  private initForm(): void {
    this.roleForm = this.fb.group({
      name: ['', [Validators.required]],
   
      id:[]
    });
  }

  get fc() {
    return this.roleForm.controls;
  }

  doSubmit() {
   debugger
    this.submitted = true;
    console.log(this.roleForm);
    if (this.roleForm.invalid) {
      return;
    }
    
  


  this.Service.addRole(this.roleForm.value).subscribe( res=>{
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
}

