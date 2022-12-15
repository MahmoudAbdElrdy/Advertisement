import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { AppConsts } from 'src/AppConsts';
import {  AdvertisementServiceProxy, SpaceInfoDto } from 'src/shared/service-proxies/service-proxies';

@Component({
  selector: 'app-advertisements-details',
  templateUrl: './advertisements-details.component.html',
  styleUrls: ['./advertisements-details.component.scss']
})
export class AdvertisementsDetailsComponent implements OnInit {
  displayedColumns : string [] = ['id','adType','price','fromDate','toDate'];

  id: any;
AdvertisementDetailDto:SpaceInfoDto;
//AdDto:AdDto[];
images: string[];
baseUrlImage = AppConsts.baseUrlImage;
  constructor( private activatedRoute: ActivatedRoute,
    private Service: AdvertisementServiceProxy,) { }

    ngOnInit() {
      ;
      this.activatedRoute.queryParams.subscribe(parm => {
        let querySting = parm['id'];
        if (querySting) {
          this.id = querySting;
          this.Service.getAdvertisementDetail(this.id).subscribe(
            (result) => {
              console.log(result);
              ;
              this.AdvertisementDetailDto = result;
            //  this.AdDto=result?.adList;
              this.images=result?.images;
  
            },
            (err) => {
              this.errorOccured(err);
            }
          );
        }
      });
    }
    errorOccured(err: any) {
      throw new Error("Method not implemented.");
    }
}
