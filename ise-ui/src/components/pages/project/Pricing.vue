<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';

const emits = defineEmits(['created']);
</script>

<template>
	<div class="estimated-price shadowed">
		<div class="estimated-price-calculation">
			<h3 style="font-weight: bold">Estimated Price</h3>
		</div>
		<div class="create-project-container">
			<PrimaryButton square disabled label="Create Project" @click="emits('created')" />
		</div>
	</div>
</template>

<style scoped>
.estimated-price {
	height: 100%;
	padding: 20px;
	display: flex;
	flex-direction: column;
}

.estimated-price-calculation {
	flex-grow: 1;
}
</style>
