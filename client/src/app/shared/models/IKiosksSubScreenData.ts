import { IAnnounceForKiosks } from 'src/app/kiosks/models/IAnnounceForKiosks';
import { IVehicleAnnounceForKiosks } from 'src/app/kiosks/models/IVehicleAnnounceForKiosks';
import { IHomeAnnounceForKiosks } from 'src/app/kiosks/models/IHomeAnnounceForKiosks';
import { INewsForKiosks } from 'src/app/kiosks/models/INewsForKiosks';
import { IFoodMenuForKiosks } from 'src/app/kiosks/models/IFoodMenuForKiosks';
import { ILiveTvBroadCastForKiosks } from 'src/app/kiosks/models/ILiveTvBroadCastForKiosks';

export interface IKiosksSubScreenData {
    announces: IAnnounceForKiosks[];
    vehicleAnnounces: IVehicleAnnounceForKiosks[];
    homeAnnounces: IHomeAnnounceForKiosks[];
    news: INewsForKiosks[];
    foodsMenu: IFoodMenuForKiosks[];
    liveTvBroadCasts:ILiveTvBroadCastForKiosks[];
  }
  