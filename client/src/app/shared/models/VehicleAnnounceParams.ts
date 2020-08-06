export class VehicleAnnounceParams{
    pageIndex:number=1;
    pageSize:number=10;
    sort:string='header';
    search:string;
    screenId:number;
    subScreenId:number;
    reject:boolean;
    isPublish:boolean;
    isNew:boolean;
}