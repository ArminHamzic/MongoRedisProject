import { Injectable } from '@angular/core';
import * as moment from 'moment';

@Injectable({
  providedIn: 'root',
})
export class UtilsService {

  public isNullOrUndefined(obj: any): boolean {
    if (typeof obj === 'undefined') {
      return true;
    }
    if (obj === undefined || obj === null) {
      return true;
    }
    return false;
  }

  public isNullOrEmpty(str: string): boolean {
    if (this.isNullOrUndefined(str) || str === '') {
      return true;
    }
    return false;
  }

  public isNullOrWhiteSpace(str: string): boolean {
    if (this.isNullOrUndefined(str) || this.isNullOrEmpty(str.trim())) {
      return true;
    }
    return false;
  }

  public parseRawDate(date: Date): moment.Moment {
    return moment(date);
  }

  public parseRawDateString(date: string): moment.Moment {
    return moment(date);
  }

  public formatRawDate(date: Date, withTime: boolean = false): string {
    return this.formatDate(this.parseRawDate(date), withTime);
  }

  public compareDatesForSort(date1: moment.Moment, date2: moment.Moment, ascending: boolean = true): number {
    const d1Val = date1.valueOf();
    const d2Val = date2.valueOf();
    if (ascending) {
      return d1Val - d2Val;
    }
    return d2Val - d1Val;
  }

  public formatDate(date: moment.Moment, withTime: boolean = false): string {
    const dateFormat = 'YYYY-MM-DD';
    const dateTimeFormat = 'YYYY-MM-DD HH:mm';
    const str = date.format(withTime ? dateTimeFormat : dateFormat);
    return str;
  }

}

export const MY_DATE_FORMATS = {
  parse: {
    dateInput: 'YYYY-MM-DD',
  },
  display: {
    dateInput: 'YYYY-MM-DD',
    monthYearLabel: 'MMM YYYY',
    dateA11yLabel: 'YYYY-MM-DD',
    monthYearA11yLabel: 'MMM YYYY',
  },
};
