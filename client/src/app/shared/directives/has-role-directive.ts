import {
  Directive,
  Input,
  OnInit,
  ViewContainerRef,
  TemplateRef,
} from '@angular/core';
import { AuthStore } from 'src/app/auth/auth.store';

@Directive({ selector: '[appHasRole]' })
export class HasRoleDirective implements OnInit {
  @Input() appHasRole: string[];
  isVisible: boolean = false;

  constructor(
    private authStore: AuthStore,
    private viewContainerRef: ViewContainerRef,
    private templateRef: TemplateRef<any>
  ) {}

  ngOnInit() {
    if (!this.authStore.getUserRoles()) {
      this.viewContainerRef.clear();
    }

    const isMatch = this.authStore.isMatchRoles(this.appHasRole);
    if (isMatch) {
      if (!this.isVisible) {
        this.isVisible = true;
        this.viewContainerRef.createEmbeddedView(this.templateRef);
      } else {
        this.isVisible = false;
        this.viewContainerRef.clear();
      }
    }
  }
}
