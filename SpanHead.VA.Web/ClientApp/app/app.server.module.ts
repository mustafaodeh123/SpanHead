import { NgModule } from '@angular/core';
import { ServerModule } from '@angular/platform-server';
import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './components/app/app.component';

import { ServiceBase } from './components/core/services/index';
import { VoterService } from './components/voters/services/voter.service';
import { AccountService } from './components/accounts/services/appuser.service';
import { PrecinctService } from './components/precincts/services/precinct.service';
import { AuthGuard } from "./components/core/auth/auth.guard";

@NgModule({
    bootstrap: [ AppComponent ],
    imports: [
        ServerModule,
        AppModuleShared
    ],

    providers: [[ServiceBase, AccountService, VoterService, PrecinctService, AuthGuard]],

})
export class AppModule {
}
