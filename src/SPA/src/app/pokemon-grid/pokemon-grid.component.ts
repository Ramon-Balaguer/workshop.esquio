import { Component, OnInit } from '@angular/core';
import { PokemonService, Pokemon } from './pokemon-service';

@Component({
  selector: 'app-pokemon-grid',
  templateUrl: './pokemon-grid.component.html',
  styleUrls: ['./pokemon-grid.component.scss']
})
export class PokemonGridComponent implements OnInit {

  constructor(private pokemonService: PokemonService) { }

  public pokemons: Pokemon[];

  ngOnInit(): void {
    this.pokemonService.pokedex().subscribe((pokemons: Pokemon[])=>{  
      console.log(pokemons);  
      this.pokemons = pokemons;
		})  

  }

}
