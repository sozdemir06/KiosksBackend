import { IUserList } from 'src/app/shared/models/IUser';
import { IFoodMenuPhoto } from 'src/app/shared/models/IFoodMenuPhoto';
import { IFoodMenuSubScreen } from 'src/app/shared/models/IFoodMenuSubScreen';

export interface IFoodMenuForKiosks{
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