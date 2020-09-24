import { IAnnounceForPublic } from './IAnnounceForPublic';
import { IFoodMenuForPublic } from './IFoodMenuForPublic';
import { IHomeAnnounceForPublic } from './IHomeAnnounceForPublic';
import { INewsForPublic } from './INewsForPublic';
import { IVehicleAnnounceForPublic } from './IVehicleAnnounceForPublic';


export interface IPublic{
    announces:IAnnounceForPublic[];
    homeAnnounces:IHomeAnnounceForPublic[];
    vehicleAnnounces:IVehicleAnnounceForPublic[];
    news:INewsForPublic[];
    foodsMenu:IFoodMenuForPublic[];  
}