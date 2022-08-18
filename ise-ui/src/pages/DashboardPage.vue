<script setup lang="ts">
import OpenRisksGrid from '@/components/grids/OpenRisksGrid.vue';
import ProjectsGrid from '@/components/grids/ProjectsGrid.vue';
import Panel from '@/components/Panel.vue';
import { useProjectService } from '@/services/ProjectService';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import { ProjectStatusEnum } from '@/types/enums/ProjectStatus';
import type { Project } from '@/types/Project';
import type { DataTableRowSelectEvent } from 'primevue/datatable';
import { onMounted, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const breadcrumb = useBreadcrumbStore();
const projectService = useProjectService();

const projects = ref<Project[]>([]);
const risks = ref<Project[]>([]);

const ProjectSelected = (e: DataTableRowSelectEvent) => {
	router.push({
		name: 'project',
		params: {
			id: e.data.Id,
		},
	});
};
onMounted(async () => {
	breadcrumb.$reset();

	projects.value = await projectService.GetAllAsync();
	risks.value = await projectService.GetAllByStatusAsync([ProjectStatusEnum.Enum.Started]);
});
</script>

<template>
	<div class="dashboard-container-grid">
		<div>
			<Panel>
				<template #header>
					<span style="font-weight: 600">
						Projects <span style="color: lightskyblue">{{ projects.length }}</span>
					</span>
				</template>

				<ProjectsGrid :projects="projects" @row-select="ProjectSelected" />
			</Panel>
		</div>
		<div>
			<Panel>
				<template #header>
					<span style="font-weight: 600">
						Open Risks <span style="color: lightskyblue">{{ risks.length }}</span>
					</span>
				</template>

				<OpenRisksGrid :risks="risks" @row-select="ProjectSelected" />
			</Panel>
		</div>
		<div>Cards</div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.dashboard-container-grid {
	display: grid;
	grid-template-columns: 3fr 1fr;
	gap: 20px;

	@media screen and (max-width: $xl) {
		grid-template-columns: 1fr;
	}
}
</style>
