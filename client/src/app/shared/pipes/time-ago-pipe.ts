import { Pipe, PipeTransform } from '@angular/core';
import { formatDistance,parseISO} from "date-fns";
import localetr from "date-fns/locale/tr";
@Pipe({
    name: 'timeAgo'
})

export class TimeAgoPipe implements PipeTransform {
    transform(value: any, ...args: any[]): any {
        if(value){
            let result=formatDistance(parseISO(value),new Date(),{
                locale:localetr,
                addSuffix:true

            });
            return result;
        }

        return value;
    }
}