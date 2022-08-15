<script setup lang="ts">
import TargetAudience from '@/model/TargetAudience';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import { useToast } from 'primevue/usetoast';
import { computed, reactive } from 'vue';
import PrimaryButton from '../buttons/PrimaryButton.vue';

const props = defineProps({ visible: Boolean, projectId: { type: Number, required: true } });
const emits = defineEmits<{
	(event: 'update:visible', value: boolean): void;
	(event: 'created', value: TargetAudience): void;
}>();
const visible = computed({
	get: () => props.visible,
	set: (value) => emits('update:visible', value),
});
const fields = reactive<{
	Name: string | null;
	EstimatedIR: string | null;
	EstimatedLOI: string | null;
}>({
	Name: null,
	EstimatedIR: null,
	EstimatedLOI: null,
});

const toast = useToast();
const Create = () => {
	// TODO: Use validator instead.
	if (fields.Name == null || fields.EstimatedIR == null || fields.EstimatedLOI == null) return;

	const ta = new TargetAudience();
	ta.ProjectId = props.projectId;
	ta.Name = fields.Name;
	ta.EstimatedIR = Number.parseInt(fields.EstimatedIR);
	ta.EstimatedLOI = Number.parseInt(fields.EstimatedLOI);

	emits('created', ta);

	toast.add({
		severity: 'success',
		summary: 'Target Audience Created',
		detail: 'Successfully created a new Target Audience.',
		life: 3000,
	});

	visible.value = false;
};

const ResetFields = () => {
	fields.Name = null;
	fields.EstimatedIR = null;
	fields.EstimatedLOI = null;
};
</script>

<template>
	<Dialog modal v-model:visible="visible" @after-hide="ResetFields">
		<form>
			<div class="target-audience-form-inputs">
				<span class="p-float-label">
					<InputText id="target-audience-name" type="text" v-model="fields.Name" />
					<label for="target-audience-name">Target Audience Name</label>
				</span>
				<span class="p-float-label">
					<InputText id="target-audience-IR" type="number" v-model="fields.EstimatedIR" />
					<label for="target-audience-IR">Estimated IR</label>
				</span>
				<span class="p-float-label">
					<InputText id="target-audience-LOI" type="number" v-model="fields.EstimatedLOI" />
					<label for="target-audience-LOI">Estimated LOI</label>
				</span>
			</div>

			<PrimaryButton square label="Create Target Audience" @click="Create" />
		</form>
	</Dialog>
</template>

<style scoped>
.target-audience-form-inputs > * {
	margin: 25px 0;
}
</style>
