import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  AfterViewInit,
  ViewChild,
  ElementRef,
  OnDestroy,
} from '@angular/core';
import { IToolbarFilterList } from '../models/toolbar-filter-list';
import { fromEvent } from 'rxjs';
import { map, debounceTime, distinctUntilChanged} from 'rxjs/operators';

@Component({
  selector: 'app-admin-toolbar',
  templateUrl: './admin-toolbar.component.html',
  styleUrls: ['./admin-toolbar.component.scss'],
})
export class AdminToolbarComponent implements OnInit, AfterViewInit, OnDestroy {
  @Input() toolbarTitle:string="Sayfa Başlığı";
  @Input() searchInputPlaceHolder: string = 'Search';
  @Input() filterList: IToolbarFilterList[] = [];
  @Input() filterListType:string;
  @Input() isDisableInput:boolean=false;
  @Input() isDisabledConfirm:boolean=false;
  @Input() isDisabledFilterList:boolean=false;
  @Input() isHiddenConfirm:boolean=true;
  @Input() isDisabledReset:boolean=false;

  @Input() allowedRoles: string[] = [];

  @Output() searchKeyWord = new EventEmitter<string>();
  @Output() filterByWaitingConfirm = new EventEmitter();
  @Output() createNew = new EventEmitter();
  @Output() reset=new EventEmitter();
  @Output() onFilter=new EventEmitter<any>();

  @ViewChild('searchInput', { static: true }) Input: ElementRef;

  unSubscribeSearchInputEvent: any;

  constructor() {}

  ngOnInit(): void {
    
  }

  ngAfterViewInit() {
    this.unSubscribeSearchInputEvent = fromEvent<any>(
      this.Input.nativeElement,
      'keyup'
    )
      .pipe(
        map((event) => event.target.value),
        debounceTime(400),
        distinctUntilChanged()
      )
      .subscribe((result) => {
        this.searchKeyWord.emit(result);
      });
  }

  filterBy(filter: IToolbarFilterList | any) {
     this.onFilter.emit(filter);
  }

  onWaitingConfirm() {
    this.filterByWaitingConfirm.emit('isWaitingConfirm');
  }

  onCreateNew() {
    this.createNew.emit();
  }

  onReset(){
    this.reset.emit();
  }

  ngOnDestroy() {
    this.unSubscribeSearchInputEvent.unsubscribe();
  }
}
