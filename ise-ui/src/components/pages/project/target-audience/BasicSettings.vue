<script setup lang="ts">
import type { TargetAudience } from '@/types/TargetAudience.js';
import InputNumber from 'primevue/inputnumber';
import InputText from 'primevue/inputtext';
import { computed } from 'vue';

const props = defineProps<{
	targetAudience: TargetAudience;
}>();
const emits = defineEmits<{
	(event: 'update:targetAudience', ta: TargetAudience): void;
}>();
const ta = computed<TargetAudience>({
	get: () => props['targetAudience'],
	set: (value: TargetAudience) => emits('update:targetAudience', value),
});
</script>

<template>
	<div class="basic-settings shadowed">
		<div class="basic-settings-title">
			<h2 style="font-weight: bold">Basic Settings</h2>
		</div>

		<div class="basic-input-container">
			<div class="basic-input">
				<span class="p-float-label">
					<InputText id="ta-name" class="ta-input" v-model="ta.Name" />
					<label for="ta-name">Target Audience Name</label>
				</span>
			</div>

			<div class="basic-input">
				<span class="p-float-label">
					<InputNumber id="ta-estimated-ir" class="ta-input" v-model="ta.EstimatedIR" />
					<label for="ta-estimated-ir">Estimated IR</label>
				</span>
			</div>

			<div class="basic-input">
				<span class="p-float-label">
					<InputNumber id="ta-estimated-ir" class="ta-input" v-model="ta.EstimatedLOI" />
					<label for="ta-estimated-loi">Estimated LOI</label>
				</span>
			</div>

			<div class="basic-input">
				<span class="p-float-label">
					<InputText id="ta-name" class="ta-input" type="text" />
				</span>
			</div>

			<div class="basic-input">
				<span class="p-float-label">
					<InputText id="ta-name" class="ta-input" type="text" />
				</span>
			</div>

			<div class="basic-input">
				<span class="p-float-label">
					<InputNumber id="ta-estimated-ir" class="ta-input" type="number" />
				</span>
			</div>
		</div>
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.basic-settings-title {
	padding: 10px 0 10px 10px;
}

.basic-input-container {
	display: grid;
	grid-template-columns: 1fr 1fr;
	gap: 30px 15px;
	padding: 15px;

	@media screen and (max-width: $sm) {
		grid-template-columns: 1fr;
	}

	.basic-input {
		.ta-input {
			width: 100%;
		}
	}

	> .basic-input-row {
		display: flex;
		flex-direction: row;

		.basic-input {
			flex-grow: 1;
			margin: 15px;

			.ta-input {
				width: 100%;
			}
		}
	}
}
</style>
