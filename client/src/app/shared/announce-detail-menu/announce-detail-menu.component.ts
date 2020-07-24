import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { HelperService } from 'src/app/core/services/helper-service';

@Component({
  selector: 'app-announce-detail-menu',
  templateUrl: './announce-detail-menu.component.html',
  styleUrls: ['./announce-detail-menu.component.scss']
})
export class AnnounceDetailMenuComponent implements OnInit {
@Input() isNew:boolean;
@Input() isPublish:boolean;
@Input() isReject:boolean;
@Input() startDate:Date;
@Input() finishDate:Date;

@Input() roleForUpdate:string[]=[];
@Input() roleForPublish:string[]=[];

@Output() onPublish=new EventEmitter();
@Output() unPublish=new EventEmitter();
@Output() onDelete=new EventEmitter();
@Output() onReject=new EventEmitter();
@Output() onUpdate=new EventEmitter();

  constructor(
    private helperService:HelperService
  ) { }

  ngOnInit(): void {
  }

  onSelectUpdate(){
    this.onUpdate.emit();
  }

  onSelectDelete(){
    this.onDelete.emit();
  }

  onSelectUnpublish(){
      this.unPublish.emit();
  }

  onSelectPublish(){
    this.onPublish.emit();
  }
  onSelectReject(){
    this.onReject.emit();
  }

  checkUnpublish(){
    let checkMatch:boolean=false;
    if(!this.isPublish && !this.isNew && !this.isReject){
      checkMatch=true;
    }

    return checkMatch;
  }

  checkIsNew():boolean{
    let checkMatch:boolean=false;
    if(this.isNew && !this.isPublish && !this.isReject){
      checkMatch=true;
    }
    return checkMatch;
  }

  checkIsPublish(){
    let checkMatch:boolean=false;
    if(this.isPublish && !this.isNew && !this.isReject){
      checkMatch=true;
    }
    return checkMatch;
  }

  checkIsReject(){
    let checkMatch:boolean=false;
    if(this.isReject && !this.isNew && !this.isPublish){
      checkMatch=true;
    }
    return checkMatch;
  }

  checkExpireDate(){
    let checkMatch:boolean=false;
    if(this.checkIsPublish()){
      const dateNow:Date=this.helperService.dateToLocaleFormat(new Date());
      if(this.finishDate<dateNow){
        checkMatch=true;
      }
    }
    return checkMatch;
  }

}
