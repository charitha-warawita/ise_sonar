<script setup lang="ts">
import { reactive, computed } from 'vue';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import { useToast } from 'primevue/usetoast';

import PrimaryButton from '../buttons/PrimaryButton.vue';
import { Project } from '@/model/Project';

// https://vuejs.org/guide/components/events.html#usage-with-v-model
// Configure two-way data binding. We recieve a prop, 'visible'.
// Create a computed varible, visible. Which points to our prop.
// When the variable is updated, set will emit the update event.
// Vue will then update the parent component's prop value.
// We do this because we need to be able to set visible when we create a project.
const props = defineProps({ visible: Boolean });
const emits = defineEmits(['update:visible', 'created']);
const visible = computed({
	get: () => props.visible,
	set: (value) => emits('update:visible', value),
});

const fields = reactive<{
	Name: string | null;
	MaconomyNumber: string | null;
	Owner: string | null;
}>({
	Name: null,
	MaconomyNumber: null,
	Owner: null,
});

// TODO: Project probably needs to be a class for easier instantiation.
const toast = useToast();
const CreateProject = () => {
	if (
		fields.Name == null ||
		fields.MaconomyNumber == null ||
		fields.Owner == null
	)
		return;

	const now = new Date();
	const project = new Project();
	project.Name = fields.Name as string;
	project.MaconomyNumber = fields.MaconomyNumber;
	project.Owner = fields.Owner;
	project.CreationDate = now;
	project.LastActivity = now;

	// TODO: Validate.
	// TODO: API call to create modal. Returns the API generated id.

	emits('created', project);

	toast.add({
		severity: 'success',
		summary: 'New Project Created',
		detail: 'Successfully created a new project.',
		life: 3000,
	});
	visible.value = false;
};

const ResetFields = () => {
	fields.Name = null;
	fields.MaconomyNumber = null;
	fields.Owner = null;
};
</script>

<template>
	<Dialog
		modal
		ref="dialog"
		v-model:visible="visible"
		@after-hide="ResetFields"
	>
		<form class="project-form">
			<div class="project-form-inputs">
				<span class="p-float-label">
					<InputText
						id="project-name"
						type="text"
						v-model="fields.Name"
					></InputText>
					<label for="project-name">Project Name</label>
				</span>
				<span class="p-float-label">
					<InputText
						id="maconomy-number"
						type="number"
						v-model="fields.MaconomyNumber"
					></InputText>
					<label for="maconomy-number">Maconomy Number</label>
				</span>
				<span class="p-float-label">
					<InputText
						id="project-owner"
						type="text"
						v-model="fields.Owner"
					></InputText>
					<label for="project-owner">Owner</label>
				</span>
			</div>

			<PrimaryButton square label="Create Project" @click="CreateProject" />
		</form>
	</Dialog>
</template>

<style scoped>
.project-form-inputs > * {
	margin: 25px 0;
}
</style>
