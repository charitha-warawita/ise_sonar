import { ProjectStatusEnum } from '@/types/enums/ProjectStatus';
import type { Project } from '@/types/Project';
import { defineStore } from 'pinia';

export const useProjectStore = defineStore('projects', () => {
	const projects: Project[] = [
		{
			Id: 1,
			Status: ProjectStatusEnum.enum.Completed,
			Name: 'An Example Completed Project',
			MaconomyNumber: '102038475',
			Owner: 'Doe, John',
			StartDate: new Date(2021, 11, 20, 14, 30),
			EndDate: new Date(2022, 2, 15),
			CreationDate: new Date(2021, 11, 19, 9),
			LastActivity: new Date(2021, 11, 19, 9, 30),
			TargetAudiences: [],
		} as Project,
		{
			Id: 2,
			Status: ProjectStatusEnum.enum.Draft,
			Name: 'An Example Open Project',
			MaconomyNumber: '102038475',
			Owner: 'Doe, John',
			StartDate: new Date(2022, 3, 20, 9),
			EndDate: new Date(2025, 4, 15),
			CreationDate: new Date(2022, 3, 10, 15),
			LastActivity: new Date(2022, 3, 15, 13),
			TargetAudiences: [],
		} as Project,
	];

	return {
		projects,
	};
});
