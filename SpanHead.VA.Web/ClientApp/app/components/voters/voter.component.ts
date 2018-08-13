import { Component, Input } from "@angular/core";
import { Voter } from "./index";

@Component({
    selector: "voter-details",
    templateUrl:"./voter.component.html"
})

export class VoterComponent {
    // this is for the metadata
    // this function tied to the property binding [voter]="selectedVoter"
    @Input() voter: Voter;
    // componentTitle: string = "Voter Details";
}