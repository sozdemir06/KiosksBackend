import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { IUserList } from 'src/app/shared/models/IUser';

export interface IFoodMenuForPublic{
    id:number;
    content:string;
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
    foodMenuPhotos:IFoodMenuPhoto[];
   
}