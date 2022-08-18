<script setup lang="ts">
import ProjectsGrid from '@/components/pages/home/ProjectsGrid.vue';
import Panel from '@/components/Panel.vue';
import { useProjectService } from '@/services/ProjectService';
import type { Project } from '@/types/Project';
import { onMounted, ref } from 'vue';

const projectService = useProjectService();

const projects = ref<Project[]>([]);
const risks = ref<Project[]>([]);

onMounted(async () => {
	projects.value = await projectService.GetAllAsync();
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

				<ProjectsGrid :projects="projects" />
			</Panel>
		</div>
		<div>
			<Panel>
				<template #header>
					<span style="font-weight: 600">
						Open Risks <span style="color: lightskyblue">{{ risks.length }}</span>
					</span>
				</template>
			</Panel>
		</div>
		<div>Cards</div>
	</div>
</template>

<style scoped lang="scss">
.dashboard-container-grid {
	display: grid;
	grid-template-columns: 3fr 1fr;
	gap: 20px;
}
</style>
