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

		const result = new Promise<void>((res, rej) => {
			setTimeout(() => {
				username === 'failme' ? rej('Invalid username or password') : res();
			}, 1500);
		});

		await result
			.then(
				() =>
					(user.value = {
						Id: 1,
						UserName: 'johndoe',
						FirstName: 'John',
						LastName: 'Doe',
					} as User)
			)
			.catch((err) => Promise.reject(err))
			.finally(() => (authenticating.value = false));
	};

	const LogoutAsync = async () => {
		user.value = undefined;
	};

	return {
		authenticating,
		user,
		isLoggedIn,
		LoginAsync,
		LogoutAsync,
	};
}
