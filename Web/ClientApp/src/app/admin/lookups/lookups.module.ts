import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { MatSortModule } from '@angular/material/sort';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { MatButtonModule } from '@angular/material/button';
import { MatTooltipModule } from '@angular/material/tooltip';
import { MatDialogModule } from '@angular/material/dialog';
import { MatSnackBarModule } from '@angular/material/snack-bar';
import { LookupsRoutingModule } from './lookups-routing.module';
import { ListCitiesComponent } from './cities/list-cities/list-cities.component';
import { DataService } from './data.service';
import { AddEditCityComponent } from './cities/add-edit-city/add-edit-city.component';
import { ListCountriesComponent } from './countries/list-countries/list-countries.component';
import { AddEditCountryComponent } from './countries/add-edit-country/add-edit-country.component';
import { AdComplaintServiceProxy, AdvertisementServiceProxy, OrderComplaintServiceProxy, AuthServiceProxy, CitiesServiceProxy, CountriesServiceProxy, GeneralConfigurationServiceProxy, RegionManagementServiceProxy, ServiceProxy, ServicesServiceProxy, UserManagementServiceProxy, UsersServiceProxy, RolesServiceProxy, ContactUsServiceProxy } from 'src/shared/service-proxies/service-proxies';
import { MatSelectModule } from '@angular/material/select';
import { ListAdvertisementsComponent } from './advertisements/list-advertisements/list-advertisements.component';
import { CustomDatePipe } from 'src/app/shared/CustomDatePipe.pipe';
import { ConfirmDialogDisabledComponent } from './advertisements/confirm-dialog-disabled/confirm-dialog-disabled.component';
import { ListUsersComponent } from './Users/list-users/list-users.component';
import { AdvertisementsDetailsComponent } from './advertisements/advertisements-details/advertisements-details.component';
import { MatCardModule } from '@angular/material/card';
import { RegisterComponent } from './Users/register/register.component';
import { ListcomplaintsComponent } from './complaint/list-complaint/list-complaint.component';
import { detailsComplaintComponent } from './complaint/details-complaint/details-complaint.component';
import { ListOrdsercomplaintsComponent } from './order-omplaint/list-order-complaint/list-order-complaint.component';
import { detailsOrderComplaintComponent } from './order-omplaint/details-order-complaint/details-order-complaint.component';
import { BrowserModule } from '@angular/platform-browser';
import { ListservicetypeComponent } from './ServiceType/list-servicetype/list-servicetype.component';
import { AddEditservicetypeComponent } from './ServiceType/servicetype/add-edit-servicetype.component';
import { ListRolesComponent } from './roles/list-roles/list-roles.component';
import { AddEditRoleComponent } from './roles/add-edit-role/add-edit-role.component';
import { MatTabsModule } from '@angular/material/tabs';
import { ListGeneralConfigrationsComponent } from './general-configration/list-general-configrations/list-general-configrations.component';
import { AddEditGeneralConfigrationComponent } from './general-configration/add-edit-general-configration/add-edit-general-configration.component';
import { ContactUsComponent } from './ContactUs/contact-us/contact-us.component';
import { AddEditContactComponent } from './ContactUs/add-edit-contact/add-edit-contact.component';
@NgModule({
  imports: [
    CommonModule,
    LookupsRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    MatTableModule,
    MatFormFieldModule,
    MatPaginatorModule,
    MatSortModule,
    MatInputModule,
    MatCheckboxModule,
    MatButtonModule,
    MatTooltipModule,
    MatDialogModule,
    MatSnackBarModule,
    MatSelectModule,
    MatCardModule,MatTabsModule 
  ],
    declarations: [ListCitiesComponent, AddEditCityComponent, ListcomplaintsComponent,ListOrdsercomplaintsComponent, detailsOrderComplaintComponent,detailsComplaintComponent, ListservicetypeComponent, AddEditservicetypeComponent,ListCountriesComponent, AddEditCountryComponent, ListAdvertisementsComponent,CustomDatePipe, ConfirmDialogDisabledComponent, ListUsersComponent, AdvertisementsDetailsComponent, RegisterComponent, ListRolesComponent, AddEditRoleComponent,ListGeneralConfigrationsComponent,AddEditGeneralConfigrationComponent, ContactUsComponent, AddEditContactComponent],
    providers: [ContactUsServiceProxy,DataService, RegionManagementServiceProxy, OrderComplaintServiceProxy,AdComplaintServiceProxy,RolesServiceProxy, ServicesServiceProxy, ServiceProxy,CountriesServiceProxy,CitiesServiceProxy,AdvertisementServiceProxy,UsersServiceProxy,AuthServiceProxy,UserManagementServiceProxy,GeneralConfigurationServiceProxy ],
  entryComponents: [AddEditCityComponent,detailsComplaintComponent,detailsOrderComplaintComponent,AddEditRoleComponent,AddEditGeneralConfigrationComponent],
})
export class LookupsModule { }
