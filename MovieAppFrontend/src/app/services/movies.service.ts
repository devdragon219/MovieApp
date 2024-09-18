import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpEventType, HttpRequest, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';
import { tap, filter, map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MoviesService {

  private apiUrl = environment.SERVER_URL;
  private httpClient = inject(HttpClient);

  constructor() { }

  fetchMoviesByType(): Observable<any> {
    return this.httpClient.get<any>(`${this.apiUrl}/api/movies/`);
  }

  uploadMovie(formData: FormData, onProgress: (progress: number) => void): Observable<any> {
    const req = new HttpRequest('POST', `${this.apiUrl}/api/movies`, formData, {
      reportProgress: true, 
    });

    return this.httpClient.request(req).pipe(
      tap(event => {
       
        if (event.type === HttpEventType.UploadProgress) {
          const progress = event.total ? Math.round((event.loaded / event.total) * 100) : 0;
          onProgress(progress);
        }
      }),

      filter(event => event.type === HttpEventType.Response),
      map(event => {
        if (event instanceof HttpResponse) {
          return event.body;
        }
        return null;
      })
    );
  }
}
