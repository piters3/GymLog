export enum ExerciseType {
  strength,
  stretching,
  powerlifting,
  olympic
}

export enum Difficulty {
  beginner,
  intermediate,
  expert
}

export class Exercise {
  id: number;
  name: string;
  description: string;
  type: ExerciseType;
  difficulty: Difficulty;
  detailedMuscle: string;
  otherMuscles: string;
  equipmentName: string;
  mainMuscleName: string;
}
