import { INewsPhoto } from 'src/app/shared/models/INewsPhoto';
import { IUserList } from 'src/app/shared/models/IUser';

export interface INewsForPublic{
    id:number;
    header:string;
    content:string;
    contentType:string;
    announceType:string;
    slideIntervalTime:number;
    slideId:string;
    created:Date;
    updated:Date;
    photoUrl:string;
    publishStartDate:Date;
    publishFinishDate:Date;
    newsDate:Date;
    newsAgency:string;
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    newsPhotos:INewsPhoto[];

}