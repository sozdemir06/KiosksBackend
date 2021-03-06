import { ICampus } from './ICampus';
import { IDepartment } from './IDepartment';
import { IDegree } from './IDegree';
import { IUserPhoto } from './IUserPhoto';

export interface IUser{
    userId:number;
    firstName:string;
    lastName:string;
    email:string;
    photoUrl:string;
    token:string;
    expiration:Date;
}


export interface IUserList{
    id:number;
    firstName:string;
    lastName:string;
    email:string;
    interPhone:string;
    gsmPhone:string;
    isActive:boolean;
    created:Date;
    degreeId:number;
    campus:ICampus;
    department:IDepartment;
    degree:IDegree;
    photoUrl:string;
    userPhotos:IUserPhoto[];
}