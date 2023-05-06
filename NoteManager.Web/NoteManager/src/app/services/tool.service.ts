import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';

declare var require: any

@Injectable({
  providedIn: 'root'
})
export class ToolService {

  constructor(private snackBar: MatSnackBar,
              public http: HttpClient) { }

  newHash!: string;

  openSnackBarError(message: string, action: string): void {
    this.snackBar.open(message, action, {
      duration: 5000,
      verticalPosition: 'top',
      horizontalPosition: 'center',
      panelClass: ['mat-toolbar', 'warning']
    });
  }

  openSnackBarConfirm(message: string, action: string): void {
    this.snackBar.open(message, action, {
      duration: 5000,
      verticalPosition: 'top',
      horizontalPosition: 'center',
      panelClass: ['mat-toolbar', 'confirm']
    });
  }

  /* getNewGuid(): Observable<string> {
    return this.http.get<ResponseModel<string>>('/api/data/getNewGuid').pipe(
      map((response: ResponseModel<string>) => { return response.data})
    )
  } */

  hashMd5(password: string): string {
    var md5 = require('md5');
    this.newHash = md5(password)
    return this.newHash
  }

  


}
