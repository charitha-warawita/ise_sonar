import { defineStore } from 'pinia';
import { ref, type Ref } from 'vue';

export const useMenuStore = defineStore('menu', () => {
	const shadowed: Ref<boolean> = ref(true);

	return { shadowed };
});
