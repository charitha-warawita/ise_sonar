import type { TargetAudience } from '@/types/TargetAudience';
import { defineStore } from 'pinia';

export const useTargetAudienceStore = defineStore('target-audience', () => {
	const targetAudiences: TargetAudience[] = [
		{
			Id: 1,
			ProjectId: 1,
			Name: 'An Example Target Audience',
			EstimatedIR: 40,
			EstimatedLOI: 10,
			Limit: 500,
		} as TargetAudience,
	];

	return {
		targetAudiences,
	};
});
