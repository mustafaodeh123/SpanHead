export class MyVoterRequest {
    accountId: number;
    precinctId: number;
    lastName: string;
    firstName: string;
    city: string;
    state: string;
    zipCode: string;
}

export class VoterRequest extends MyVoterRequest {
    birthDateFrom: number;
    ibirthDateTo: number;
    registerDateFrom: string;
    registerDateTo: string;
    countyCode: string;
    gender: string;
    streetNumber: string;
    streetName: string;
}

export class ContactableMethodOption {
    public static readonly byPhone = "ByPhone";
    public static readonly byEmail = "ByEmail";
    public static readonly byMail = "ByMail";
}

export class ElectionTypeOption {
    public static readonly primary = "Primary";
    public static readonly general = "General";
}

export class GenderOption {
    public static readonly female = "Female";
    public static readonly male = "Male";
    public static readonly other = "Other";
}