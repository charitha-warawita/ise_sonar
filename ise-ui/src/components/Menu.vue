<script setup lang="ts">
import Menubar from 'primevue/menubar';
import type { MenuItem } from 'primevue/menuitem';
import { ref, type Ref } from 'vue';

const items: Ref<MenuItem[]> = ref([
    {
        label: 'Projects',
        icon: 'pi pi-circle-fill'
    },
    {
        label: 'Settings',
        icon: 'pi pi-circle-fill'
    },
    {
        label: 'User Name',  // TODO: Dynamically assign username, use store.,
        icon: 'pi pi-circle-fill'
    }
]);

</script>

<template>
    <Menubar :model="items">
        <template #start>
            <h2>ISE</h2>
        </template>        
    </Menubar>
</template>

<style scoped>

.p-menubar {
    border-radius: 0;
    height: 10vh;
    background: var(--color-background) !important;
    box-shadow: 0px 0px 12px lightgrey; /* Colour needs fine tuning */
    border: none !important;
}

/* Experiment with screen sizes. */
@media (min-width: 1048px) {
    .p-menubar {
        height: 8vh;
    }
}

/* :deep allows access to child components from scoped styles */
.p-menubar :deep(.p-menubar-root-list), :deep(.p-menubar-button) {
    margin-left: auto !important;
}

</style>