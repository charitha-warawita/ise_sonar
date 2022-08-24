<script setup lang="ts">
import type { Project } from '@/types/Project';
import Column from 'primevue/column';
import DataTable from 'primevue/datatable';
import { computed } from 'vue';

const props = defineProps({
	risks: {
		type: Array<Project>,
		required: true,
	},
});

const selectable = computed(() => props.risks.length > 0);
</script>

<template>
	<DataTable :value="props.risks" :selection-mode="selectable ? 'single' : undefined">
		<Column field="Id" header="Project Id"></Column>
		<Column field="Name" header="Project Name"></Column>

		<template #empty>No Open Risks to display.</template>
	</DataTable>
</template>

<style scoped lang="scss">
:deep(.p-datatable-thead > tr > th) {
	background-color: var(--color-background) !important;
}
</style>
