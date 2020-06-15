import { ICampus } from './ICampus';
import { IDepartment } from './IDepartment';

export interface IUser{
    userId:number;
    firstName:string;
    lastName:string;
    email:string;
    token:string;
    expiration:Date;
}


export interface IUserList{
    userId:number;
    firstName:string;
    lastName:string;
    email:string;
    interPhone:string;
    gsmPhone:string;
    isActive:boolean;
    created:Date;
    campus:ICampus;
    department:IDepartment;
}