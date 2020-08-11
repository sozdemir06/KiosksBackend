import { IUserList } from './IUser';
import { IAnnouncePhoto } from './IAnnouncePhoto';
import { IAnnounceSubScreen } from './IAnnounceSubScreen';

export interface IAnnounceDetail{
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
    announcePhotos:IAnnouncePhoto[];
    announceSubScreens:IAnnounceSubScreen[];
}