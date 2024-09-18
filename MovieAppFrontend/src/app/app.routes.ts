import { Routes } from '@angular/router';
import { MoviesComponent } from './pages/movies/movies.component';
import { UploadMoviesComponent } from './pages/upload-movie/upload-movie.component';

export const routes: Routes = [
  {
    path: '',
    component: MoviesComponent,
  },
  {
    path: 'upload-movie',
    component: UploadMoviesComponent,
  },
];
