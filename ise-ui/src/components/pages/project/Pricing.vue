<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import { reactive, computed } from 'vue';

const emits = defineEmits(['created']);

// TODO: Give more complete calculation, take project in as prop?
const pricing = reactive({
	FirstField: 100,
	SecondField: 200,
});

const total = computed(() => pricing.FirstField + pricing.SecondField);
</script>

<template>
	<div class="estimated-price shadowed">
		<div class="estimated-price-calculation">
			<h3 style="font-weight: bold">Estimated Price</h3>
			<div class="price-calculator">
				<div class="price-row">
					<label class="price-type">First Field</label>
					<div class="price-amount">{{ pricing.FirstField }}</div>
				</div>
				<div class="price-row">
					<div class="price-type">Second Field</div>
					<div class="price-amount">{{ pricing.SecondField }}</div>
				</div>
				<div class="price-row total-row">
					<div class="price-type">Total:</div>
					<div class="price-amount">{{ total }}</div>
				</div>
			</div>
		</div>
		<div class="create-project-container">
			<PrimaryButton square disabled label="Create Project" @click="emits('created')" />
		</div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.estimated-price {
	height: 100%;
	padding: 20px;
	display: flex;
	flex-direction: column;
}

.estimated-price-calculation {
	flex-grow: 1;
	display: flex;
	flex-direction: column;
}

.price-calculator {
	flex-grow: 1;
	display: grid;
	grid-template-rows: 1fr 1fr 3fr;
	padding: 15px 20px;

	@media screen and (max-width: $xl) {
		padding: 15px 0;
	}

	@media screen and (max-width: ($md - 1)) {
		padding: 15px 20px;
	}
}

.price-row {
	display: flex;

	&.total-row {
		align-items: end;
	}

	.price-type {
		flex-grow: 1;
	}
}
</style>
