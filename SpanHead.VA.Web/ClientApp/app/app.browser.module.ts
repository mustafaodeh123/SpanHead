import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppModuleShared } from './app.shared.module';
import { AppComponent } from './components/app/app.component';

import { ServiceBase } from './components/core/services/index';
import { VoterService } from './components/voters/services/voter.service';
import { AccountService } from './components/accounts/services/appuser.service';
import { PrecinctService } from './components/precincts/services/precinct.service';
import { AuthGuard } from "./components/core/auth/auth.guard";

@NgModule({
    bootstrap: [AppComponent],
    imports: [
        BrowserModule,
        BrowserAnimationsModule,
        AppModuleShared
    ],
    providers: [
        [ServiceBase, AccountService, VoterService, PrecinctService, AuthGuard],
        {
            provide: 'BASE_URL',
            useFactory: getBaseUrl
        }
    ]
})
export class AppModule {
}

export function getBaseUrl() {
    return document.getElementsByTagName('base')[0].href;
}
