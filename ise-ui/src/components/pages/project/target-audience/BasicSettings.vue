<script setup lang="ts">
import TargetAudience from '@/model/TargetAudience.js';
import InputNumber from 'primevue/inputnumber';
import InputText from 'primevue/inputtext';
import { computed } from 'vue';

const props = defineProps({
	targetAudience: {
		type: TargetAudience,
		required: true,
	},
});
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
			<div class="basic-input-row">
				<div class="basic-input">
					<span class="p-float-label">
						<InputText id="ta-name" class="ta-input" type="text" v-model="ta.Name" />
						<label for="ta-name">Target Audience Name</label>
					</span>
				</div>

				<div class="basic-input">
					<span class="p-float-label">
						<InputNumber
							id="ta-estimated-ir"
							class="ta-input"
							type="number"
							v-model="ta.EstimatedIR"
						/>
						<label for="ta-estimated-ir">Estimated IR</label>
					</span>
				</div>
			</div>
			<div class="basic-input-row">
				<div class="basic-input">
					<span class="p-float-label">
						<InputNumber
							id="ta-estimated-ir"
							class="ta-input"
							type="number"
							v-model="ta.EstimatedLOI"
						/>
						<label for="ta-estimated-loi">Estimated LOI</label>
					</span>
				</div>

				<div class="basic-input">
					<span class="p-float-label">
						<InputText id="ta-name" class="ta-input" type="text" />
						<!-- <label for="ta-name">Target Audience Name</label> -->
					</span>
				</div>
			</div>
			<div class="basic-input-row">
				<div class="basic-input">
					<span class="p-float-label">
						<InputText id="ta-name" class="ta-input" type="text" />
						<!-- <label for="ta-name">Target Audience Name</label> -->
					</span>
				</div>

				<div class="basic-input">
					<span class="p-float-label">
						<InputNumber id="ta-estimated-ir" class="ta-input" type="number" />
						<!-- <label for="ta-estimated-ir">EstimatedIR</label> -->
					</span>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped lang="scss">
.basic-settings-title {
	padding: 10px 0 10px 10px;
}

.basic-input-container {
	display: flex;
	flex-direction: column;
	flex-wrap: wrap;

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
