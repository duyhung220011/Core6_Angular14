import { Role } from './role';

export class User {
  // id: string;
  // userId: string;
  // title: string;
  // firstName: string;
  // lastName: string;
  // fullName: string;
  // email: string;
  // userName: string;
  // department:string;
  // userRole:string;
  // role: Role;
  // jwtToken?: string;
  // token?: string;
  userId: number;
  username: string;
  email: string;
  password: string;
  firstName: string;
  lastName: string;
  avatar: string;
  role: Role;
  token: string;
}
