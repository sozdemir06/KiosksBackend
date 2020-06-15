import { Component, OnInit } from '@angular/core';
import { AuthStore } from '../auth.store';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { IUserForLogin } from 'src/app/shared/models/IUserForLogin';
import { NotifyService } from 'src/app/core/services/notify-service';


@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  loginForm:FormGroup;

  constructor(
    public authStore:AuthStore,
    private fb:FormBuilder,
    private router:Router,
    private notifyService:NotifyService
  ) { }

  ngOnInit(): void {
    this.checkLoginForm();
  }

  get f(){return this.loginForm.controls};

  checkLoginForm(){
    this.loginForm=this.fb.group({
      email:["sukru.ozdemir@hmb.gov.tr",[Validators.required,Validators.email]],
      password:["466357",[Validators.required,Validators.minLength(4),Validators.maxLength(8)]]
    })
  }

  login(){
    if(this.loginForm.valid){
      const model:IUserForLogin={
        ...this.loginForm.value
      }
      this.authStore.login(model).subscribe(result=>{
        
      },err=>{
        this.notifyService.notify("error",err);
      },()=>{
        this.router.navigateByUrl("/");
        this.notifyService.notify("success","Giriş Başarılı");
      })
    }
  }

}
