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
import { AppConsts } from 'src/AppConsts';
import { AdvertisementDtoPageList, AdvertisementServiceProxy } from 'src/shared/service-proxies/service-proxies';
@Component({
  selector: 'app-list-advertisements',
  templateUrl: './list-advertisements.component.html',
  styleUrls: ['./list-advertisements.component.scss']
})
export class ListAdvertisementsComponent implements OnInit {

  List 		      : AdvertisementDtoPageList[];
  dataSource: MatTableDataSource<any>;
  popUpDeleteUserResponse : any;
  resultsLength = 0;
  
  displayedColumns : string [] = ['id','vendorName','title','adType','cityName','price','fromDate','toDate','image', 'actions'];
	@ViewChild(MatPaginator,{static: false}) paginator : MatPaginator;
	@ViewChild(MatSort,{static: false}) sort           : MatSort;
  baseUrlImage = AppConsts.baseUrlImage;
  close: any;
  length=0;
  subscriptions: Subscription[]=[];
  constructor(	private router : Router,private Service :AdvertisementServiceProxy,private _snackBar: MatSnackBar,
     private dialog: MatDialog) { }
     ngAfterViewInit() {
      ;
     this.LoadData();
     }
  ngOnInit(): void {
  }
  LoadData() {
   
     merge(this.paginator.page)
       .pipe(
         startWith({}),
         switchMap(() => {
    debugger
           return this.Service.getAllAdvertisementDashboard(this.paginator.pageIndex+1,this.paginator.pageSize,"","","")
         }),
         map((data) => {
           // Flip flag to show that loading has finished.
          ;
           this.List = data.items;
           this.resultsLength = data.metadata.totalItemCount;
         
           return  this.List;
         }),
         catchError(() => {
           return observableOf([]);
         })
       )
       .subscribe((data) => {
         ;
         this.paginator.pageIndex= this.paginator.pageIndex;
         this.List = data;
       });
   }
   Reject(el){
     
    if(el.rejected==false){
      el.rejected=true;
    }
    else{
      el.rejected=true;
    }
    this.Service.disableAdvertisement(el.rejected,el.id).subscribe( res=>{
      ;
if(res.rejected==true)
{
  this._snackBar.open("تم التفعيل بنجاح","التفعيل" ,{
    duration: 2220,
    
  });
 
}
else if(res.rejected==false){
  this._snackBar.open("تم الايقاف بنجاح","الايقاف" ,{
    duration: 2220,
    
  });
}
else
{
  this._snackBar.open("حدث خطأ  ","التفعيل" ,{
    duration: 2220,
   
  });
}
this.LoadData();
})
  }
  goToDetails(id: number) {
    ;
   // this.router.navigate(["/lookups/Advertisements-Details", id]);
    this.router.navigateByUrl(
      '/lookups/Advertisements-Details?id=' + id
    );
  }
}
