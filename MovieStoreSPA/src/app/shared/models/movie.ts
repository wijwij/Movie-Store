import { Genre } from './genre';
import { Cast } from './cast';

export interface Movie {
  id: number;
  title: string;
  posterUrl: string;
  tagline: string;
  runTime: number;
  createdDate: Date;
  price: number;
  rating: number;
  overview: string;
  budget: number;
  revenue: number;

  isFavoriteByUser: boolean;
  isPurchasedByUser: boolean;

  genres: Genre[];
  casts: Cast[];
}
