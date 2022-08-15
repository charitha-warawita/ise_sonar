import type Project from '@/model/Project';
import { defineStore } from 'pinia';

export const useProjectStore = defineStore('projects', () => {
	const projects: Project[] = [];

	return {
		projects,
	};
});
