export interface Movie {
  id: number;
  title: string;
  description: string;
  genre: string;
  coverImage: string;
  movieFilePath: string;
}

export interface Movies {
  results: Movie[];
}


