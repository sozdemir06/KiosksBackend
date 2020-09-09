import { ISubScreen } from 'src/app/shared/models/ISubScreen';
import { IScreenHeader } from 'src/app/shared/models/IScreenHeader';
import { IScreenFooter } from 'src/app/shared/models/IScreenFooter';
import { IScreenHeaderPhoto } from 'src/app/shared/models/IScreenHeaderPhoto';

export interface IScreenForKiosks{
    id:number;
    name:string;
    position:string;
    isFull:boolean;
    screenHeaders:IScreenHeader;
    screenFooters:IScreenFooter;
    subScreens:ISubScreen[];
    screenHeaderPhotos:IScreenHeaderPhoto[];
}