import { Component, DestroyRef, inject, ViewChild, ElementRef } from '@angular/core';
import { MoviesService } from '../../services/movies.service';
import { AsyncPipe, CommonModule } from '@angular/common';
import { MovieComponent } from '../../components/movie/movie.component';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';

@Component({
  selector: 'app-movies',
  standalone: true,
  imports: [CommonModule, AsyncPipe, MovieComponent, ReactiveFormsModule],
  templateUrl: './upload-movie.component.html',
  styleUrls: ['./upload-movie.component.css']
})
export class UploadMoviesComponent {
  private moviesService = inject(MoviesService);
  private destroyRef = inject(DestroyRef);
  public moviesObs$ = this.moviesService.fetchMoviesByType();
  public uploadForm: FormGroup;
  public uploadProgress = 0; // Track upload progress
  public uploadSuccess = false; // Track if the upload was successful

  @ViewChild('videoElement') videoElement!: ElementRef<HTMLVideoElement>;
  @ViewChild('fileInput') fileInput!: ElementRef<HTMLInputElement>;

  constructor(private fb: FormBuilder) {
    this.uploadForm = this.fb.group({
      title: [''],
      description: [''],
      genre: [''],
      movieFile: [null],
      coverImage: [null]
    });
  }

  ngOnInit(){}

  onVideoSelected(event: Event) {
    const file = (event.target as HTMLInputElement).files?.[0];
    if (file) {
      this.uploadForm.patchValue({ movieFile: file });
      const videoUrl = URL.createObjectURL(file);
      this.videoElement.nativeElement.src = videoUrl;
      this.videoElement.nativeElement.load();

      this.videoElement.nativeElement.onloadeddata = () => {
        this.captureCoverImage();
      };
    }
  }

  captureCoverImage() {
    const video = this.videoElement.nativeElement;
    const canvas = document.createElement('canvas');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    const context = canvas.getContext('2d');

    if (context) {
      video.currentTime = 0;
      video.pause();

      setTimeout(() => {
        context.drawImage(video, 0, 0, canvas.width, canvas.height);

        canvas.toBlob((blob) => {
          if (blob) {
            const coverImageFile = new File([blob], 'cover-image.png', { type: 'image/png' });
            this.uploadForm.patchValue({ coverImage: coverImageFile });
          }
        }, 'image/png');
      }, 300);
    }
  }

  uploadMovieWithMetadata() {
    const formData = new FormData();
    formData.append('title', this.uploadForm.get('title')?.value);
    formData.append('description', this.uploadForm.get('description')?.value);
    formData.append('genre', this.uploadForm.get('genre')?.value);
    formData.append('movieFile', this.uploadForm.get('movieFile')?.value);

    const coverImage = this.uploadForm.get('coverImage')?.value;
    if (coverImage) {
      formData.append('coverImage', coverImage, 'cover-image.png');
    }

    this.moviesService.uploadMovie(formData, (progress: number) => {
      this.uploadProgress = progress; 
    }).subscribe({
      next: (response) => {
        console.log('Video and metadata uploaded successfully:', response);
        this.uploadSuccess = true; 
      },
      error: (err) => {
        console.error('Error uploading movie:', err);
      }
    });
  }
}
