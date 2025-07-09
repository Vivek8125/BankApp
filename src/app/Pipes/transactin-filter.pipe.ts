import { Pipe, PipeTransform } from '@angular/core';
import { IGetTransaction } from '../Model/IGetTransaction';

@Pipe({
  name: 'transactinFilter'
})
export class TransactinFilterPipe implements PipeTransform {

  transform(value: IGetTransaction[] , filterString : string): IGetTransaction[] {
    if(filterString == "Debit"){
      return value.filter(x => x.type == "Debit")
    }
    else if(filterString == "Credit"){
      return value.filter(x => x.type == "Credit")
    }
    return value;
  }

}
