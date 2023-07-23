import { Component, OnInit } from '@angular/core';
import { CommonMethods } from 'app/common.methods';
import { UtilizatoriService } from 'app/services/utilizatori.service';

declare const $: any;
declare interface RouteInfo {
    path: string;
    title: string;
    icon: string;
    class: string;
}
export const ROUTES: RouteInfo[] = [
    { path: 'home/relfollowup', title: 'RelFollowUp',  icon: 'dashboard', class: '' },
    { path: 'home/nourelease', title: 'Adauga release',  icon:'person', class: '' },
    // { path: 'home/table-list', title: 'Table List',  icon:'content_paste', class: '' },
    // { path: 'home/typography', title: 'Typography',  icon:'library_books', class: '' },
    // { path: 'home/icons', title: 'Icons',  icon:'bubble_chart', class: '' },
    // { path: 'home/maps', title: 'Maps',  icon:'location_on', class: '' },
    // { path: 'home/notifications', title: 'Notifications',  icon:'notifications', class: '' },
    // { path: 'home/upgrade', title: 'Upgrade to PRO',  icon:'unarchive', class: 'active-pro' },
];

export const ROUTESAdmin: RouteInfo[] = [
  { path: 'home/aplicatii', title: 'Aplicatii',  icon:'person', class: '' },
  // { path: 'home/table-list', title: 'Table List',  icon:'content_paste', class: '' },
  // { path: 'home/typography', title: 'Typography',  icon:'library_books', class: '' },
  // { path: 'home/icons', title: 'Icons',  icon:'bubble_chart', class: '' },
  // { path: 'home/maps', title: 'Maps',  icon:'location_on', class: '' },
  // { path: 'home/notifications', title: 'Notifications',  icon:'notifications', class: '' },
  // { path: 'home/upgrade', title: 'Upgrade to PRO',  icon:'unarchive', class: 'active-pro' },
];
@Component({
  selector: 'app-sidebar',
  templateUrl: './sidebar.component.html',
  styleUrls: ['./sidebar.component.css']
})
export class SidebarComponent implements OnInit {
  menuItems: any[];
  adminItems: any[];
  isAdmin: boolean = false;
  commonMethods: CommonMethods = new CommonMethods();

  constructor(public userService: UtilizatoriService,) { }


  ngOnInit() {
    this.menuItems = ROUTES.filter(menuItem => menuItem);
    this.adminItems = ROUTESAdmin.filter(menuItem => menuItem);

    this.checkIsAdmin();
  }

  checkIsAdmin = async () => {
    this.isAdmin = await this.commonMethods.checkIsAdmin(this.userService);
    console.log(this.isAdmin)
  }
  isMobileMenu() {
      if ($(window).width() > 991) {
          return false;
      }
      return true;
  };
}
