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

	const UpdateAsync = async (targetAudience: TargetAudience): Promise<void> => {
		const i = targetAudienceStore.targetAudiences.findIndex(
			(ta) => ta.ProjectId === targetAudience.ProjectId && ta.Id === targetAudience.Id
		);
		if (i === -1) return Promise.reject('Target Audience does not exist.');
		targetAudienceStore.targetAudiences[i] = targetAudience;
		return Promise.resolve();
	};

	return {
		GetAllAsync,
		GetAsync,
		CreateAsync,
		UpdateAsync,
	};
}
