import { Component, OnInit } from "@angular/core";
import { AccountService } from "../accounts/services/appuser.service";
import { Router } from "@angular/router";

@Component({
    template: "<div>Logout...</div>"
})

export class LogoutComponent implements OnInit {
    
    constructor(private accountService: AccountService, private router: Router) { }


    ngOnInit() {
        this.accountService.logout();
        this.router.navigate(['/login']);
        return false;
    }
}
