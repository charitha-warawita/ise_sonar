import type TargetAudience from '@/model/TargetAudience';
import { defineStore } from 'pinia';

export const useTargetAudienceStore = defineStore('target-audience', () => {
	const targetAudiences: TargetAudience[] = [];

	function Add(ta: TargetAudience) {
		targetAudiences.push(ta);
	}

	return {
		targetAudiences,
		Add,
	};
});
