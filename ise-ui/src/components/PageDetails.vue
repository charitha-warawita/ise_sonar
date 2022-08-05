<script setup lang="ts">
import { useMenuStore } from '@/stores/MenuStore.js';
import { onMounted, onUnmounted } from 'vue';

const menu = useMenuStore();

onMounted(() => {
	menu.shadowed = false;
});

onUnmounted(() => {
	menu.shadowed = true;
});
</script>

<template>
	<div class="page-details-container">
		<div class="page-details shadowed">
			<slot></slot>
		</div>
	</div>
</template>

<style scoped lang="scss">
/* Container exists to hide the shadow extending past the top border. */
.page-details-container {
	margin-left: -30px;
	margin-right: -30px;
	margin-top: -30px;
	overflow: hidden;
}

.page-details {
	background-color: var(--background-color);
	margin-bottom: 30px;
	padding: 0 30px;
}
</style>
