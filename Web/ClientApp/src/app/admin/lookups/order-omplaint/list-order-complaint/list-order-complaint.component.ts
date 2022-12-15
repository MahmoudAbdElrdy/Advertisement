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
import { OrderComplaintServiceProxy, OrderComplaintDtoPageList, CreateOrderComplaintCommand, RegionManagementServiceProxy, EditOrderComplaintCommand } from 'src/shared/service-proxies/service-proxies';
import { detailsOrderComplaintComponent } from '../details-order-complaint/details-order-complaint.component';


@Component({
  selector: 'app-list-orderComplaints',
  templateUrl: './list-order-complaint.component.html',
  styleUrls: ['./list-order-complaint.component.scss']
})
export class ListOrdsercomplaintsComponent implements OnInit, OnDestroy {
  public data: CreateOrderComplaintCommand;
  List: OrderComplaintDtoPageList[];
  dataSource: MatTableDataSource<any>;
  popUpDeleteUserResponse: any;
  resultsLength = 0;
  displayedColumns = [ 'id', 'clientName', 'adOwnerName', 'isComplaintSeen', 'actions'];

  @ViewChild(MatPaginator, { static: false }) paginator: MatPaginator;
  @ViewChild(MatSort, { static: false }) sort: MatSort;
  close: any;
  length = 0;
  subscriptions: Subscription[] = [];
  constructor(private router: Router, private complaintService: OrderComplaintServiceProxy, private _snackBar: MatSnackBar,
    private Service: RegionManagementServiceProxy, private dialog: MatDialog) { }
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
          return this.complaintService.getAllOrderComplaint(this.paginator.pageIndex + 1, this.paginator.pageSize, "", "", "")
        }),
        map((data) => {
          debugger
          // Flip flag to show that loading has finished.
          this.List = data.items;
          this.resultsLength = data.metadata.totalItemCount;
          return this.List;
        }),
        catchError(() => {
          return observableOf([]);
        })
      )
      .subscribe((data) => {
        ;
        this.paginator.pageIndex = this.paginator.pageIndex;
        this.List = data;
      });
  }
  openDialog(el): void {
    const dialogRef = this.dialog.open(detailsOrderComplaintComponent, {
      width: '50%',
    });

    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
      this.LoadData();
      //this.ngOnInit();
    });
  }
  onEdit(obj) {
    const dialogRef = this.dialog.open(detailsOrderComplaintComponent, {
      width: '50%',
      data: obj,
      disableClose: true
    })
      .afterClosed().subscribe(result => {
        this.LoadData();
      });
  }
  seen(el) {
    const message = `هل انت متاكد تعديل البلاغ?`;

    const dialogData = new ConfirmDialogModel("تاكيد العملية", message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '30%',
      data: dialogData,
      disableClose: true
    });
    dialogRef.afterClosed().subscribe(dialogResult => {
      this.close = dialogResult;
      if (dialogResult == false) {
        el.isComplaintSeen = !el.isComplaintSeen;
        var edit: EditOrderComplaintCommand = new EditOrderComplaintCommand();
        edit.complaintReason = el.complaintReason;
        edit.complaintReasonReplay = el.complaintReasonReplay;
        edit.id = el.id;
        edit.isComplaintSeen = el.isComplaintSeen;
        this.complaintService.editOrderComplaint(edit).subscribe(res => {

          if (res.isComplaintSeen == el.isComplaintSeen) {
            this._snackBar.open("تم التعديل بنجاح", "التعديل", {
              duration: 2220,

            });

          }
          else {
            this._snackBar.open("حدث خطأ عند التعديل", "التعديل", {
              duration: 2220,

            });
          }
          this.LoadData();
        })

      }
      else {

      }

    });

  }
}
