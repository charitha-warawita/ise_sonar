<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import ProjectDetailsModal from '@/components/modals/ProjectDetailsModal.vue';
import PageDetails from '@/components/PageDetails.vue';
import TargetAudienceList from '@/components/pages/project/TargetAudienceList.vue';
import { useProjectService } from '@/services/ProjectService';
import { useTargetAudienceService } from '@/services/TargetAudienceService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import type { Project } from '@/types/Project';
import type { TargetAudience } from '@/types/TargetAudience';
import { format } from 'date-fns';
import type { MenuItem } from 'primevue/menuitem';
import { useToast } from 'primevue/usetoast';
import { onMounted, ref } from 'vue';
import { useRoute } from 'vue-router';
import PricingBox from '../components/pages/project/PricingBox.vue';

const route = useRoute();
const breadcrumb = useBreadcrumbStore();
const toast = useToast();
const projectService = useProjectService();
const targetAudienceService = useTargetAudienceService();

const id = Number.parseInt(route.params.id as string);
const project = ref<Project | null>(null);
const visible = ref(false);

const EditProjectDetails = () => {
	if (!project.value) return;

	visible.value = true;
};

const UpdateProjectDetails = async (p: Project) => {
	if (!project.value) return;

	// TODO: API call to update record in DB.
	await projectService
		.UpdateAsync(p)
		.then(() => {
			if (project.value) {
				// TODO: Breadcrumb needs to update Name.

				project.value.Name = p.Name;
				project.value.Owner = p.Owner;
				project.value.MaconomyNumber = p.MaconomyNumber;
				project.value.StartDate = p.StartDate;
				project.value.EndDate = p.EndDate;
				project.value.LastActivity = new Date();
			}

			toast.add({
				severity: 'success',
				summary: 'Project Update',
				detail: 'Successfully updated the Project.',
				life: 3000,
			});
		})
		.catch(() => {
			toast.add({
				severity: 'error',
				summary: 'Project Update',
				detail: 'Failed to update the Project.',
				life: 3000,
			});
		});

	visible.value = false;
};

const TargetAudienceAdded = async (ta: TargetAudience) => {
	if (!project.value) return;

	project.value.TargetAudiences.push(ta);
};

onMounted(async () => {
	const proj = await projectService.GetAsync(id);
	if (!proj) return;

	project.value = proj;
	project.value.TargetAudiences = await targetAudienceService.GetAllAsync(project.value.Id);

	breadcrumb.$patch((state) => {
		const crumbs: MenuItem[] = [
			{
				label: project.value?.Name,
				to: route.path,
			},
		];

		state.items = crumbs;
	});
});
</script>

<template>
	<PageDetails>
		<div class="overview">
			<div class="overview-top-row">
				<div class="project-name">{{ project?.Name }}</div>
				<div class="project-edit-button">
					<PrimaryButton square label="Edit" @click="EditProjectDetails" />
				</div>
			</div>
			<div class="overview-content">
				<div>
					<span>Status: </span>
					<span></span>
				</div>
				<div>
					<span>Owner: </span>
					<span>{{ project?.Owner }}</span>
				</div>
				<div>
					<span>Type: </span>
					<span></span>
				</div>
				<div>
					<span>Start Date: </span>
					<span>{{ project?.StartDate ? format(project.StartDate, 'dd/MM/yyyy') : '' }}</span>
				</div>
				<div>
					<span>End Date: </span>
					<span>{{ project?.EndDate ? format(project.EndDate, 'dd/MM/yyyy') : '' }}</span>
				</div>
				<div class="progress-bar">
					<p>Progress</p>
				</div>
			</div>
		</div>
	</PageDetails>
	<div class="project-container">
		<div class="configuration-container">
			<div>
				<TargetAudienceList
					v-if="project"
					@created="TargetAudienceAdded"
					:project-id="id"
					:target-audiences="project.TargetAudiences"
				/>
			</div>
		</div>

		<div class="pricing-container">
			<PricingBox />
		</div>
	</div>
	<ProjectDetailsModal
		v-model:visible="visible"
		:project="project!"
		@submitted="UpdateProjectDetails"
		header="Edit Project"
	/>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.project-container {
	display: grid;
	grid-gap: 20px 20px;
	grid-template-columns: 4fr 1fr;

	@media screen and (max-width: $lg) {
		grid-template-columns: 2fr 1fr;
	}

	@media screen and (max-width: $md) {
		grid-template-columns: 1fr;
	}

	> div {
		width: 100%;
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

		@media screen and (max-width: $lg) {
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

.configuration-container {
	display: flex;
	flex-direction: column;
}

.pricing-container {
	min-width: 10rem;
	min-height: 18rem;

	@media screen and (min-width: $lg) {
		height: 20vh;
	}

	@media screen and (min-width: $xl) {
		height: 25vh;
	}
}
</style>
