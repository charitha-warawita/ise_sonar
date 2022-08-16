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
import { onMounted, ref, type Ref } from 'vue';
import { useRoute } from 'vue-router';

const route = useRoute();
const breadcrumbs = useBreadcrumbStore();
const projectService = useProjectService();
const targetAudienceService = useTargetAudienceService();

const projectId = Number.parseInt(route.params.project as string);
const audienceId = Number.parseInt(route.params.ta as string);

const project: Ref<Project | null> = ref(null);
const audience: Ref<TargetAudience | null> = ref(null);

const Save = (): void => {};

const AddElement = (): void => {};

onMounted(async () => {
	audience.value = await targetAudienceService.GetAsync(projectId, audienceId);
	project.value = await projectService.GetAsync(projectId);

	if (!breadcrumbs.items.some((i) => i.label === 'Target Audience')) {
		breadcrumbs.$patch((state) => {
			state.items.push({
				label: 'Target Audience', // NOTE: Should this be dynamic to the TA name?
			});
		});
	}
});
</script>

<template>
	<PageDetails>
		<div class="page-details">
			<div class="page-details-top-row">
				<div class="project-name">{{ project?.Name }} > {{ audience?.Name }}</div>
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
						<PrimaryButton square label="Save" @click="Save" />
					</div>
				</div>
			</div>
		</div>
	</PageDetails>

	<div v-if="audience">
		<BasicSettings :target-audience="audience" />
	</div>
	<div v-else></div>
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

.overview {
	display: grid;
	grid-gap: 10px;
	padding: 10px 0;

	.overview-top-row {
		display: flex;

		.project-name {
			font-weight: bold;
			flex-grow: 1;
		}
	}

	.overview-content {
		display: flex;

		@media screen and (max-length: $lg) {
			flex-direction: column;
		}

		> div {
			flex-grow: 1;

			> span:first-of-type {
				font-weight: bold;
			}
		}

		& > .progress-bar {
			flex-grow: 3;
			text-align: end;
		}
	}
}
</style>
