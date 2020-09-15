import { ILiveTvBroadCastSubScreen } from 'src/app/shared/models/ILiveTvBroadCastSubScreen';
import { IUserList } from 'src/app/shared/models/IUser';

export interface ILiveTvBroadCastForKiosks{
    id:number;
    header:string;
    announceType:string;
    contentType:string;
    youtubeId:string;
    slideId:string;
    created:Date;
    updated:Date;
    publishStartDate:Date;
    publishFinishDate:Date;
    slideIntervalTime:number;
    userId:number;
    isNew:boolean;
    reject:boolean;
    isPublish:boolean;
    user:IUserList;
    liveTvBroadCastSubScreens:ILiveTvBroadCastSubScreen[];
}