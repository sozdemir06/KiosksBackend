import { IAnnounceForKiosks } from './IAnnounceForKiosks';
import { IHomeAnnounceForKiosks } from './IHomeAnnounceForKiosks';
import { IVehicleAnnounceForKiosks } from './IVehicleAnnounceForKiosks';
import { INewsForKiosks } from './INewsForKiosks';
import { IFoodMenuForKiosks } from './IFoodMenuForKiosks';
import { IScreenForKiosks } from './IScreenForKiosks';
import { ILiveTvBroadCastForKiosks } from './ILiveTvBroadCastForKiosks';

export interface IKiosks{
    screen:IScreenForKiosks;
    announces:IAnnounceForKiosks[];
    homeAnnounces:IHomeAnnounceForKiosks[];
    vehicleAnnounces:IVehicleAnnounceForKiosks[];
    news:INewsForKiosks[];
    foodsMenu:IFoodMenuForKiosks[];
    liveTvBroadCasts:ILiveTvBroadCastForKiosks[];
}