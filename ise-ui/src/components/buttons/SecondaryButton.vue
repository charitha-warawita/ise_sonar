<script setup lang="ts">
import Button from 'primevue/button';

const props = defineProps({
	square: {
		type: Boolean,
		required: false,
		default: false,
	},
});
</script>

<template>
	<Button :class="{ square: props.square }" />
</template>

<style scoped>
.p-button {
	background-color: #ffffff;
	border: var(--blue-300) 1px solid;
	color: var(--blue-300);
	width: 100%;
}

.square {
	border-radius: 0;
}
</style>
