import { Component, OnInit, ViewChild } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { MatPaginator } from '@angular/material/paginator';
import { MatSnackBar } from '@angular/material/snack-bar';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { Router } from '@angular/router';
import { catchError, map, startWith, switchMap } from 'rxjs/operators';
import { AddContactUsCommand, ContactUsDtoPageList, ContactUsServiceProxy, EditContactUsCommand } from 'src/shared/service-proxies/service-proxies';
import { merge, of as observableOf } from 'rxjs';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent, ConfirmDialogModel } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { AddEditContactComponent } from '../add-edit-contact/add-edit-contact.component';
@Component({
  selector: 'app-contact-us',
  templateUrl: './contact-us.component.html',
  styleUrls: ['./contact-us.component.scss']
})
export class ContactUsComponent implements OnInit {
  @ViewChild("CommitModelComment", { static: false }) CommitModelComment;
  @ViewChild(MatPaginator,{static: false}) paginator : MatPaginator;
	@ViewChild(MatSort,{static: false}) sort           : MatSort;
  getContactUsDto: any;
  public data: AddContactUsCommand;
  UpdateContactUsDto = new EditContactUsCommand;
  List 		      : ContactUsDtoPageList[];
  dataSource: MatTableDataSource<any>;
  popUpDeleteUserResponse : any;
  resultsLength = 0;
  displayedColumns = ['id',  'name', 'email', 'content','isContact', 'actions'];
  close: any;
  constructor(private formBuilder: FormBuilder,
    private contactUsService: ContactUsServiceProxy,
    private route: Router,private _snackBar: MatSnackBar,private dialog: MatDialog) { }

    ngAfterViewInit() {
      ;
     this.LoadData();
     }
     ngOnInit(): void {
     
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
             return this.contactUsService.contactUs(this.paginator.pageIndex+1,this.paginator.pageSize,"","","")
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
     onEdit(obj){
      
       const dialogRef = this.dialog.open(AddEditContactComponent, {
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
            this.contactUsService.deleteContactUs(el.id).subscribe( res=>{
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
