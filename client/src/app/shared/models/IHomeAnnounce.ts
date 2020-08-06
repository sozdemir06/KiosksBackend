import { IUserList } from './IUser';


export interface IHomeAnnounce{
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
    numberOfRoomId:number;
    heatingTypeId:number;
    flatOfHomeId:number;
    buildingAgeId:number;
    price:number;
    squareMeters:number;
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;

  

}