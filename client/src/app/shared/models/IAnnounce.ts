import { IAnnouncePhoto } from './IAnnouncePhoto';
import { IAnnounceSubScreen } from './IAnnounceSubScreen';
import { IUserList } from './IUser';

export interface IAnnounce{
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
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    announcePhotos:IAnnouncePhoto[];
    announceSubScreens:IAnnounceSubScreen[];
}