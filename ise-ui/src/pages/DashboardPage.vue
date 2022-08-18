<script setup lang="ts">
import ProjectsGrid from '@/components/pages/home/ProjectsGrid.vue';
import { useProjectService } from '@/services/ProjectService';
import type { Project } from '@/types/Project';
import { onMounted, ref } from 'vue';

const projectService = useProjectService();

const projects = ref<Project[]>([]);

onMounted(async () => {
	projects.value = await projectService.GetAllAsync();
});
</script>

<template>
	<div class="dashboard-container-grid">
		<div>
			<ProjectsGrid :projects="projects" />
		</div>
		<div>Open Risks</div>
		<div>Cards</div>
	</div>
</template>

<style scoped lang="scss">
.dashboard-container-grid {
	display: grid;
	grid-template-columns: 3fr 1fr;
}
</style>
