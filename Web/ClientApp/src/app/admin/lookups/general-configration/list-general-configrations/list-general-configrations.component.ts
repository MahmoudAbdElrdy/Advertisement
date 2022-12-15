import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { merge, of as observableOf } from 'rxjs';
import { catchError, map, startWith, switchMap, tap } from 'rxjs/operators';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { CreateGeneralConfigurationCommand, GeneralConfigurationDtoPageList, GeneralConfigurationServiceProxy } from 'src/shared/service-proxies/service-proxies';
import { AddEditGeneralConfigrationComponent } from '../add-edit-general-configration/add-edit-general-configration.component';

@Component({
  selector: 'app-list-GeneralConfigrations',
  templateUrl: './list-general-configrations.component.html',
  styleUrls: ['./list-general-configrations.component.scss']
})
export class ListGeneralConfigrationsComponent implements OnInit, OnDestroy {
  public data: CreateGeneralConfigurationCommand;
  List: GeneralConfigurationDtoPageList[];
  dataSource: MatTableDataSource<any>;
  popUpDeleteUserResponse: any;
  resultsLength = 0;
  displayedColumns: string[] = ['id', 'name', 'value', 'actions'];
  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  close: any;
  length = 0;
  subscriptions: Subscription[] = [];
  constructor(private router: Router, private GeneralConfigrationsService: GeneralConfigurationServiceProxy, private _snackBar: MatSnackBar,
    private dialog: MatDialog) { }
  ngOnDestroy(): void {
    this.subscriptions.forEach(s => s.unsubscribe)
  }
  ngAfterViewInit() {
    this.LoadData();
  }
  ngOnInit(): void {

  }

  LoadData() {

    merge(this.paginator.page)
      .pipe(
        startWith({}),
        switchMap(() => {
          return this.GeneralConfigrationsService.getAllGeneralConfiguration(this.paginator.pageIndex + 1, this.paginator.pageSize, "", "", "")
        }),
        map((data) => {        
          this.List = data.items;
          this.resultsLength = data.metadata.totalItemCount;
          return this.List;
        }),
        catchError(() => {
          return observableOf([]);
        })
      )
      .subscribe((data) => {
        this.paginator.pageIndex = this.paginator.pageIndex;
        this.List = data;
      });
  }
  openDialog(el): void {
    const dialogRef = this.dialog.open(AddEditGeneralConfigrationComponent, {
      width: '50%',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.LoadData();
      //this.ngOnInit();
    });
  }
  onEdit(obj) {
    ;
    const dialogRef = this.dialog.open(AddEditGeneralConfigrationComponent, {
      width: '50%',
      data: obj,
      disableClose: true
    })
      .afterClosed().subscribe(result => {
        this.LoadData();
      });
  }
  
}
