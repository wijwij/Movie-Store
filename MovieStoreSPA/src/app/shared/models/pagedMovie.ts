import { Movie } from './movie';

export interface PagedMovie {
  pageIndex: number;
  pageSize: number;
  totalPages: number;
  totalCount: number;
  hasPreviousPage: boolean;
  hasNextPage: boolean;
  data: Movie[];
}
