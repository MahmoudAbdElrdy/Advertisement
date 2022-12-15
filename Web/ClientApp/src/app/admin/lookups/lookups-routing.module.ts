import { Component, NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { ListCitiesComponent } from './cities/list-cities/list-cities.component';
import { ListCountriesComponent } from './countries/list-countries/list-countries.component';
import { ListAdvertisementsComponent } from './advertisements/list-advertisements/list-advertisements.component'
import { ListUsersComponent } from './Users/list-users/list-users.component';
import { AdvertisementsDetailsComponent } from './advertisements/advertisements-details/advertisements-details.component';
import { ListcomplaintsComponent } from './complaint/list-complaint/list-complaint.component';
import { ListOrdsercomplaintsComponent } from './order-omplaint/list-order-complaint/list-order-complaint.component';

import { ListservicetypeComponent } from './ServiceType/list-servicetype/list-servicetype.component';
import { ListGeneralConfigrationsComponent } from './general-configration/list-general-configrations/list-general-configrations.component';
import { ListRolesComponent } from './roles/list-roles/list-roles.component';
import { ContactUsComponent } from './ContactUs/contact-us/contact-us.component';
const routes: Routes = [
  {
    path: '',
    component: ListCitiesComponent
  },
  {
    path: 'list-cities',
    component: ListCitiesComponent
  }
  ,
  {
    path: 'list-GeneralConfigrations',
    component: ListGeneralConfigrationsComponent
  },
  {
    path: 'list-Countries',
    component: ListCountriesComponent
  },
  {
    path: 'list-complaints',
    component: ListcomplaintsComponent
  },
  {
    path: 'list-orderComplaints',
    component: ListOrdsercomplaintsComponent
  },
  {
    path: 'list-Advertisements',
    component: ListAdvertisementsComponent
  },
  {
    path: 'list-Users',
    component: ListUsersComponent
  },
  {
    path: 'Advertisements-Details',
    component: AdvertisementsDetailsComponent
  }
    ,
    {
        path: 'List-servicetype',
        component: ListservicetypeComponent
    },
    {
      path:'List-Roles',
      component:ListRolesComponent
    },
    {
      path:'List-ContactUs',
      component:ContactUsComponent
    }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class LookupsRoutingModule { }
