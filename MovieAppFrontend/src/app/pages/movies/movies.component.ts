import { Component, DestroyRef, inject } from '@angular/core';
import { MoviesService } from '../../services/movies.service';
import { AsyncPipe } from '@angular/common';
import { Movie } from '../../models/movie';
import { MovieComponent } from '../../components/movie/movie.component';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';


@Component({
  selector: 'app-movies',
  standalone: true,
  imports: [AsyncPipe, MovieComponent],
  templateUrl: './movies.component.html',
  styleUrl: './movies.component.css'
})
export class MoviesComponent {
  private moviesService = inject(MoviesService);
  private destroyRef  =  inject(DestroyRef)
  public moviesObs$ = this.moviesService.fetchMoviesByType();
  public moviesResults: Movie[] = []; 

  
  ngOnInit(){
    this.moviesObs$.pipe(takeUntilDestroyed(this.destroyRef)).subscribe((data) => {
      this.moviesResults = data;
    });
  }
}
