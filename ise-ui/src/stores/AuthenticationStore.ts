import { defineStore } from 'pinia';
import { type Ref, ref, computed } from 'vue';

export const useAuthenticationStore = defineStore('authentication', () => {
	const isLoggedIn: Ref<boolean> = ref(false);
	const user: Ref<object | null> = ref(null);

	function login(username: string, password: string) {
		if (!username || !password) return; // TODO: Error: Incorrect details.

		isLoggedIn.value = true;
	}

	return {
		isLoggedIn: computed(() => isLoggedIn),
		user: computed(() => user),
		login,
	};
});
