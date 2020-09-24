import { IHomeAnnouncePhoto } from 'src/app/shared/models/IHomeAnnouncePhoto';
import { IUserList } from 'src/app/shared/models/IUser';

export interface IHomeAnnounceForPublic{
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
    slideIntervalTime:number;
    price:number;
    squareMeters:number;
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    homeAnnouncePhotos:IHomeAnnouncePhoto[];
  
}