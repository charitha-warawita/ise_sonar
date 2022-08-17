import { useTargetAudienceStore } from '@/stores/TargetAudienceStore';
import { TargetAudienceSchema, type TargetAudience } from '@/types/TargetAudience';
import { z } from 'zod';

export function useTargetAudienceService() {
	const targetAudienceStore = useTargetAudienceStore();

	const GetAllAsync = async (projectId: number): Promise<TargetAudience[]> => {
		// TODO: Replace with API call.
		const results = targetAudienceStore.targetAudiences.filter(
			(t: TargetAudience) => t.ProjectId === projectId
		);
		if (results.length === 0) return results;

		const res = JSON.parse(JSON.stringify(results)); // TODO: Remove.

		const t = await z.array(TargetAudienceSchema).safeParseAsync(res);
		return t.success ? t.data : Promise.reject(t.error);
	};

	const GetAsync = async (project: number, id: number): Promise<TargetAudience | null> => {
		// TODO: Replace with API call.
		const result = targetAudienceStore.targetAudiences.find(
			(ta) => ta.ProjectId === project && ta.Id === id
		);
		if (!result) return null;

		const value = JSON.parse(JSON.stringify(result)); // TODO: Remove.
		const validation = await TargetAudienceSchema.safeParseAsync(value);

		return validation.success ? validation.data : Promise.reject(validation.error);
	};

	const CreateAsync = async (targetAudience: TargetAudience): Promise<number> => {
		const validation = await TargetAudienceSchema.safeParseAsync(targetAudience);
		if (!validation.success) return Promise.reject(validation.error);

		// TODO: Replace with API Call.
		const id = targetAudienceStore.targetAudiences.length + 1;
		targetAudience.Id = id;
		targetAudienceStore.targetAudiences.push(targetAudience);

		return Promise.resolve(id);
	};

	const UpdateAsync = async (targetAudience: TargetAudience): Promise<void> => {
		const validation = await TargetAudienceSchema.safeParseAsync(targetAudience);
		if (!validation.success) return Promise.reject(validation.error);

		// TODO: Replace with API call.
		const i = targetAudienceStore.targetAudiences.findIndex(
			(ta) => ta.ProjectId === targetAudience.ProjectId && ta.Id === targetAudience.Id
		);
		if (i === -1) return Promise.reject('Target Audience does not exist.');

		targetAudienceStore.targetAudiences[i] = validation.data;
	};

	return {
		GetAllAsync,
		GetAsync,
		CreateAsync,
		UpdateAsync,
	};
}
