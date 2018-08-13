import { PipeTransform, Pipe } from "@angular/core";
import { VoterService } from "../services/voter.service";
import { Voter, VoterRequest } from "../index";
import { ServiceResponse } from "../../core/common/service.responce"; 

@Pipe({
    name: "voterFilter"
})

export class VoterFilter implements PipeTransform {
    voters: ServiceResponse<Voter[]>;
    request: VoterRequest = new VoterRequest();

    constructor(private voterService: VoterService) { }

    transform(value: Voter[], filterBy: string): Voter[] {
        this.request.city = this.request.countyCode = this.request.firstName =
            this.request.lastName = this.request.state = this.request.streetName =
            this.request.streetNumber = this.request.zipCode = filterBy;

        this.voterService.getEntities(this.request)
            .subscribe(v => this.voters = v,
            err => {
                this.voters.errors = <any>err;
                this.voters.isSuccess = false;
            });

        return this.voters.data;
    }
}