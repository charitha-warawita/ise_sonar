import type { Project } from '@/types/Project';
import { defineStore } from 'pinia';

export const useProjectStore = defineStore('projects', () => {
	const projects: Project[] = [
		{
			Id: 1,
			Name: 'An Example Project',
			MaconomyNumber: '102038475',
			Owner: 'Doe, John',
			StartDate: new Date(2021, 11, 20, 14, 30),
			EndDate: new Date(2022, 2, 15),
			CreationDate: new Date(2021, 11, 19, 9),
			LastActivity: new Date(2021, 11, 19, 9, 30),
			TargetAudiences: [],
		} as Project,
	];

	return {
		projects,
	};
});
