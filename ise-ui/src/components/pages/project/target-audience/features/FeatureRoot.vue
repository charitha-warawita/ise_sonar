<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import SecondaryButton from '@/components/buttons/SecondaryButton.vue';
import Divider from 'primevue/divider';

const emits = defineEmits<{
	(event: 'cancel'): void;
	(event: 'selected'): void;
}>();
</script>

<template>
	<div class="feature-container">
		<div class="feature-slot">
			<slot></slot>
		</div>
		<Divider />
		<div class="feature-buttons">
			<SecondaryButton square label="Cancel" @click="emits('cancel')" />

			<PrimaryButton square label="Select Feature" @click="emits('selected')" />
		</div>
	</div>
</template>

<style scoped lang="scss">
.feature-container {
	display: flex;
	flex-direction: column;
	height: 100%;
	padding-left: 20px;

	.feature-slot {
		flex-grow: 1;
	}

	.feature-buttons {
		display: flex;

		:first-child {
			margin-right: 10px;
		}

		button {
			flex-grow: 1;
		}
	}
}
</style>
