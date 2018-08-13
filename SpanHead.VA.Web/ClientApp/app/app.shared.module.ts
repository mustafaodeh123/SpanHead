import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";
import { HttpModule } from "@angular/http";
import { RouterModule } from "@angular/router";

import { AuthGuard } from "./components/core/auth/auth.guard";

import { AppComponent } from "./components/app/app.component";
import { NavMenuComponent, LogoutComponent } from "./components/navmenu/index";
import { VoterSummaryComponent } from "./components/voters/voters.summary.component";
import { VoterComponent } from "./components/voters/voter.component";
//import { VoterFilter } from "./components/voters/filters/voter.filter";
import { AppUserComponent } from "./components/accounts/index";

@NgModule({
    declarations: [
        AppComponent,
        NavMenuComponent,
        LogoutComponent,
        VoterComponent,
        VoterSummaryComponent,
        //VoterFilter,
        AppUserComponent
    ],
    imports: [
        CommonModule,
        HttpModule,
        FormsModule,
        RouterModule.forRoot([
            { path: "", redirectTo: "home", pathMatch: "full" },
            { path: "home", component: VoterSummaryComponent, canActivate: [AuthGuard] },
            { path: "login", component: AppUserComponent },
            { path: "logout", component: LogoutComponent },
            { path: "**", redirectTo: "home" } // we need a not found page!
        ])
    ]
})
export class AppModuleShared {
}
