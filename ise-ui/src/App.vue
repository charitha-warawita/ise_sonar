<script setup lang="ts">
import Menu from '@/components/Menu.vue';
import Toast from 'primevue/toast';
import { RouterView } from 'vue-router';
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
