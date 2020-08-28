import { IScreen } from 'src/app/shared/models/IScreen';
import { IAnnounce } from 'src/app/shared/models/IAnnounce';
import { IHomeAnnounce } from 'src/app/shared/models/IHomeAnnounce';
import { IAnnounceDetail } from 'src/app/shared/models/IAnnounceDetail';
import { IHomeAnnounceDetail } from 'src/app/shared/models/IHomeAnnounceDetail';
import { IVehicleAnnounceDetail } from 'src/app/shared/models/IVehicleAnnounceDetail';
import { INewsDetail } from 'src/app/shared/models/INewsDetail';
import { IFoodMenuDetail } from 'src/app/shared/models/IFoodMenuDetail';
import { IAnnounceForKiosks } from './IAnnounceForKiosks';
import { IHomeAnnounceForKiosks } from './IHomeAnnounceForKiosks';
import { IVehicleAnnounceForKiosks } from './IVehicleAnnounceForKiosks';
import { INewsForKiosks } from './INewsForKiosks';
import { IFoodMenuForKiosks } from './IFoodMenuForKiosks';

export interface IKiosks{
    screen:IScreen;
    announces:IAnnounceForKiosks[];
    homeAnnounces:IHomeAnnounceForKiosks[];
    vehicleAnnounces:IVehicleAnnounceForKiosks[];
    news:INewsForKiosks[];
    foodsMenu:IFoodMenuForKiosks[];
}