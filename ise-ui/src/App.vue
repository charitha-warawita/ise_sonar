<script setup lang="ts">
import Menu from '@/components/Menu.vue';
import Toast from 'primevue/toast';
import { computed } from 'vue';
import { RouterView, useRoute } from 'vue-router';

const route = useRoute();

const isLoginOrRegister = computed(() => route.name === 'login' || route.name === 'register');
</script>

<template>
	<header v-if="!isLoginOrRegister">
		<Menu></Menu>
	</header>

	<main :class="{ 'main-margin': !isLoginOrRegister }">
		<RouterView />
		<Toast position="bottom-right" />
	</main>
</template>

<style scoped>
/* Needs media queries */
main {
	flex-grow: 1;
}

.main-margin {
	margin: 30px;
}
</style>
