import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { PokemonGridComponent } from './pokemon-grid/pokemon-grid.component';
import { FeatureFlagGuardService } from './feature-toggle/feature-flag-guard.service';


const routes: Routes = [
  {
      path: "pokemons",
      component: PokemonGridComponent,
      canActivate: [FeatureFlagGuardService],
      data: { featureName: 'Pokedex' }
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
