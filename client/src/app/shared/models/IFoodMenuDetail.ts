import { IUserList } from './IUser';
import { IFoodMenuPhoto } from './IFoodMenuPhoto';
import { IFoodMenuSubScreen } from './IFoodMenuSubScreen';

export interface IFoodMenuDetail{
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