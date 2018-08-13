import { Voter } from '../voters/index';

export class Precinct {
    id: number;
    name: string;
    number: string;
    location: string;
    predirectional: string;
    voters: Voter[];

    constructor() {
        this.voters = new Array<Voter>();
    }
}