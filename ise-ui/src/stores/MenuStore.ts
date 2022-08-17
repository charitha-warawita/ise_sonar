import { defineStore } from 'pinia';
import { ref } from 'vue';

export const useMenuStore = defineStore('menu', () => {
	const shadowed = ref(true);

	return { shadowed };
});
