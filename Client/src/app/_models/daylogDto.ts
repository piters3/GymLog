import { WorkoutDto } from './WorkoutDto';

export interface DaylogDto {
  id: number;
  date: Date;
  workouts: WorkoutDto[];
}
