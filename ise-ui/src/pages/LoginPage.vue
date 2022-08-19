<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import SecondaryButton from '@/components/buttons/SecondaryButton.vue';
import { useAuthentication } from '@/services/AuthenticationService';
import Divider from 'primevue/divider';
import InputText from 'primevue/inputtext';
import Password from 'primevue/password';
import { ref, watch } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const { authenticating, user, LoginAsync } = useAuthentication();

const username = ref<string>();
const password = ref<string>();
watch(user, (newValue) => {
	if (newValue !== undefined)
		router.push({
			path: '/',
		});
});

const HandleLogin = async () => {
	if (!username.value || !password.value) return;

	await LoginAsync(username.value, password.value);
};

const HandleRegister = () => {
	router.push({
		name: 'register',
	});
};
</script>

<template>
	<div class="login-container">
		<div class="login-form-container">
			<div class="login-form">
				<span>
					<InputText class="login-input" placeholder="Username" v-model="username" />
				</span>
				<span>
					<Password
						class="login-input"
						:feedback="false"
						placeholder="Password"
						toggleMask
						v-model="password"
					/>
				</span>

				<div class="button-container">
					<PrimaryButton label="Log In" @click="HandleLogin" :loading="authenticating" />
				</div>
				<div class="button-container">
					<SecondaryButton label="Register" @click="HandleRegister" />
				</div>
			</div>

			<div style="color: grey"><Divider /></div>

			<div>
				<PrimaryButton label="Sign In with SSO" />
			</div>
		</div>
		<div class="login-side-placeholder"></div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.login-container {
	display: flex;
	height: 100vh;

	.login-form-container {
		flex-grow: 1;
		margin: auto 0;
		max-width: 30vw;
		padding-right: 8rem;
		padding-left: 2rem;

		@media screen and (max-width: $xxl) {
			max-width: 40vw;
		}

		@media screen and (max-width: $xl) {
			max-width: 50vw;
		}

		@media screen and (max-width: $lg) {
			max-width: 60vw;
		}

		@media screen and (max-width: $md) {
			max-width: 100vw;
			padding: 0 4rem;
		}

		.login-form {
			display: flex;
			flex-direction: column;

			.login-input {
				width: 100%;
				margin-bottom: 1rem;
			}

			.login-input > :deep(input) {
				width: 100%;
			}

			.button-container {
				margin-bottom: 1rem;
			}
		}
	}

	.login-side-placeholder {
		flex-grow: 1;
		background: lightskyblue;
		opacity: 0.3;

		@media screen and (max-width: $md) {
			flex-grow: 0;
		}
	}
}
</style>
