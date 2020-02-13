export class Day {
  public dayNumber: number;
  public row: number;
  public column: number;
  public filled: boolean;

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
  public rowsNumber: number;
  public weeks: Week[] = [];

  constructor(date: Date, public filledDays: number[]) {
    this.daysInMonth = this.calculateDaysInMonth(date);
    this.rowsNumber = Math.ceil(this.daysInMonth / this.daysOfWeek.length);
    this.CreateMonth(date.getDay());
  }

  private calculateDaysInMonth(date: Date): number {
    return new Date(date.getFullYear(), date.getMonth() + 1, 0).getDate();
  }

  private CreateMonth(firstDayNumber: number) {
    let dayNumber = - firstDayNumber;

    for (let row = 0; row < this.rowsNumber; row++) {
      this.weeks.push(new Week());
      for (let column = 0; column < this.daysOfWeek.length; column++) {

        const newDay = new Day(dayNumber, row, column);

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


