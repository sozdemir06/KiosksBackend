import { IUserList } from './IUser';
import { IHomeAnnouncePhoto } from './IHomeAnnouncePhoto';
import { IHomeAnnounceSubScreen } from './IHomeAnnounceSubScreen';

export interface IHomeAnnounceDetail{
    id:number;
    header:string;
    description:string;
    announceType:string;
    slideId:string;
    created:Date;
    updated:Date;
    photoUrl:string;
    publishStartDate:Date;
    publishFinishDate:Date;
    numberOfRoomName:string
    numberOfRoomId:number;
    heatingtypeName:string;
    heatingTypeId:number;
    flatOfHomeName:string;
    flatOfHomeId:number;
    buildingAgeName:string;
    buildingAgeId:number;
    price:number;
    squareMeters:number;
    slideIntervalTime:number;
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    homeAnnouncePhotos:IHomeAnnouncePhoto[];
    homeAnnounceSubScreens:IHomeAnnounceSubScreen[];
}