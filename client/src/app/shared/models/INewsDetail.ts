import { IUserList } from './IUser';
import { INewsPhoto } from './INewsPhoto';
import { INewsSubScreen } from './INewsSubScreen';

export interface INewsDetail{
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
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    newsPhotos:INewsPhoto[];
    newsSubScreens:INewsSubScreen[];
}