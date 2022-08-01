<script setup lang="ts">
import type { Project } from '@/types/Project';

const props = defineProps<{
    project: Project | null
}>();

</script>

<template>
<!-- Use Skelenton instead -->
    <div v-if="project !== null" class="project-overview shadowed">
        <h1>{{ project?.Name }}</h1>
        <!-- TODO: Show further details. -->
    </div>
</template>

<style scoped>
.project-overview {
    /* flex-grow: 1; */
    padding: 20px;
    /* margin-bottom: 50px; */
}
</style>