<script setup lang="ts">
import { RouterLink, RouterView } from 'vue-router';
import Menu from '@/components/Menu.vue';
import Toast from 'primevue/toast';
</script>

<template>
	<header>
		<Menu></Menu>
	</header>

	<main>
		<RouterView />
		<Toast position="bottom-right" />
	</main>
</template>

<style scoped>
/* Needs media queries */
main {
	flex-grow: 1;
	margin: 30px;
}
</style>
