import { User } from "app/auth/models";
import { Sort } from "./sort";

export class UserPage {
    content:          User[];
    totalPages:       number;
    last:             boolean;
    totalElements:    number;
    size:             number;
    number:           number;
    sort:             Sort;
    numberOfElements: number;
    first:            boolean;
    empty:            boolean;
}
