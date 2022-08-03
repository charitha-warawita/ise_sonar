<script setup lang="ts">
import type Project from '@/model/Project.js';

const props = defineProps<{
	project: Project | null;
}>();
</script>

<template>
	<!-- Use Skelenton instead of hiding div. -->
	<div v-if="props.project !== null" class="project-overview shadowed">
		<h1>{{ props.project?.Name }}</h1>
		<!-- TODO: Show further details. -->
	</div>
</template>

<style scoped>
.project-overview {
	padding: 20px;
}
</style>
