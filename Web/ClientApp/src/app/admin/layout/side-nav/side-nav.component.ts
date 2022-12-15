import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-side-nav',
  templateUrl: './side-nav.component.html',
  styleUrls: ['./side-nav.component.scss']
})
export class SideNavComponent implements OnInit {
  showLockupsMenu:boolean = false;
  showUsersMenu:boolean = false;
  showLockupsMenu1:boolean = false;
  showLockupsMenu2:boolean = false;
  showLockupsMenu3:boolean = false;
  showLockupsMenu4:boolean = false;
  showLockupsMenu5:boolean = false;

  constructor() {}

  ngOnInit() {}
}
