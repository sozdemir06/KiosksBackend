import { IRoleCategory } from './IRoleCategory';

export interface IRole{
    id:number;
    name:string;
    roleCategoryId:number;
    description:string;
    created:Date;
    roleCategory:IRoleCategory;    
}