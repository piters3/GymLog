import * as moment from 'moment';

export class Day {
  public dayNumber: number;
  public row: number;
  public column: number;
  public filled: boolean;
  public displayValue: string;
  public date: string;

  constructor(dayNumber: number, row: number, column: number) {
    this.dayNumber = dayNumber;
    this.row = row;
    this.column = column;
  }
}

export class Week {
  public days: Day[] = [];
}

export class Month {
  public daysOfWeek = ['Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday', 'Sunday'];
  public daysInMonth: number;
  public rowsCount: number;
  public weeks: Week[] = [];

  constructor(selectedDate: Date, public filledDays: number[]) {
    const previousMonthDays = this.correctSunday(selectedDate.getDay());
    this.daysInMonth = this.calculateDaysInMonth(selectedDate);
    this.rowsCount = Math.ceil((this.daysInMonth + previousMonthDays - 1) / this.daysOfWeek.length);
    this.CreateMonth(previousMonthDays, selectedDate);
  }

  private correctSunday(dayNumber: number): number {
    if (dayNumber === 0)
      return 7;
    return dayNumber;
  }

  private calculateDaysInMonth(date: Date): number {
    return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
  }

  private CreateMonth(firstDayNumber: number, selectedDate: Date) {
    let dayNumber = 2 - firstDayNumber;

    for (let row = 0; row < this.rowsCount; row++) {
      this.weeks.push(new Week());
      for (let column = 0; column < this.daysOfWeek.length; column++) {
        const newDay = new Day(dayNumber, row, column);

        if (dayNumber > 0) {
          newDay.displayValue = dayNumber.toString();
          newDay.date = moment(new Date(selectedDate.getFullYear(), selectedDate.getMonth(), dayNumber)).format('YYYY-MM-DD');
        } else {
          newDay.displayValue = '-';
        }

        if (this.filledDays.includes(dayNumber))
          newDay.filled = true;

        this.weeks[row].days.push(newDay);
        dayNumber++;

        if (dayNumber > this.daysInMonth)
          break;
      }
    }
  }
}


