
export interface IAppUser {
    accountId: number;
    firstName: string;
    lastName: string;
    isActive: boolean;
}

export class JWTDto {
    accessToken: string;
    expiresIn: number;
    constructor() {
        this.accessToken = "";
    }
}

export class AppUser extends JWTDto implements IAppUser {
    accountId: number;
    firstName: string;
    lastName: string;
    isActive: boolean;
    privileges: any;
    userName: string;
    password: string;

    constructor() {
        super();
        this.userName = "";
        this.password = "";
        this.privileges = null;
    }
}