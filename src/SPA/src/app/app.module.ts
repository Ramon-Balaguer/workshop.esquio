import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule } from '@angular/common/http';
import { PokemonGridComponent } from './pokemon-grid/pokemon-grid.component';
import { PokedexClient, PokemonServiceApiBaseUrl } from './pokemon-grid/pokemon-service';

import { EsquioService, EsquioServiceApiBaseUrl } from './feature-toggle/esquio-service';
import { FeatureFlagGuardService } from './feature-toggle/feature-flag-guard.service';
import { RemoveIfFeatureOffDirective } from './feature-toggle/remove-if-feature-off.directive';
import { RemoveIfFeatureOnDirective } from './feature-toggle/remove-if-feature-on.directive';


import { environment } from '../environments/environment';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';

@NgModule({
  declarations: [
    AppComponent,
    RemoveIfFeatureOffDirective,
    RemoveIfFeatureOnDirective,
    PokemonGridComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgbModule
  ],
  providers: [
    FeatureFlagGuardService,
    PokedexClient,
    EsquioService,
    { provide: PokemonServiceApiBaseUrl, useValue: environment.API_BASE_URL },
    { provide: EsquioServiceApiBaseUrl, useValue: environment.API_BASE_URL }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
