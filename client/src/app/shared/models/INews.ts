import { INewsPhoto } from './INewsPhoto';
import { INewsSubScreen } from './INewsSubScreen';
import { IUserList } from './IUser';

export interface INews{
    id:number;
    header:string;
    description:string;
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
    newsSubScreens:INewsSubScreen[];
}