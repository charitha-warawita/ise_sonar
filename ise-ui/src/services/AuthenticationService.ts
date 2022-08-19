import type { User } from '@/types/User';
import { computed, ref } from 'vue';
import type { z } from 'zod';

type User = z.infer<typeof User>;

const authenticating = ref(false);
const user = ref<User>();
const isLoggedIn = computed(() => !authenticating.value && user.value);

export function useAuthentication() {
	const LoginAsync = async (username: string, password: string) => {
		if (!username || !password) return;

		authenticating.value = true;

		// Simulate network call;
		setTimeout(() => {
			user.value = {
				Id: 1,
				UserName: 'johndoe',
				FirstName: 'John',
				LastName: 'Doe',
			} as User;

			authenticating.value = false;
		}, 1500);
	};

	return {
		authenticating,
		user,
		isLoggedIn,
		LoginAsync,
	};
}
