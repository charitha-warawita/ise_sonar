import { defineStore } from 'pinia';
import type { MenuItem } from 'primevue/menuitem';

export const useBreadcrumbStore = defineStore('breadcrumb', {
	state: () => {
		return {
			home: {
				icon: 'pi pi-home',
				to: '/',
			} as MenuItem,
			items: [] as MenuItem[],
		};
	},
});
