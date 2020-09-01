// a map to the response token
export interface User {
  nameid: number;
  email: string;
  exp: string;
  alias: string;
  family_name: string;
  given_name: string;
  isAdmin: boolean;
  birthdate: Date;
  role: Array<string>;
}
