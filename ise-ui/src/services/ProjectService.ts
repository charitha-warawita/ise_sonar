import type Project from '@/model/Project';
import { useProjectStore } from '@/stores/ProjectStore';

// NOTE: Does this need to be a composable? Nothing reactive, but we'll be using a store.
// TODO: Implement API calls.
export function useProjectService() {
	const projectStore = useProjectStore();

	const GetAllAsync = async (): Promise<Project[]> => {
		const result = [...projectStore.projects] as Project[];

		return Promise.resolve(result);
	};

	const GetAsync = async (id: number): Promise<Project | null> => {
		const result = projectStore.projects.find((p) => p.Id === id) ?? null;

		const p = Object.assign({}, result) as Project;

		return Promise.resolve(p);
	};

	const GetByNameAsync = async (filter: string): Promise<Project[]> => {
		const result = projectStore.projects.filter((p) => p.Name.includes(filter));

		return Promise.resolve(result);
	};

	const CreateAsync = async (p: Project): Promise<number> => {
		const result = projectStore.projects.push(p);

		return Promise.resolve(result);
	};

	const UpdateAsync = async (p: Project): Promise<void> => {
		if (p.Id === -1) return Promise.reject('Project does not have a valid Id.');

		const i = projectStore.projects.findIndex((x) => x.Id == p.Id);
		if (i === -1) return Promise.reject('Project Not Found');

		const proj = projectStore.projects[i];
		proj.Name = p.Name;
		proj.MaconomyNumber = p.MaconomyNumber;
		proj.Owner = p.Owner;
		proj.StartDate = p.StartDate;
		proj.EndDate = p.EndDate;
		proj.LastActivity = new Date();

		return Promise.resolve();
	};

	return {
		GetAllAsync,
		GetAsync,
		GetByNameAsync,
		CreateAsync,
		UpdateAsync,
	};
}
