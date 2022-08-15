<script setup lang="ts">
import type Project from '@/model/Project.js';
import { format } from 'date-fns';
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';
import { computed } from 'vue';

const props = defineProps({
	projects: {
		type: Array<Project>,
		required: true,
	},
});

const selectable = computed(() => props.projects.length > 0);
</script>

<template>
	<DataTable
		:value="props.projects"
		class="project-table"
		:selection-mode="selectable ? 'single' : undefined"
	>
		<Column field="Id" header="Project Id"></Column>
		<Column field="Name" header="Project Name"></Column>
		<Column field="CreationDate" header="Creation Date">
			<template #body="slotProps">
				{{ format(slotProps.data.CreationDate, 'dd/MM/yyyy') }}
			</template>
		</Column>
		<Column field="LastActivity" header="Last Activity">
			<template #body="slotProps">
				{{ format(slotProps.data.LastActivity, 'dd/MM/yyyy - hh:mm aa') }}
			</template>
		</Column>

		<template #empty> No Projects to display.</template>
	</DataTable>
</template>

<style scoped>
.project-table :deep(.p-datatable-thead > tr > th) {
	background-color: var(--color-background) !important;
}
</style>
