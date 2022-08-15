import type TargetAudience from '@/model/TargetAudience';
import { useTargetAudienceStore } from '@/stores/TargetAudienceStore';

export function useTargetAudienceService() {
	const targetAudienceStore = useTargetAudienceStore();

	const GetAllAsync = async (projectId: number): Promise<TargetAudience[]> => {
		const results = targetAudienceStore.targetAudiences.filter(
			(t: TargetAudience) => t.ProjectId === projectId
		);

		return JSON.parse(JSON.stringify(results)) as TargetAudience[];
	};

	const GetAsync = async (project: number, id: number): Promise<TargetAudience | null> => {
		const result = targetAudienceStore.targetAudiences.find(
			(ta) => ta.ProjectId === project && ta.Id === id
		);

		const value = result ? JSON.parse(JSON.stringify(result)) : null;
		return Promise.resolve(value);
	};

	const CreateAsync = async (targetAudience: TargetAudience): Promise<number> => {
		const id = targetAudienceStore.targetAudiences.length + 1;
		targetAudience.Id = id;
		targetAudienceStore.Add(targetAudience);

		return Promise.resolve(id);
	};

	return {
		GetAllAsync,
		GetAsync,
		CreateAsync,
	};
}
