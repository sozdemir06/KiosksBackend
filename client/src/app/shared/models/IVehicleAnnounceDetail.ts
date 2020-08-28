import { IUserList } from './IUser';
import { IVehicleAnnouncePhoto } from './IVehicleAnnouncePhoto';
import { IVehicleAnnounceSubScreen } from './IVehicleAnnounceSubScreen';

export interface IVehicleAnnounceDetail {
  id: number;
  header: string;
  description: string;
  announceType: string;
  slideId: string;
  created: Date;
  updated: Date;
  photoUrl: string;
  publishStartDate: Date;
  publishFinishDate: Date;
  vehicleCategoryId: number;
  vehicleCategoryName:string;
  vehicleBrandId: number;
  vehicleBrandName:string;
  vehicleModelId: number;
  vehicleModelName:string;
  vehicleFuelTypeId: number;
  vehicleFuelTypeName:string;
  vehicleGearTypeId: number;
  vehicleGearTypeName:string;
  vehicleEngineSizeId: number;
  vehicleEngineSizeName:string;
  slideIntervalTime:number;
  price: number;
  userId: number;
  isNew: boolean;
  reject: boolean;
  isPublish: boolean;
  user: IUserList;
  vehicleAnnouncePhotos:IVehicleAnnouncePhoto[];
  vehicleAnnounceSubScreens:IVehicleAnnounceSubScreen[];
}
