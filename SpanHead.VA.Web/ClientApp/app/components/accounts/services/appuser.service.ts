import { Injectable, Inject, PLATFORM_ID } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { isPlatformBrowser } from '@angular/common';

import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import "../../core/services/rxjs.extensions";

import { ServiceResponse } from "../../core/common/index";
import { ServiceBase, IServiceBase } from '../../core/services/index';
import { AppUser } from '../index';

export interface IAccountService extends IServiceBase<AppUser> {
    login(user: AppUser): Observable<ServiceResponse<AppUser>>;
}

Injectable()
export class AccountService extends ServiceBase<AppUser> implements IAccountService {

    getEntityUrl: string = this.baseUrl + "api/accounts/getuser";
    getEntitiesUrl: string = this.baseUrl + "api/accounts/getusers";
    saveEntityUrl: string = this.baseUrl + "api/accounts/saveuser";
    saveEntitiesUrl: string = this.baseUrl + "api/accounts/saveusers";
    deleteEntityUrl: string = this.baseUrl + "api/accounts/deleteuser";
    loginUrl: string = this.baseUrl + "api/accounts/login";

    // Observable navItem source
    private _authNavStatusSource = new BehaviorSubject<boolean>(false);
    // Observable navItem stream
    authNavStatus$ = this._authNavStatusSource.asObservable();

    private loggedIn = false;

    constructor(@Inject(Http) http: Http, @Inject("BASE_URL") baseUrl: string,
                @Inject(PLATFORM_ID) platformId: Object) {
        super(http, baseUrl, platformId);
        this.loggedIn = !!localStorage.getItem('auth_token');
        // ?? not sure if this the best way to broadcast the status but seems to resolve issue on page refresh where auth status is lost in
        // header component resulting in authed user nav links disappearing despite the fact user is still logged in
        this._authNavStatusSource.next(this.loggedIn);
    }


    login(user: AppUser): Observable<ServiceResponse<AppUser>> {
        let body = JSON.stringify(user);
        let headers = new Headers({ "Content-Type": "application/json" });
        let options = new RequestOptions({ headers: headers });

        return this.http.post(this.loginUrl, body, options)
            .map((response: Response) => <ServiceResponse<AppUser>>response.json())
            .map(res => {
                if (res.isSuccess) {
                    localStorage.setItem('auth_token', res.data.accessToken);
                    this.loggedIn = true;
                    this._authNavStatusSource.next(true);
                   // return true;
                }
                else {
                    this.logout();
                }
                return res;
            })
            .catch(this.handlerError);
    }

    logout() {
        localStorage.removeItem('auth_token');
        this.loggedIn = false;
        this._authNavStatusSource.next(false);
    }

    isLoggedIn() {
        return this.loggedIn;
    }
}