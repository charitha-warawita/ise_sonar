<script setup lang="ts">
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore.js';

const breadcrumbs = useBreadcrumbStore();
breadcrumbs.$patch((state) => {
	state.items.push({
		label: 'Target Adudience', // NOTE: Should this be dynamic to the TA name?
	});
});
</script>

<template>
	<div></div>
</template>

<style scoped></style>
