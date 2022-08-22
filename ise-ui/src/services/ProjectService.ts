// import type Project from '@/model/Project';
import { useProjectStore } from '@/stores/ProjectStore';
import { ProjectSchema, type Project } from '@/types/Project';
import { z } from 'zod';

// TODO: Implement API calls.
export function useProjectService() {
	const projectStore = useProjectStore();

	const GetAllAsync = async () => {
		const result = [...projectStore.projects] as Project[]; // TODO: Replace with API Call.
		if (result.length === 0) return result;

		const r = await z.array(ProjectSchema).safeParseAsync(result);
		return r.success ? r.data : Promise.reject(r.error);
	};

	const GetAsync = async (id: number) => {
		const result = projectStore.projects.find((p) => p.Id === id) ?? null; // TODO: Replace with API Call.
		if (!result) return null;

		const proj = Object.assign({}, result);

		const p = await ProjectSchema.safeParseAsync(proj);
		return p.success ? p.data : Promise.reject(p.error);
	};

	const CreateAsync = async (p: Project) => {
		const validation = await ProjectSchema.safeParseAsync(p);
		if (!validation.success) return Promise.reject(validation.error);

		p.Id = projectStore.projects.length + 1;
		const result = projectStore.projects.push(p); // TODO: Replace with API Call.
		return result;
	};

	const UpdateAsync = async (p: Project): Promise<void> => {
		const validation = await ProjectSchema.safeParseAsync(p);
		if (!validation.success) return Promise.reject(validation.error);

		const i = projectStore.projects.findIndex((x) => x.Id == p.Id); // TODO: Replace with API Call.
		if (i === -1) return Promise.reject('Project Not Found');

		p.LastActivity = new Date();
		projectStore.projects[i] = p;
	};

	return {
		GetAllAsync,
		GetAsync,
		CreateAsync,
		UpdateAsync,
	};
}
