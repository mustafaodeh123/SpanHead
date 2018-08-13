import {
    Component, Output, EventEmitter,
    OnInit, OnDestroy
} from "@angular/core";
//import { trigger, state, style, transition, animate, keyframes } from "@angular/animations";
import { Voter, VoterRequest } from "./index";
import { VoterService } from "./services/voter.service";

@Component({
    templateUrl: "./voters.summary.component.html",
    styleUrls: ["../core/common/search.style.css"]
    //animations: [
    //    trigger("focusPanel", [
    //        state("inactive", style({
    //            transform: "scale(1)",
    //        })),
    //        state("active", style({
    //            transform: "scale(1.04)",
    //        })),
    //        transition("inactive => active", animate("500ms ease-in")),
    //        transition("active => inactive", animate("500ms ease-out")),

    //    ]),
    //]
})

export class VoterSummaryComponent implements OnInit, OnDestroy {

    viewMode: string;
    @Output() changed = new EventEmitter<Voter>();
    componentTitle: string;
    voters: Voter[];
    errorMsg: string[] = new Array<string>();
    selectedVoter: Voter;
    request: VoterRequest = new VoterRequest();
    listFilter: string;

    // focusPanel: string = "inactive";

    constructor(private voterService: VoterService) { }

    //toggleFocus() {
    //    this.focusPanel = (this.focusPanel === 'inactive' ? 'active' : 'inactive');
    //}

    ngOnInit(): void {
        this.componentTitle = "Voters Summary";
        this.viewMode = "votersList";
        this.voterService.getEntities(this.request)
            .subscribe(v => {
                if (v.isSuccess) {
                    this.voters = v.data;
                } else {
                    this.errorMsg = v.errors;
                }
            },
            err => this.errorMsg = <any>err);
    }

    filterVoter(value: string) {
        this.request.city = this.request.countyCode = this.request.firstName =
            this.request.lastName = this.request.state = this.request.streetName =
            this.request.streetNumber = this.request.zipCode = value;
        this.voterService.getEntities(this.request)
            .subscribe(v => {
                if (v.isSuccess) {
                    this.voters = v.data;
                } else {
                    this.errorMsg = v.errors;
                }
            },
            err => this.errorMsg = <any>err);
    }

    ngOnDestroy(): void {
        console.log("OnDestroy interface" + new Date().getDate());
    }

    // tslint:disable-next-line:typedef
    select(voter: Voter) {
        this.componentTitle = "Voter Details";
        this.viewMode = "voterDetails";
        this.selectedVoter = voter;
        this.changed.emit(voter);
    }

    changeView() {
        this.componentTitle = "Voters Summary";
        this.viewMode = "votersList";
    }

}