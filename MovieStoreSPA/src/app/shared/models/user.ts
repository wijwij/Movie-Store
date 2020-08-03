import { Movie } from './movie';

export interface User {
  id: number;
  email: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  roles: string[];
}
