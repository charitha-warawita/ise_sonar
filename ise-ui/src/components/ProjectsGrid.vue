<script setup lang="ts">

import DataTable, { type DataTableRowSelectEvent } from 'primevue/datatable'
import Column from 'primevue/column'

const rowSelect = (e: DataTableRowSelectEvent) => {
    console.log(e)
}

</script>

<template>
    <DataTable class="project-table" selection-mode="single" @row-select="rowSelect">
        <Column field="Id" header="Project Id"></Column>
        <Column field="Name" header="Project Name"></Column>
        <Column field="CreationDate" header="Creation Date"></Column>
        <Column field="LastActivity" header="Last Activity"></Column>
    </DataTable>
</template>

<style scoped>
       
    .project-table :deep(.p-datatable-thead > tr > th) {
        background-color: var(--color-background) !important;
    }

</style>