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

export interface Role {
  id: number;
  name: string;
}

export enum Gender {
  male,
  female
}
