export interface Profile {
  id: number;
  userName: string;
  firstName: string;
  lastName: string;
  dateOfBirth: Date;
  email: string;
  phoneNumber: string;
  twoFactorEnabled: boolean;
  lockoutEndDate: Date;
  lastLoginDateTime: Date;
  roles: Role[];
}

export interface Role {
  id: number;
  name: string;
}
