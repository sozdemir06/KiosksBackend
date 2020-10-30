import { Component, OnDestroy, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Subscription } from 'rxjs';
import { PublicFooterTextStore } from 'src/app/core/services/stores/public-footer-text-store';
import { IPublicFooterText } from 'src/app/shared/models/IPublicFooterText';

@Component({
  selector: 'app-public-footer-text',
  templateUrl: './public-footer-text.component.html',
  styleUrls: ['./public-footer-text.component.scss']
})
export class PublicFooterTextComponent implements OnInit ,OnDestroy{
footerTextForm:FormGroup;
formValue:IPublicFooterText;
subscribe:Subscription=Subscription.EMPTY;

  constructor(
    private fb:FormBuilder,
    public publicFooterTextStore:PublicFooterTextStore
  ) { }

  get f(){
    return this.footerTextForm.controls;
  }
  ngOnInit(): void {
    this.checkForm();
    this.subscribe=this.publicFooterTextStore.footerText$.subscribe(result=>{
      if(result){
        this.footerTextForm.patchValue({
          ...result
        });
      }
    }) 
  }

  checkForm(){
    this.footerTextForm=this.fb.group({
      footerText:["",Validators.required],
      contentPhoneNumber:[]
    })
  }

  onSubmitform(){
    if(this.footerTextForm.valid){
       const model:IPublicFooterText={
         ...this.footerTextForm.value,

       };
       this.publicFooterTextStore.cretaeOrUpdate(model);
    }
  }

  ngOnDestroy(){
    this.subscribe.unsubscribe();
  }

}
