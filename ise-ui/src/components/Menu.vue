<script setup lang="ts">
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore.js';
import { useMenuStore } from '@/stores/MenuStore.js';
import Breadcrumb from 'primevue/breadcrumb';
import Menubar from 'primevue/menubar';
import type { MenuItem } from 'primevue/menuitem';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const breadcrumbs = useBreadcrumbStore();
const menu = useMenuStore();
const router = useRouter();

const items = ref<MenuItem[]>([
	{
		label: 'Projects',
		icon: 'pi pi-circle-fill',
		to: '/projects',
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
	<div :class="{ 'top-nav-container': true, shadowed: menu.shadowed }">
		<Menubar :model="items">
			<template #start>
				<span @click="router.push('/')" class="ise-icon"><h2>ISE</h2></span>
			</template>
		</Menubar>

		<div v-if="breadcrumbs.items.length > 0">
			<Breadcrumb :model="breadcrumbs.items" :home="breadcrumbs.home" />
		</div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.ise-icon:hover {
	cursor: pointer;
	color: skyblue;
}

.p-menubar {
	border-radius: 0;
	height: 4vh;
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
