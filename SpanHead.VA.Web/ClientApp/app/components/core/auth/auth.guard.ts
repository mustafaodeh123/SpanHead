// auth.guard.ts
import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AccountService } from '../../accounts/services/appuser.service';

@Injectable()
export class AuthGuard implements CanActivate {
    constructor(private accountService: AccountService, private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
        let url: string = state.url;
        console.log('Url:' + url);
        if (this.accountService.isLoggedIn()) {
            return true;
        }
        this.router.navigate(['/login']);
        return false;
    }

    //canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    //    if (!this.accountService.isLoggedIn()) {
    //        this.router.navigate(['/login']);
    //        return false;
    //    }
    //    return true;
    //}  
}
