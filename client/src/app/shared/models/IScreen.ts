import { ISubScreen } from './ISubScreen';

export interface IScreen{
    id:number;
    name:string;
    position:string;
    isFull:boolean;
    subScreens:ISubScreen[];

}