import { Injectable, Inject, PLATFORM_ID } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { isPlatformBrowser } from '@angular/common';
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';
import "./rxjs.extensions";

import { ServiceResponse } from "../common/index";

export interface IServiceBase<TEntity> {
    getEntity(id: number): Observable<ServiceResponse<TEntity>>;
    getEntities(request: any): Observable<ServiceResponse<TEntity[]>>;
    saveEntity(entity: TEntity): Observable<ServiceResponse<any>>;
    saveEntities(entities: TEntity[]): Observable<ServiceResponse<any>>;
    deleteEntity(entity: TEntity): Observable<ServiceResponse<any>>;

    getEntityUrl: string;
    getEntitiesUrl: string;
    saveEntityUrl: string;
    saveEntitiesUrl: string;
    deleteEntityUrl: string;
}

@Injectable()
export abstract class ServiceBase<TEntity> implements IServiceBase<TEntity>{

    abstract getEntityUrl: string;
    abstract getEntitiesUrl: string;
    abstract saveEntityUrl: string;
    abstract saveEntitiesUrl: string;
    abstract deleteEntityUrl: string;

    protected options: RequestOptions;

    constructor(@Inject(Http) protected http: Http, @Inject("BASE_URL") protected baseUrl: string,
                @Inject(PLATFORM_ID) protected platformId: Object) {
        
        if (isPlatformBrowser(this.platformId)) {
            let authToken = localStorage.getItem("auth_token");
            let headers = new Headers({ "Content-Type": "application/json", "Authorization": `Bearer ${authToken}` });
            this.options = new RequestOptions({ headers: headers });
        }
    }

    getEntity(id: number): Observable<ServiceResponse<TEntity>> {

        return this.http.get(`${this.getEntityUrl}/${id}`, this.options)
            .map((response: Response) => <ServiceResponse<TEntity>>response.json())
            // .do(data => console.log('All: ' + JSON.stringify(data)))
            .catch(this.handlerError);
    }

    getEntities(request: any): Observable<ServiceResponse<TEntity[]>> {
        let body = JSON.stringify(request);

        return this.http.post(this.getEntitiesUrl, body, this.options)
            .map((response: Response) => <ServiceResponse<TEntity[]>>response.json())
            //.do(data => console.log("MyData: " + JSON.stringify(data)))
            .catch(this.handlerError);
    }

    saveEntity(entity: TEntity): Observable<ServiceResponse<any>> {
        let body = JSON.stringify(entity);

        return this.http.post(this.saveEntityUrl, body, this.options)
            .map((response: Response) => <ServiceResponse<any>>response.json())
            .catch(this.handlerError);
    }

    saveEntities(entities: TEntity[]): Observable<ServiceResponse<any>> {
        let body = JSON.stringify(entities);

        return this.http.post(this.saveEntitiesUrl, body, this.options)
            .map((response: Response) => <ServiceResponse<any>>response.json())
            .catch(this.handlerError);
    }

    deleteEntity(entity: TEntity): Observable<ServiceResponse<any>> {
        let body = JSON.stringify(entity);

        return this.http.post(this.deleteEntityUrl, body, this.options)
            .map((response: Response) => <ServiceResponse<any>>response.json())
            .catch(this.handlerError);
    }


    protected handlerError(error: Response) {
        console.error("My Error: " + error);

        if (error.status === 400)
            return Observable.throw(error.json().error || "Bad request");
        else if (error.status === 401)
            return Observable.throw(error.json().error || "Unauthorized: Sorry you cannot access this service!");
        else if (error.status === 404)
            return Observable.throw(error.json().error || "Service is not available!");
        else if (error.status === 500)
            return Observable.throw(error.json().error || "Server Error!");
        else
            return Observable.throw(error.json().error || "Cannot make this request at this moment!");
    }

}