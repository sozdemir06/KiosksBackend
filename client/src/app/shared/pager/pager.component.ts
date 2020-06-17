import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';

@Component({
  selector: 'app-pager',
  templateUrl: './pager.component.html',
  styleUrls: ['./pager.component.scss']
})
export class PagerComponent implements OnInit {

  @Input() totalCount:number=0;
  @Input() pageSize:number=0;
  @Input() pageSizeOptions:number[]=[5,10,15,20,25,100];

  @Output() pageChange=new EventEmitter<PageEvent>();

  constructor() { }

  ngOnInit(): void {
  }

  onPageChange(event:PageEvent){
    this.pageChange.emit(event);
  }
}
