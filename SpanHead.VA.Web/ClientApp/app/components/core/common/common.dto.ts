
export class NameValueItem {
    name: string = "";
    value: number;

    constructor() {
        this.name = "";
    }
}

export class NameValueCodeItem extends NameValueItem {
    code: string;
    description: string;
    displayOrder: number;

    constructor() {
        super();
        this.code = "";
        this.description = "";
    }
}

export class ValidatableField {
    errorSection: string;
    errorText: string;
    isError: boolean;

    constructor() {
        this.errorSection = "";
        this.errorText = "";
        this.isError = false;
    }
}

export class DropDownDataField extends ValidatableField {
    value: NameValueCodeItem;

    constructor() {
        super();
        this.value = new NameValueCodeItem();
    }
}

export class DataField extends ValidatableField {
    value: string;

    constructor() {
        super();
        this.value = "";
    }
}
