import { Component, Inject, OnInit, ViewChild } from '@angular/core';
import { FormArray, FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AddUserCommand, AuthServiceProxy, EditUserCommand,UsersServiceProxy, RoleDto, RoleDtoPageList, RolesServiceProxy, UserDto, UserManagementServiceProxy } from 'src/shared/service-proxies/service-proxies';
import { ListUsersComponent } from '../list-users/list-users.component';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {
  id:string;
  UserRegisteration:AddUserCommand;
  identityrole:any[];
  dataModel:EditUserCommand;
  show:boolean=true;
  ctrls: FormControl[];
constructor(private _Router:Router,
  private formBuilder: FormBuilder,
  private IdentityServ:AuthServiceProxy,
  private RoleServ:UserManagementServiceProxy,
@Inject(MAT_DIALOG_DATA) public data:EditUserCommand,
    public dialogRef: MatDialogRef<RegisterComponent>,private _snackBar: MatSnackBar,
    private fb: FormBuilder,private UsersServiceProxy:UsersServiceProxy
 ) { 
  this.UserRegisteration=new AddUserCommand();
  this.identityrole=new Array<any>();
  this.dataModel =data;
}
loaded: boolean = false;
signupForm:FormGroup;
ngOnInit(): void {
  this.InitForm();
  this.getAllRoles();
 
}

InitForm(){
  this.ctrls = this.identityrole.map(control => this.formBuilder.control(false));
 this.signupForm = this.formBuilder.group({
  id:[],
    userName: ['', [Validators.required,Validators.minLength(5),Validators.maxLength(20)]],
    email: ['', [Validators.required,Validators.email]],
    password: ['',Validators.required],
    firstName: ['',Validators.required],
    lastName: [''],
    phoneNumber:[''],
    roles:this.formBuilder.array(this.ctrls),
   
});
}
     
  MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          // return if another validator has already found an error on the matchingControl
          return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  }
}
signUp(){
if(this.dataModel===null){
  
  this.Addnew();
}
else{
  this.Update();
}

}
Addnew(){
  debugger
  const selectedRoles= this.signupForm.value.roles
  .map((checked, i) => checked ? this.identityrole[i].name : null)
  .filter(value => value !== null);
  this.signupForm.value.roles=selectedRoles;
  var Object={body:this.signupForm.value}
 
 this.RoleServ.users(Object.body).subscribe( res=>{
  if(res.items!==null)
  {
         
         
          this._snackBar.open("تم الاضافة بنجاح","اضافة" ,{
            duration: 2220,
            
          });
          /** spinner starts on init */
     
    }
    else{
      this._snackBar.open("حدث خطأ عند الاضافة","الاضافة" ,{
        duration: 2220,
        
      });
    }
    this.dialogRef.close();
  },
 err=>{console.log(err)}
 
 )

}
Update(){
  const selectedRoles= this.signupForm.value.roles
  .map((checked, i) => checked ? this.identityrole[i].name : null)
  .filter(value => value !== null);
  this.signupForm.value.roles=selectedRoles;
  var Object={body:this.signupForm.value}
  this.signupForm.removeControl('userName');
  this.signupForm.removeControl('password');
  
 this.UsersServiceProxy.editUser(this.dataModel.id,Object.body).subscribe( res=>{
  if(res.items!==null)
  {
         
         
          this._snackBar.open("تم التعديل بنجاح","التعديل" ,{
            duration: 2220,
            
          });
          /** spinner starts on init */
     
    }
    else{
      this._snackBar.open("حدث خطأ عند التعديل","التعديل" ,{
        duration: 2220,
        
      });
    }
    this.dialogRef.close();
  },
 err=>{console.log(err)}
 )

}
get permissionsArr() {
 
  return this.signupForm.get('roles') as FormArray;
}
getAllRoles(){
  this.RoleServ.roles(1,100,"","","").subscribe(res=>{
 
  this.signupForm.value.roles= this.identityrole=res.items;
    this.ctrls = this.identityrole.map(control => this.formBuilder.control(false));
    if(this.dataModel!==null){
    
      console.log( this.permissionsArr.value)
      this.id=this.dataModel.id;
      this.show=false;
     
      this.signupForm = this.formBuilder.group({
        
        id:[this.dataModel.id],
        userName: ['', ],
        email: [this.dataModel.email||'', [Validators.required,Validators.email]],
        password: [this.dataModel.password||'',Validators.required],
        firstName: [this.dataModel.firstName,Validators.required],
        lastName: [this.dataModel.lastName||''],
        phoneNumber:[this.dataModel.phoneNumber||''],
        roles:this.fb.array(this.ctrls),
       
    });
    if(this.dataModel.roles!=undefined){
      this.identityrole.map((perm, i) => {
      
        if (this.dataModel.roles.indexOf(perm.name) !== -1) {
          this.permissionsArr.at(i).patchValue(true)
        }
      })
    }
    }
    else
    {
     
    
      this.signupForm = this.formBuilder.group({
        id:[],
          userName: ['', [Validators.required,Validators.minLength(5),Validators.maxLength(20)]],
          email: ['', [Validators.required,Validators.email]],
          password: ['',Validators.required],
          firstName: ['',Validators.required],
          lastName: [''],
          phoneNumber:[''],
          roles:this.fb.array(this.ctrls),
         
      });
    }
   
    
    this.loaded = true
  })
}



}