import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { PokemonGridComponent } from './pokemon-grid/pokemon-grid.component';
import { PokemonService, PokemonServiceApiBaseUrl } from './pokemon-grid/pokemon-service';
import { EsquioService, EsquioServiceApiBaseUrl } from './feature-toggle/esquio-service';
import { FeatureFlagGuardService } from './feature-toggle/feature-flag-guard.service';
import { environment } from '../environments/environment';

@NgModule({
  declarations: [
    AppComponent,
    PokemonGridComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule
  ],
  providers: [
    FeatureFlagGuardService,
    PokemonService,
    EsquioService,
    { provide: PokemonServiceApiBaseUrl, useValue: environment.API_BASE_URL },
    { provide: EsquioServiceApiBaseUrl, useValue: environment.API_BASE_URL }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
