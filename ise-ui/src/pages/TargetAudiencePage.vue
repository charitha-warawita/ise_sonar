<script setup lang="ts">
import type TargetAudience from '@/model/TargetAudience';
import { useTargetAudienceService } from '@/services/TargetAudienceService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore.js';
import { onMounted, ref, type Ref } from 'vue';
import { useRoute } from 'vue-router';
import BasicSettings from '../components/pages/project/target-audience/BasicSettings.vue';

const route = useRoute();
const breadcrumbs = useBreadcrumbStore();
const targetAudienceService = useTargetAudienceService();

const project = Number.parseInt(route.params.project as string);
const audienceId = Number.parseInt(route.params.ta as string);

const audience: Ref<TargetAudience | null> = ref(null);

onMounted(async () => {
	audience.value = await targetAudienceService.GetAsync(project, audienceId);

	breadcrumbs.$patch((state) => {
		state.items.push({
			label: 'Target Adudience', // NOTE: Should this be dynamic to the TA name?
		});
	});
});
</script>

<template>
	<div v-if="audience">
		<BasicSettings :target-audience="audience" />
	</div>
	<div v-else></div>
</template>

<style scoped></style>
