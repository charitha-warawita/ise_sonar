<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import SecondaryButton from '@/components/buttons/SecondaryButton.vue';
import { useAuthentication } from '@/services/AuthenticationService';
import useVuelidate from '@vuelidate/core';
import { minLength, required } from '@vuelidate/validators';
import Divider from 'primevue/divider';
import InputText from 'primevue/inputtext';
import Message from 'primevue/message';
import Password from 'primevue/password';
import { computed, ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const { authenticating, LoginAsync } = useAuthentication();

const username = ref<string>('');
const password = ref<string>('');
const error = ref<string>();

const submitted = ref(false);
const rules = computed(() => ({
	username: {
		required,
		minLength: minLength(1),
	},
	password: {
		required,
		minLength: minLength(1),
	},
}));
const validator = useVuelidate(rules, { username, password });

const HandleLogin = async () => {
	submitted.value = true;

	await LoginAsync(username.value, password.value)
		.then(() => {
			router.push({
				path: '/',
			});
		})
		.catch((err: string) => {
			error.value = err;
		});
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
				<div v-if="error">
					<Message severity="error" :closable="false">{{ error }}</Message>
				</div>
				<form @submit.prevent="HandleLogin">
					<span>
						<small v-if="validator.username.$invalid && submitted" class="p-error">
							{{ validator.username.required.$message.replace('Value', 'Name') }}
						</small>
						<InputText
							class="login-input"
							:class="{ 'p-invalid': submitted && validator.username.$invalid }"
							placeholder="Username"
							v-model="username"
							autocomplete="username"
						/>
					</span>
					<span>
						<small v-if="validator.password.$invalid && submitted" class="p-error">
							{{ validator.password.required.$message.replace('Value', 'Password') }}
						</small>
						<Password
							class="login-input"
							:class="{ 'p-invalid': submitted && validator.password.$invalid }"
							:feedback="false"
							placeholder="Password"
							toggleMask
							autocomplete="current-password"
							v-model="password"
						/>
					</span>

					<div class="button-container">
						<PrimaryButton type="submit" label="Log In" :loading="authenticating" />
					</div>
					<div class="button-container">
						<SecondaryButton label="Register" @click="HandleRegister" />
					</div>
				</form>
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
