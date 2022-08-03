<script setup lang="ts">
import Menubar from 'primevue/menubar';
import type { MenuItem } from 'primevue/menuitem';
import Breadcrumb from 'primevue/breadcrumb';
import { ref, type Ref } from 'vue';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore.js';

const breadcrumbs = useBreadcrumbStore();
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
	<div class="top-nav-container shadowed">
		<Menubar :model="items">
			<template #start>
				<h2>ISE</h2>
			</template>
		</Menubar>

		<div v-if="breadcrumbs.items.length > 0">
			<Breadcrumb :model="breadcrumbs.items" :home="breadcrumbs.home" />
		</div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.p-menubar {
	border-radius: 0;
	height: 8vh;
	background: var(--color-background) !important;
	border: none !important;
}

.p-breadcrumb {
	border: none;
	font-size: small;
	padding: 0.5rem;
	padding-top: 0;

	:v-deep(.pi) {
		font-size: small;
	}
}
/* Experiment with screen sizes. */

@media screen and (max-height: $md) {
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
