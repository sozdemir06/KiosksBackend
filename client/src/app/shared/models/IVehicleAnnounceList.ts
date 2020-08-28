import { IUserList } from './IUser';


export interface IVehicleAnnounceList {
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
  vehicleBrandId: number;
  vehicleModelId: number;
  vehicleFuelTypeId?: number;
  vehicleGearTypeId: number;
  vehicleEngineSizeId: number;
  slideIntervalTime:number;
  price: number;
  userId: number;
  isNew: boolean;
  reject: boolean;
  isPublish: boolean;
  user: IUserList;
}
