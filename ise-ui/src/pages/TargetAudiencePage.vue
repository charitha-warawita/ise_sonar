<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import SecondaryButton from '@/components/buttons/SecondaryButton.vue';
import PageDetails from '@/components/PageDetails.vue';
import BasicSettings from '@/components/pages/project/target-audience/BasicSettings.vue';
import type Project from '@/model/Project';
import type TargetAudience from '@/model/TargetAudience';
import { useProjectService } from '@/services/ProjectService';
import { useTargetAudienceService } from '@/services/TargetAudienceService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore.js';
import { format } from 'date-fns';
import { useToast } from 'primevue/usetoast';
import { onMounted, ref, watch, type Ref } from 'vue';
import { useRoute, useRouter } from 'vue-router';
import TargetAudienceFeatureModal from '../components/modals/TargetAudienceFeatureModal.vue';

const route = useRoute();
const router = useRouter();
const toast = useToast();
const breadcrumbs = useBreadcrumbStore();
const projectService = useProjectService();
const targetAudienceService = useTargetAudienceService();

const projectId = Number.parseInt(route.params.project as string);
const audienceId = Number.parseInt(route.params.ta as string);

const name = ref('');
const hasChanged = ref(false);
const project: Ref<Project | null> = ref(null);
const audience: Ref<TargetAudience | null> = ref(null);
const featureModalVisible = ref(false);

watch(
	audience,
	() => {
		if (!project.value) return;
		hasChanged.value = true;
	},
	{
		deep: true,
	}
);

const Save = async (): Promise<void> => {
	if (!audience.value) return;

	await targetAudienceService.UpdateAsync(audience.value).then(() => {
		hasChanged.value = false;

		toast.add({
			severity: 'success',
			summary: 'Target Audience Update',
			detail: 'Successfully updated the Target Audience.',
			life: 3000,
		});

		// Do we want to redirect here?
		router.push({
			name: 'project',
			params: {
				id: audience.value?.ProjectId,
			},
		});
	});
};

const AddElement = () => {
	featureModalVisible.value = true;
};

const FeatureSaved = () => {
	featureModalVisible.value = false;
};

onMounted(async () => {
	audience.value = await targetAudienceService.GetAsync(projectId, audienceId);
	project.value = await projectService.GetAsync(projectId);

	if (!audience.value || !project.value) return;

	// TODO: Check for null audience/project, handle accordingly.
	name.value = audience.value.Name;

	if (!breadcrumbs.items.some((i) => i.label === 'Target Audience')) {
		breadcrumbs.$patch((state) => {
			state.items.push({
				label: audience.value?.Name,
			});
		});
	}
});
</script>

<template>
	<PageDetails>
		<div class="page-details">
			<div class="page-details-top-row">
				<div class="project-name">{{ project?.Name }} > {{ name }}</div>
			</div>
			<div class="page-details-content-row">
				<div class="page-details-content-start">
					<div class="detail-item">
						<span>Status: </span>
						<span></span>
					</div>
					<div class="detail-item">
						<span>Owner: </span>
						<span>{{ project?.Owner }}</span>
					</div>
					<div class="detail-item">
						<span>Type: </span>
						<span></span>
					</div>
					<div class="detail-item">
						<span>Start Date: </span>
						<span>{{ project?.StartDate ? format(project.StartDate, 'dd/MM/yyyy') : '' }}</span>
					</div>
					<div class="detail-item">
						<span>End Date: </span>
						<span>{{ project?.EndDate ? format(project.EndDate, 'dd/MM/yyyy') : '' }}</span>
					</div>
				</div>
				<div class="page-details-content-end">
					<div class="detail-button">
						<SecondaryButton square label="Add Element" @click="AddElement" />
					</div>
					<div class="detail-button">
						<PrimaryButton square :disabled="!hasChanged" label="Save" @click="Save" />
					</div>
				</div>
			</div>
		</div>
	</PageDetails>

	<div v-if="audience">
		<BasicSettings v-model:target-audience="audience" />
	</div>
	<div v-else></div>

	<TargetAudienceFeatureModal
		v-model:visible="featureModalVisible"
		@selected="FeatureSaved"
		@canceled="featureModalVisible = false"
	/>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.page-details {
	display: grid;
	padding: 10px 0;

	> .page-details-content-row {
		display: flex;

		@media screen and (max-width: $lg) {
			flex-direction: column;
		}

		> .page-details-content-start {
			flex-grow: 1;
			display: flex;

			@media screen and (max-width: $lg) {
				flex-direction: column;
			}

			> .detail-item {
				flex-grow: 1;

				> span:first-of-type {
					font-weight: bold;
				}
			}
		}

		> .page-details-content-end {
			display: flex;

			@media screen and (max-width: $lg) {
				flex-direction: column;
			}

			> div.detail-button:first-of-type {
				margin-right: 10px;

				@media screen and (max-width: $lg) {
					margin-right: 0;
					margin-bottom: 10px;
				}
			}
		}
	}
}
</style>
