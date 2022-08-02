<script setup lang="ts">
import Menubar from 'primevue/menubar';
import type { MenuItem } from 'primevue/menuitem';
import { ref, type Ref } from 'vue';

const items: Ref<MenuItem[]> = ref([
	{
		label: 'Projects',
		icon: 'pi pi-circle-fill',
		to: '/',
	},
	{
		label: 'Settings',
		icon: 'pi pi-circle-fill',
	},
	{
		label: 'User Name', // TODO: Dynamically assign username, use store.,
		icon: 'pi pi-circle-fill',
	},
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
	height: 8vh;
	background: var(--color-background) !important;
	box-shadow: 0px 0px 12px var(--shadow-color);
	border: none !important;
}

/* Experiment with screen sizes. */

@media screen and (max-height: 748px) {
	.p-menubar {
		height: 55px;
	}
}

/* :deep allows access to child components from scoped styles */
.p-menubar :deep(.p-menubar-root-list),
:deep(.p-menubar-button) {
	margin-left: auto !important;
}
</style>
