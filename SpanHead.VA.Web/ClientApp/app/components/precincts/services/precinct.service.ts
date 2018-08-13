import { Injectable } from '@angular/core';
import { ServiceBase, IServiceBase } from '../../core/services/index';
import { Precinct } from '../index';

export interface IPrecinctService extends IServiceBase<Precinct> {
}


Injectable()
export class PrecinctService extends ServiceBase<Precinct> implements IPrecinctService {

    getEntityUrl: string = this.baseUrl + "api/precincts/getprecinct";
    getEntitiesUrl: string = this.baseUrl + "api/precincts/getprecincts";
    saveEntityUrl: string = this.baseUrl + "api/precincts/saveprecinct";
    saveEntitiesUrl: string = this.baseUrl + "api/precincts/saveprecincts";
    deleteEntityUrl: string = this.baseUrl + "api/precincts/deleteprecinct";
}


