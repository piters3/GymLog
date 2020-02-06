import { Role } from './role';

export interface User {
  id: number;
  name: string;
  userName: string;
  surname: string;
  email: string;
  enabled: boolean;
  gender: Gender;
  weight: number;
  height: number;
  registerDate: Date;
  roles?: Role[];
}

export enum Gender {
  male,
  female
}
