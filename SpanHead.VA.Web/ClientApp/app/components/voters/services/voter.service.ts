import { Injectable } from "@angular/core";
import { ServiceBase, IServiceBase } from "../../core/services/index";
import { Voter } from "../index";

export interface IVoterService extends IServiceBase<Voter> {
}


Injectable()
export class VoterService extends ServiceBase<Voter> implements IVoterService {

    getEntityUrl: string = this.baseUrl + "api/voters/getvoter";
    getEntitiesUrl: string = this.baseUrl + "api/voters/getvoters";
    saveEntityUrl: string = this.baseUrl + "api/voters/savevoter";
    saveEntitiesUrl: string = this.baseUrl + "api/voters/savevoters";
    deleteEntityUrl: string = this.baseUrl + "api/voters/deletevoter";
}


