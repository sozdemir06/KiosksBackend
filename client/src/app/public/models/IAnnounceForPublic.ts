import { IAnnouncePhoto } from 'src/app/shared/models/IAnnouncePhoto';
import { IUserList } from 'src/app/shared/models/IUser';

export interface IAnnounceForPublic{
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
}