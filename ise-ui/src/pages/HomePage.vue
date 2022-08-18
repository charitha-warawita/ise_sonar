<script async setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import ProjectDetailsModal from '@/components/modals/ProjectDetailsModal.vue';
import ProjectsGrid from '@/components/pages/home/ProjectsGrid.vue';
import { useProjectService } from '@/services/ProjectService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import type { Project } from '@/types/Project';
import type { DataTableRowSelectEvent } from 'primevue/datatable';
import InputText from 'primevue/inputtext';
import Panel from 'primevue/panel';
import { useToast } from 'primevue/usetoast';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const toast = useToast();
const projectService = useProjectService();
const breadcrumbs = useBreadcrumbStore();
breadcrumbs.$reset();

const projectFilter = ref<string | null>(null);
const projects = ref([] as Project[]);

const visible = ref(false);
const showModal = () => {
	visible.value = true;
};

const ProjectCreated = async (project: Project) => {
	visible.value = false;

	project.Id = await projectService.CreateAsync(project);
	await LoadProjects(); // TODO: Can we just push to projects ref or do we need to hit API? Would need to at least update the Total.

	toast.add({
		severity: 'success',
		summary: 'New Project Created',
		detail: 'Successfully created a new project.',
		life: 3000,
	});
};

const ProjectSelected = (e: DataTableRowSelectEvent) => {
	router.push({
		name: 'project',
		params: {
			id: e.data.Id,
		},
	});
};

const LoadProjects = async () => {
	projects.value = await projectService.GetAllAsync();
};

onMounted(async () => {
	await LoadProjects();
});
</script>

<template>
	<div class="home-container">
		<div class="search-box">
			<InputText
				class="project-filter-input"
				id="search"
				type="text"
				v-model="projectFilter"
				placeholder="Search"
			/>
		</div>
		<div class="button-cell">
			<PrimaryButton square label="Create Project" @click="showModal" />
		</div>
		<div class="project-row">
			<Panel>
				<template #header>
					<span>
						<b style="font-weight: 600">
							Projects <span style="color: lightskyblue">{{ projects.length }}</span>
						</b>
					</span>
				</template>

				<ProjectsGrid :projects="projects" dataKey="Id" @row-select="ProjectSelected" />
			</Panel>
		</div>
	</div>
	<ProjectDetailsModal
		v-model:visible="visible"
		@submitted="ProjectCreated"
		header="Create Project"
	/>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;
@use '@/assets/mixins.scss';

.home-container {
	display: grid;
	grid-template-columns: 4fr 1fr;
	grid-row-gap: 30px;

	@media screen and (max-width: $md) {
		grid-template-columns: 1fr;
	}
}

.project-row {
	@media screen and (min-width: $sm) {
		grid-column: span 2;
	}
}

.button-cell {
	width: 100%;

	/* When sm, put create button at top of grid. */
	@media screen and (max-width: $sm) {
		grid-row-start: 0;
		grid-row-end: 1;
	}

	@media screen and (min-width: $sm) {
		justify-self: end;
		min-width: 150px;
		max-width: 250px;
	}
}

/* Panel */
.p-panel :deep(.p-panel-header) {
	@include mixins.shadowed();

	border: none;
	z-index: 1;
	background-color: var(--color-background);
	border-top-left-radius: $border-radius;
	border-top-right-radius: $border-radius;
}

.p-panel :deep(.p-panel-content) {
	@include mixins.shadowed();

	border: none;
	border-bottom-left-radius: $border-radius;
	border-bottom-right-radius: $border-radius;
}

.project-filter-input {
	background: var(--blue-100);
	border: none;

	/* Greater than md */
	@media screen and (min-width: $md) {
		max-width: 535px;
		width: 65%;
	}

	/* Less than md */
	@media screen and (max-width: $md) {
		width: 90%;
	}

	/* Less than sm */
	@media screen and (max-width: $sm) {
		width: 100%;
	}

	& + label {
		color: var(--blue-500);
	}
}
</style>
