import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'shorten'
})

export class ShortenPipe implements PipeTransform {
    transform(value: any, ...args: any[]): any {
        if(value && value.length>18){
            return value.substr(0,18)+"....";
        }
        return value;
    }
}