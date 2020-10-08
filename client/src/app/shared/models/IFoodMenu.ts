import { IFoodMenuPhoto } from './IFoodMenuPhoto';
import { IFoodMenuSubScreen } from './IFoodMenuSubScreen';
import { IUserList } from './IUser';

export interface IFoodMenu{
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
    foodMenuSubScreens:IFoodMenuSubScreen[];
}