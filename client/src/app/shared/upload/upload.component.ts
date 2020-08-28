import {
  Component,
  OnInit,
  Input,
  Output,
  EventEmitter,
  ViewChild,
  ElementRef,
  OnDestroy,
} from '@angular/core';
import { NotifyService } from 'src/app/core/services/notify-service';
import { HttpClient, HttpEventType } from '@angular/common/http';
import { Subscription } from 'rxjs';



@Component({
  selector: 'app-upload',
  templateUrl: './upload.component.html',
  styleUrls: ['./upload.component.scss'],
})
export class UploadComponent implements OnInit,OnDestroy {
  @Input() apiUrl: string;
  @Input() announceId: number;
  @Input() roleForUpload:string[]=[];
  @Output() uploadResult = new EventEmitter<any>();

  allowedFileTypes: string[] = ['image','video','gif'];
  subscription:Subscription=Subscription.EMPTY;
  videoDuration:number=0;
  selectedFile: any;
  fileType: string;
  url: any;

  progress: number | null;
  @ViewChild('fileInput') Input: ElementRef;

  constructor(
    private notifyService: NotifyService,
    private httpClient: HttpClient,
    private elRef:ElementRef
  ) {}

  ngOnInit(): void {
     
  }


  onChange(){
    const video:HTMLVideoElement=this.elRef?.nativeElement.querySelector('video');
    const duration=Math.ceil(video?.duration);
    this.videoDuration=duration;
   
  }


  onFileSelected(event) {
  
    this.selectedFile = <File>event.target.files[0];
    
    console.log(this.selectedFile.type)
    if (this.selectedFile) {
      var reader = new FileReader();
      reader.readAsDataURL(this.selectedFile);
      if (this.selectedFile.type.indexOf('image') > -1) {
        this.fileType = 'image';
      } else if (this.selectedFile.type.indexOf('video') > -1) {
        this.fileType = 'video';
      } 

      reader.onload = (event) => {
        this.url = (<FileReader>event.target).result;
      
      };
    }
  }

  cancelSelectedFile() {
    this.url = null;
    this.fileType = null;
    this.selectedFile = null;
    this.Input.nativeElement.value = '';
    this.videoDuration=0;
  }

  uploadFile() {
    if (this.selectedFile) {
      const checkFileType = this.allowedFileTypes.some(
        (x) => x == this.fileType
      );
      if (!checkFileType) {
        this.notifyService.notify(
          'error',
          'Video ve Resim dışında dosya kabul edilmemektedir.'
        );
      }

      const formData = new FormData();
      formData.append('file', this.selectedFile, this.selectedFile.name);
      formData.append('announceId', this.announceId.toString());
      formData.append('fileType', this.fileType);
      formData.append('duration', this.videoDuration?.toString());
    
      this.subscription = this.httpClient
        .post<any>(this.apiUrl, formData, {
          reportProgress: true,
          observe: 'events',
        })
        .subscribe(
          (event) => {
            if (event.type === HttpEventType.UploadProgress) {
              this.progress = Math.round((100 * event.loaded) / event.total);
            }
            if (event.type === HttpEventType.Response) {
              this.uploadResult.emit(event?.body);
              this.url = null;
              this.selectedFile = null;
              this.fileType = null;
              this.Input.nativeElement.value = '';
              this.progress = null;
              this.videoDuration=0;
            }
          },
          (error) => {
            this.notifyService.notify('error', error);
            this.url = null;
            this.selectedFile = null;
            this.fileType = null;
            this.Input.nativeElement.value = '';
            this.progress = null;
            this.videoDuration=0;
          },
        );
    }
  }

  ngOnDestroy(){
    this.subscription.unsubscribe();
  }
}
