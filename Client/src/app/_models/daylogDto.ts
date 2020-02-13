import { WorkoutDto } from './WorkoutDto';

export interface DaylogDto {
  date: Date;
  workouts: WorkoutDto[];
}
