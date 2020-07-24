import { Component, OnInit, Input } from '@angular/core';
import { HelperService } from 'src/app/core/services/helper-service';

@Component({
  selector: 'app-announce-status',
  templateUrl: './announce-status.component.html',
  styleUrls: ['./announce-status.component.scss'],
})
export class AnnounceStatusComponent implements OnInit {
  @Input() isNew: boolean;
  @Input() isPublish: boolean;
  @Input() reject: boolean;
  @Input() startDate: Date;
  @Input() finishDate: Date;

  constructor(private helperService: HelperService) {}

  ngOnInit(): void {
    this.checkIsNowPublish();
  }

  checkIsNowPublish(): boolean {
    let checkMatch: boolean = false;
    const startDate = new Date(this.startDate);
    const finishDate = new Date(this.finishDate);
    const now = new Date();

    if (
      this.isPublish &&
      !this.isNew &&
      !this.reject &&
      now > startDate &&
      finishDate > now
    ) {
      checkMatch = true;
    }
    return checkMatch;
  }

  checkIsNew(): boolean {
    let checkMatch: boolean = false;
    if (this.isNew && !this.isPublish && !this.reject) {
      checkMatch = true;
    }
    return checkMatch;
  }

  checkUnpublish() {
    let checkMatch: boolean = false;
    if (!this.isPublish && !this.isNew && !this.reject) {
      checkMatch = true;
    }

    return checkMatch;
  }

  checkIsPublish() {
    let checkMatch: boolean = false;
    if (this.isPublish && !this.isNew && !this.reject) {
      checkMatch = true;
    }
    return checkMatch;
  }

  checkIsReject() {
    let checkMatch: boolean = false;
    if (this.reject && !this.isNew && !this.isPublish) {
      checkMatch = true;
    }
    return checkMatch;
  }

  checkIsNextDatePublish(): boolean {
    let checkMatch: boolean = false;
    const startDate = new Date(this.startDate);
    const now = new Date();
    if (this.checkIsPublish() && startDate > now) {
      checkMatch = true;
    }

    return checkMatch;
  }

  checkExpireDate() {
    let checkMatch: boolean = false;
    if (this.checkIsPublish()) {
      const dateNow = new Date();
      const finishDate = new Date(this.finishDate);

      if (this.finishDate < dateNow) {
        checkMatch = true;
      }
    }
    return checkMatch;
  }
}
