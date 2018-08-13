import { Component } from '@angular/core';
import { Router } from '@angular/router';

import { AppUser } from './index';
import { AccountService } from './services/appuser.service';

@Component({
    templateUrl: "./appuser.component.html",
    styleUrls: ["./appuser.component.css"]
})

export class AppUserComponent {

    componentTitle: "Login";
    appUser: AppUser = new AppUser();
    errorMsg: string[] = new Array<string>();
    userName: string;

    constructor(private accountService: AccountService, private router: Router) { }

    login() {
        this.accountService.login(this.appUser)
            .subscribe(v => {
                if (v.isSuccess) {
                    this.router.navigate(['/Home']);
                } else {
                    this.errorMsg = v.errors;
                }
            },
            err => this.errorMsg = <any>err);
    }
   
}