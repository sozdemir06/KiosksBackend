import { NgModule } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatBadgeModule } from '@angular/material/badge';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatMenuModule } from '@angular/material/menu';
import { CommonModule } from '@angular/common';
import { MatExpansionModule } from '@angular/material/expansion';
import { MatListModule } from '@angular/material/list';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatTabsModule } from '@angular/material/tabs';
import { MatInputModule } from '@angular/material/input';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import {MatChipsModule} from '@angular/material/chips';
import {MatDialogModule} from '@angular/material/dialog';




@NgModule({
  imports: [
    CommonModule,
    MatButtonModule,
    MatBadgeModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    MatExpansionModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatTabsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatChipsModule,
    MatDialogModule
    
  ],
  exports: [
    MatButtonModule,
    MatBadgeModule,
    MatIconModule,
    MatCardModule,
    MatMenuModule,
    MatExpansionModule,
    MatListModule,
    MatSidenavModule,
    MatToolbarModule,
    MatFormFieldModule,
    MatSelectModule,
    MatTabsModule,
    MatInputModule,
    MatTableModule,
    MatPaginatorModule,
    MatChipsModule,
    MatDialogModule
  ],
})
export class MaterialModule {}
