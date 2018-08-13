//import {
//    AfterViewChecked,
//    Component,
//    EventEmitter,
//    Input,
//    OnChanges,
//    Output,
//    SimpleChange,
//    ViewEncapsulation
//} from "@angular/core";

//import {
//    SortOrder,
//    SortDetails,
//    Pagination,
//    TableRow,
//    Table
//} from "./sorted-table.dto";

//declare var $;

//@Component({
//    selector: "sorted-table",
//    templateUrl: "./sorted-table.component.heml",
//    styleUrls: ["./sorted-table.component.css"],
//    encapsulation: ViewEncapsulation.None
//})

//export class SortedTableComponent implements OnChanges, AfterViewChecked  {

//    @Input() data: Table;
//    @Input() sortDetails: SortDetails;
//    @Input() paginate: boolean = true;
//    @Input() pageSize: number = 10;
//    @Input() useSort: boolean = true;
//    @Output() onSort = new EventEmitter<SortDetails>();

//    sortedRows: TableRow[];
//    filteredRows: TableRow[];
//    numColumns: number;

//    pagination: Pagination = {
//        page: 1,
//        pageSize: 10,
//        start: 1,
//        end: 10,
//        length: 11
//    };

//    sortOrder = SortOrder;

//    setDefault() {

        
//    }

//    ngOnChanges(changes: SimpleChange) {
//        if (!this.data) {
//            this.filteredRows = [];
//            return;
//        }

//        this.setDefault();
//    }

//    ngAfterViewChecked() {

//    }

//}
