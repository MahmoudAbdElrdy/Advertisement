import {Component,OnInit} from '@angular/core';
import {FormBuilder, FormControl,FormGroup,Validators} from '@angular/forms';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Router } from '@angular/router';
import { AuthServiceProxy } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  
  signinForm = this.formBuilder.group({
    username: ['', [Validators.required]],
    password: ['', [Validators.required]]
  });
  constructor(private router: Router,private formBuilder: FormBuilder, private IdentityServ:AuthServiceProxy,private _snackBar: MatSnackBar) {}

  ngOnInit() {}
  onLogin() {
    localStorage.setItem('isLoggedin', 'true');
    this.router.navigate(['/dashboard']);
  }
  signIn(){
    ;
    var Object=this.signinForm.value;
   this.IdentityServ.login(Object).subscribe((data:any)=>{
    let obj =data.data;
     if(obj.token != null)
      {
        localStorage.setItem('isLoggedin', 'true');
            localStorage.setItem('userToken',obj.token );
            this._snackBar.open("تم تسجيل الدخول بنجاح"," تسجيل الدخول" ,{
              duration: 2220,
              
            });
            this.router.navigate(['/dashboard']);
            /** spinner starts on init */
      
            /** spinner ends after 5 seconds */
          
      }
      else{
      //  this.toastr.error( 'Error occured during login!');
      this._snackBar.open("اضافة","Error occured during login!" ,{
        duration: 2220,
        
      });
      }
    },
   err=>{console.log(err)}
   )
  }
}
