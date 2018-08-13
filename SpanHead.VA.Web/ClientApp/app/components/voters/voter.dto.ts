
import { DataField, DropDownDataField } from "../core/common/common.dto";

export class VoterHistory {
    electionYear: number;
    electionType: string;
}

export class Voter {
    id: number;
    precinctName: string;
    firstName: string;
    middleName: string;
    lastName: string;
    race: string;
    origVoter: number;
    suffix: string;
    birthDate: number;
    registerDate: string;
    gender: string;
    streetNumber: string;
    predirectional: string;
    streetName: string;
    streetType: string;
    resext: string;
    address2: string;
    city: string;
    state: string;
    zip: string;
    permAvind: string;
    mailAddress: string;
    mailAddress2: string;
    stateSenateCo: string;
    stateHouseCo: string;
    usCongress: string;
    countyCode: string;
    suffDirection: string;
    voterHistory: VoterHistory[];

    constructor() {
        this.voterHistory = new Array<VoterHistory>();
    }
}

export class MyVoterDetails {
    primaryPhone: DataField;
    secondaryPhone: DataField;
    email: DataField;
    heritage: DataField;
    contactableMethod: DropDownDataField;

    constructor() {
        this.contactableMethod = new DropDownDataField();
        this.email = new DataField();
        this.heritage = new DataField();
        this.primaryPhone = new DataField();
        this.secondaryPhone = new DataField();
    }
}

export class MyVoterComment {
    accountId: number;
    voterId: number;
    comment: DataField;
    insertedBy: string;
    insertedDate: string;
    lastUpdateBy: string;
    updatedDate: string;

    constructor() {
        this.comment = new DataField();
    }
}

export class MyVoter {
    accountId: number;
    voter: Voter
    details: MyVoterDetails;
    comments: MyVoterComment[];
    isContactable: boolean;

    constructor() {
        this.comments = new Array<MyVoterComment>();
        this.voter = new Voter();
        this.details = new MyVoterDetails();
    }
}