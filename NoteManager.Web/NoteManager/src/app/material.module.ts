import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { MatBadgeModule } from '@angular/material/badge';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatTableModule } from '@angular/material/table';
import { LayoutModule } from '@angular/cdk/layout';
import { MatProgressBarModule } from '@angular/material/progress-bar';
import { MatMenuModule } from '@angular/material/menu';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { MatDividerModule } from '@angular/material/divider';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatRadioModule } from '@angular/material/radio';
import { MatSortModule } from '@angular/material/sort';
import { MatPaginatorIntl, MatPaginatorModule } from '@angular/material/paginator';
import { MatTabsModule } from '@angular/material/tabs';

//import { OverlayModule } from '@angular/cdk/overlay';
//import { CdkTreeModule } from '@angular/cdk/tree';
//import { PortalModule } from '@angular/cdk/portal';
//import { MatAutocompleteModule } from '@angular/material/autocomplete';
//import { MatButtonToggleModule } from '@angular/material/button-toggle';
//import { MatChipsModule } from '@angular/material/chips';
//import { MatRippleModule } from '@angular/material/core';
//import { MatExpansionModule } from '@angular/material/expansion';
//import { MatTreeModule } from '@angular/material/tree';
//import { MatTooltipModule } from '@angular/material/tooltip';


const materialModules = [
  MatBadgeModule,
  MatButtonModule,
  MatCardModule,
  MatInputModule,
  MatFormFieldModule,
  MatProgressSpinnerModule,
  MatToolbarModule,
  MatSidenavModule,
  MatListModule,
  MatIconModule,
  MatSnackBarModule,
  MatGridListModule,
  MatTableModule,
  LayoutModule,
  MatProgressBarModule,
  MatMenuModule,
  MatDialogModule,
  MatDatepickerModule,
  MatSelectModule,
  MatDividerModule,
  MatCheckboxModule,
  MatRadioModule,
  MatSortModule,
  MatPaginatorModule,
  MatTabsModule
];

@NgModule({
  imports: [
    CommonModule,
    ...materialModules
  ],
  exports: [
    ...materialModules,
  ],
  declarations: [
  ],
  providers: [
  ]


})

export class MaterialModule { }
