import {
  Component,
  OnInit,
  Input,
  ChangeDetectionStrategy,
} from '@angular/core';
import { IMageGallery } from '../models/IMageGallery';
import {
  NgxGalleryImage,
  NgxGalleryOptions,
  NgxGalleryAnimation,
} from 'ngx-gallery-9';


@Component({
  selector: 'app-image-slider',
  templateUrl: './image-slider.component.html',
  styleUrls: ['./image-slider.component.scss'],
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class ImageSliderComponent implements OnInit{
  @Input() images: IMageGallery[];
  @Input() thumbnails:boolean=true;
  @Input() preview:boolean=false;
  @Input() imageAutoPlay:boolean=false;

  defaultImage: NgxGalleryImage[];
  galleryOptions: NgxGalleryOptions[];
  galleryImages: NgxGalleryImage[];
  
  constructor(
  
  ) {}

  ngOnInit(): void {
    this.galleryOptions = [
      {
        width: '100%',
        height: '300px',
        imagePercent: 100,
        thumbnailsColumns: 4,
        imageAnimation: NgxGalleryAnimation.Slide,
        preview: this.preview,
        thumbnails:this.thumbnails,
        imageAutoPlay:this.imageAutoPlay

      },
    ];

      this.galleryImages = this.getImages();
      
    

    this.defaultImage = [
      {
        small: 'assets/no_image.jpg',
        medium: 'assets/no_image.jpg',
        big: 'assets/no_image.jpg',
      },
    ];
  }

 
  getImages() {
    const imageUrls = [];
    for (let i = 0; i < this.images?.length; i++) {
      imageUrls.push({
        small: this.images[i].fullPath,
        medium: this.images[i].fullPath,
        big: this.images[i].fullPath,
      });
    }
    return imageUrls;
  }
}
