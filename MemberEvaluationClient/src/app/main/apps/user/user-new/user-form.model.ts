import { Role } from "app/main/model/role";

export class UserForm {
    title: string;
    firstName: string;
    lastName: string;
    userName: string;
    email: string;
    password: string;
    confirmPassword: string;
    department:string;
    role: Role;
    userRole:string;
}
