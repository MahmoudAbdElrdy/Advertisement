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
import { AddEditCityComponent } from '../add-edit-city/add-edit-city.component';
import { AddCityCommand, CityDtoPageList, CitiesServiceProxy, RegionManagementServiceProxy, CountriesServiceProxy, CountryDto } from 'src/shared/service-proxies/service-proxies';


@Component({
  selector: 'app-list-cities',
  templateUrl: './list-cities.component.html',
  styleUrls: ['./list-cities.component.scss']
})
export class ListCitiesComponent implements  OnInit,OnDestroy {
  public data: AddCityCommand;
  List 		      : CityDtoPageList[];
  dataSource: MatTableDataSource<any>;
  CountryDDL: CountryDto[] = [];
  popUpDeleteUserResponse : any;
  resultsLength = 0;
  displayedColumns = [ 'id', 'nameAr', 'nameEn', 'countryName', 'actions'];
	@ViewChild(MatPaginator,{static: false}) paginator : MatPaginator;
	@ViewChild(MatSort,{static: false}) sort           : MatSort;
  close: any;
  length=0;
  subscriptions: Subscription[]=[];
  constructor(	private router : Router,private CitiesService :CitiesServiceProxy,private CountriesService :CountriesServiceProxy,private _snackBar: MatSnackBar,
    private Service : RegionManagementServiceProxy, private dialog: MatDialog) { }
  ngOnDestroy(): void {
   this.subscriptions.forEach(s=>s.unsubscribe)
  }
  ngAfterViewInit() {
   ;
  this.LoadData();
  }
  ngOnInit(): void {
  
  }
  applyFilter(filterValue: string) {
    this.dataSource.filter = filterValue.trim().toLowerCase();
    if (this.dataSource.paginator) {
      this.dataSource.paginator.firstPage();
    }
  }
  LoadCountries(){

    return this.Service.countries(1,150,"","","").subscribe(res=>{
      this.CountryDDL=res.items;
    })

    
  }
LoadData() {
 ;
  // If the user changes the sort order, reset back to the first page.
  // this.sort.sortChange.subscribe(() => (this.paginator.pageIndex = 0));

  merge(this.paginator.page)
    .pipe(
      startWith({}),
      switchMap(() => {
     ;
        return this.Service.cities(this.paginator.pageIndex+1,this.paginator.pageSize,"","","")
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
openDialog(el): void {
  const dialogRef = this.dialog.open(AddEditCityComponent, {
    width: '50%',
  });

  dialogRef.afterClosed().subscribe(result => {
    console.log(`Dialog result: ${result}`);
    this.LoadData();
    //this.ngOnInit();
  });
}
onEdit(obj){
 ;
  const dialogRef = this.dialog.open(AddEditCityComponent, {
    width: '50%',
    data:obj,
    disableClose:true
  })
  .afterClosed().subscribe(result => {
    this.LoadData();
  });
}
deleteItem(el){
  const message = `Are you sure you want to do this?`;

    const dialogData = new ConfirmDialogModel("Confirm Action", message);

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '30%',
      data: dialogData,
      disableClose:true
    });
    dialogRef.afterClosed().subscribe( dialogResult => {
      this.close = dialogResult;
      //this.result = dialogResult;
      ;
      if(dialogResult==false){
        this.CitiesService.deleteCity(el.id).subscribe( res=>{
          ;
    if(res.items!==null)
    {
      this._snackBar.open("تم الحذف بنجاح","الحذف" ,{
        duration: 2220,
        
      });
     
    }
    else
    {
      this._snackBar.open("حدث خطأ عند الحذف","الحذف" ,{
        duration: 2220,
        
      });
    }
    this.LoadData();
  })

      }
      else{

      }
     
    });

}
}
