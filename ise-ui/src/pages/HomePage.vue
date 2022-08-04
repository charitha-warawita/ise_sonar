<script async setup lang="ts">
import { onMounted, ref, type Ref } from 'vue';
import { useRouter } from 'vue-router';

import Panel from 'primevue/panel';
import InputText from 'primevue/inputtext';
import type { DataTableRowSelectEvent } from 'primevue/datatable';

import ProjectsGrid from '../components/pages/home/ProjectsGrid.vue';
import PrimaryButton from '../components/buttons/PrimaryButton.vue';
import CreateProjectModal from '@/components/modals/CreateProjectModal.vue';

import ProjectService from '@/services/ProjectService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import type Project from '@/model/Project';

const router = useRouter();
const breadcrumbs = useBreadcrumbStore();
breadcrumbs.$reset();

const projectFilter: Ref<string | null> = ref(null);
const projects: Ref<Project[]> = ref([]);

onMounted(async () => {
	projects.value = await ProjectService.GetProjectsAsync();
});

const visible: Ref<boolean> = ref(false);
const showModal = () => {
	visible.value = true;
};

const ProjectCreated = (project: Project) => {
	// TODO: Refresh grid.
	projects.value.push(project);
};

const ProjectSelected = (e: DataTableRowSelectEvent) => {
	router.push({
		name: 'project',
		params: {
			id: e.data.Id,
		},
	});
};
</script>

<!-- Should we refactor Project filter, Project Grid and Project Modal into a separate comp? -->
<template>
	<div class="home-container">
		<div class="search-box">
			<span class="p-float-label">
				<InputText class="project-filter-input" id="search" type="text" v-model="projectFilter" />
				<label class="project-filter-input-label" for="search">Search</label>
			</span>
		</div>
		<div class="button-cell">
			<PrimaryButton square label="Create Project" @click="showModal" />
		</div>
		<div class="project-row">
			<Panel :value="projects">
				<template #header>
					<span>
						<b style="font-weight: 600">
							Projects <span style="color: lightskyblue">{{ projects.length }}</span>
						</b>
					</span>
				</template>

				<ProjectsGrid :value="projects" @row-select="ProjectSelected" />
			</Panel>
		</div>
	</div>
	<CreateProjectModal v-model:visible="visible" @created="ProjectCreated" />
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
}

.project-filter-input-label {
	color: var(--blue-500);
}
</style>
