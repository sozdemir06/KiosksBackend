import { ISubScreen } from './ISubScreen';
import { IScreenHeader } from './IScreenHeader';
import { IScreenFooter } from './IScreenFooter';
import { IScreenHeaderPhoto } from './IScreenHeaderPhoto';

export interface IScreen{
    id:number;
    name:string;
    position:string;
    isFull:boolean;
    screenHeaders:IScreenHeader;
    screenFooters:IScreenFooter;
    subScreens:ISubScreen[];
    screenHeaderPhotos:IScreenHeaderPhoto[];

}