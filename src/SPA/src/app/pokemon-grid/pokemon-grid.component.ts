import { Component, OnInit } from '@angular/core';
import { PokedexClient, Pokemon } from './pokemon-service';

@Component({
  selector: 'app-pokemon-grid',
  templateUrl: './pokemon-grid.component.html',
  styleUrls: ['./pokemon-grid.component.scss']
})
export class PokemonGridComponent implements OnInit {

  constructor(private pokedexClient: PokedexClient) { }

  public pokemons: Pokemon[];
  public filterName: string;

  ngOnInit() : void {
    this.refreshSearch();
  }

  submitFilter() : void{
    this.refreshSearch();
  }

  public refreshSearch() : void {
    console.log(`Search Pokemon: ${this.filterName}`);
    this.pokedexClient.pokedex(this.filterName).subscribe((pokemons: Pokemon[])=>{
      console.log(pokemons);
      this.pokemons = pokemons;
		})
  }

}
