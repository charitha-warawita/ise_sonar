import type TargetAudience from '@/model/TargetAudience';
import { useTargetAudienceStore } from '@/stores/TargetAudienceStore';

export function useTargetAudienceService() {
	const targetAudienceStore = useTargetAudienceStore();

	const GetAllAsync = async (projectId: number): Promise<TargetAudience[]> => {
		const results = targetAudienceStore.targetAudiences.filter(
			(t: TargetAudience) => t.ProjectId === projectId
		);

		return JSON.parse(JSON.stringify(results)) as TargetAudience[];

		// return [...results] as TargetAudience[]; // Avoid returning references to persistent array.
	};

	const CreateAsync = async (targetAudience: TargetAudience): Promise<number> => {
		const id = targetAudienceStore.targetAudiences.length + 1;
		targetAudience.Id = id;
		targetAudienceStore.Add(targetAudience);

		return Promise.resolve(id);
	};

	return {
		GetAllAsync,
		CreateAsync,
	};
}
