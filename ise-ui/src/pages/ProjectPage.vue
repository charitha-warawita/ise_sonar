<script setup lang="ts">
import { type Ref, ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import { format } from 'date-fns';
import Pricing from '../components/pages/project/Pricing.vue';
import type Project from '@/model/Project';
import TargetAudienceList from '../components/pages/project/TargetAudienceList.vue';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import type { MenuItem } from 'primevue/menuitem';
import PageDetails from '../components/PageDetails.vue';
import ProjectService from '@/services/ProjectService';
import PrimaryButton from '../components/buttons/PrimaryButton.vue';
import ProjectDetailsModal from '../components/modals/ProjectDetailsModal.vue';
import { useToast } from 'primevue/usetoast';

const route = useRoute();
const breadcrumb = useBreadcrumbStore();
const toast = useToast();
const project: Ref<Project | null> = ref(null);
const id = Number.parseInt(route.params.id as string);

const visible: Ref<boolean> = ref(false);
const EditProjectDetails = (): void => {
	if (!project.value) return;

	visible.value = true;
};

const UpdateProjectDetails = (p: Project): void => {
	if (!project.value) return;

	project.value.Name = p.Name;
	project.value.MaconomyNumber = p.MaconomyNumber;
	project.value.Owner = p.Owner;
	project.value.StartDate = p.StartDate;
	project.value.EndDate = p.EndDate;
	project.value.LastActivity = p.LastActivity;

	// TODO: API call to update record in DB.
	toast.add({
		severity: 'success',
		summary: 'Project Updated',
		detail: 'Successfully updated the Project.',
		life: 3000,
	});

	visible.value = false;
};

onMounted(async () => {
	project.value = await ProjectService.GetAsync(id);

	if (!project.value) return;

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
					v-model:target-audiences="project.TargetAudiences"
					:project-id="id"
				/>
			</div>
		</div>

		<div class="pricing-container">
			<Pricing />
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
