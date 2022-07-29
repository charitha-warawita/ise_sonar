<script setup lang="ts">

import DataTable from 'primevue/datatable'
import Column from 'primevue/column'



</script>

<template>
    <DataTable class="project-table">
        <Column field="Id" header="Project Id"></Column>
        <Column field="Name" header="Project Name"></Column>
        <Column field="CreationDate" header="Creation Date"></Column>
        <Column field="LastActivity" header="Last Activity"></Column>
    </DataTable>
</template>

<style scoped>
    .project-table :deep(.p-datatable-thead) {
        background-color: var(--color-background);
    }

</style>