import { SetDto } from './setDto';

export class WorkoutDto {
  exerciseId: number;
  exerciseName: string;
  sets: SetDto[];
}
