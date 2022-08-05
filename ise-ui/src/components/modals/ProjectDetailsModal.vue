<script setup lang="ts">
import { reactive, computed, ref, type Ref } from 'vue';
import { required, helpers } from '@vuelidate/validators';
import { useVuelidate } from '@vuelidate/core';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import Calendar from 'primevue/calendar';
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import Project from '@/model/Project';

const props = defineProps({
	project: {
		type: Project,
		required: false,
	},
});
const emits = defineEmits(['submitted']);
const today = new Date();
const submitted: Ref<boolean> = ref(false);
const fields = reactive<{
	Name: string;
	MaconomyNumber: string;
	Owner: string;
	StartDate: Date | undefined;
	EndDate: Date | undefined;
}>({
	Name: '',
	MaconomyNumber: '',
	Owner: '',
	StartDate: undefined,
	EndDate: undefined,
});
const rules = computed(() => ({
	Name: {
		required,
	},
	MaconomyNumber: {
		required,
	},
	Owner: {
		required,
	},
	// TODO: Once the project has been started, do not allow changes to start date.
	StartDate: {
		required,
		minValue: helpers.withMessage(
			() => `Start Date must be greater than or equal to today's date`,
			(value: Date) => value >= today
		),
	},
	// TODO: Should EndDate be editable if the project has started?
	EndDate: {
		required,
		minValue: helpers.withMessage(
			() => `End Date must be greater than or equal to the selected Start Date`,
			(value: Date) => value >= today && fields.StartDate !== undefined && value >= fields.StartDate
		),
	},
}));
const vuelidate = useVuelidate(rules, fields);

const HandleSubmit = (isFormValid: boolean): void => {
	submitted.value = true;

	if (!isFormValid) return;

	const project = props.project ? UpdateProject() : CreateProject();
	emits('submitted', project);
};

const CreateProject = (): Project => {
	if (!fields.StartDate || !fields.EndDate) throw 'StartDate or EndDate is null.';

	const project = new Project(
		fields.Name,
		fields.MaconomyNumber,
		fields.Owner,
		fields.StartDate,
		fields.EndDate
	);

	return project;
};

const UpdateProject = (): Project => {
	if (!props.project) throw 'Cannot update project when "project" prop is null.';
	if (!fields.StartDate || !fields.EndDate) throw 'StartDate or EndDate is null.';

	const project = props.project;
	project.Name = fields.Name;
	project.MaconomyNumber = fields.MaconomyNumber;
	project.Owner = fields.Owner;
	project.StartDate = fields.StartDate;
	project.EndDate = fields.EndDate;
	project.LastActivity = new Date();

	return project;
};

const SetFields = (): void => {
	if (!props.project) return;

	fields.Name = props.project.Name;
	fields.MaconomyNumber = props.project.MaconomyNumber;
	fields.Owner = props.project.Owner;
	fields.StartDate = props.project.StartDate;
	fields.EndDate = props.project.EndDate;
};

const ResetFields = () => {
	submitted.value = false;

	fields.Name = '';
	fields.MaconomyNumber = '';
	fields.Owner = '';
	fields.StartDate = undefined;
	fields.EndDate = undefined;
};
</script>

<template>
	<Dialog
		modal
		@after-hide="ResetFields"
		@show="SetFields"
		:breakpoints="{ '1200px': '30vw', '992px': '40vw', '576px': '90vw' }"
		:style="{ width: '20vw' }"
	>
		<form @submit.prevent="HandleSubmit(!vuelidate.$invalid)">
			<div class="project-form-inputs">
				<!-- Name -->
				<div class="field">
					<div class="p-float-label">
						<InputText
							id="project-name"
							:class="{ 'p-invalid': vuelidate.Name.$invalid && submitted }"
							type="text"
							v-model.lazy="vuelidate.Name.$model"
						/>
						<label for="project-name" :class="{ 'p-error': vuelidate.Name.$invalid && submitted }">
							Project Name*
						</label>
					</div>
					<small v-if="vuelidate.Name.$invalid && submitted" class="p-error">
						{{ vuelidate.Name.required.$message.replace('Value', 'Name') }}
					</small>
				</div>

				<!-- Maconomy Number -->
				<div class="field">
					<div class="p-float-label">
						<InputText
							id="maconomy-number"
							:class="{ 'p-invalid': vuelidate.MaconomyNumber.$invalid && submitted }"
							type="number"
							v-model.lazy="vuelidate.MaconomyNumber.$model"
						/>
						<label
							for="maconomy-number"
							:class="{ 'p-error': vuelidate.MaconomyNumber.$invalid && submitted }"
						>
							Maconomy Number*
						</label>
					</div>
					<small v-if="vuelidate.MaconomyNumber.$invalid && submitted" class="p-error">
						{{ vuelidate.MaconomyNumber.required.$message.replace('Value', 'Maconomy Number') }}
					</small>
				</div>

				<!-- Owner -->
				<div class="field">
					<div class="p-float-label">
						<InputText
							id="project-owner"
							:class="{ 'p-invalid': vuelidate.Owner.$invalid && submitted }"
							type="text"
							v-model.lazy="vuelidate.Owner.$model"
						/>
						<label
							for="project-owner"
							:class="{ 'p-error': vuelidate.Owner.$invalid && submitted }"
						>
							Owner*
						</label>
					</div>
					<small v-if="vuelidate.Owner.$invalid && submitted" class="p-error">
						{{ vuelidate.Owner.required.$message.replace('Value', 'Owner') }}
					</small>
				</div>

				<!-- Start Date -->
				<div class="field">
					<div class="p-float-label">
						<Calendar
							id="project-start-date"
							v-model.lazy="vuelidate.StartDate.$model"
							dateFormat="dd/mm/yy"
							:min-date="today"
							:class="{ 'p-invalid': vuelidate.StartDate.$invalid && submitted }"
						/>
						<label
							for="project-start-date"
							:class="{ 'p-error': vuelidate.StartDate.$invalid && submitted }"
						>
							Start Date*
						</label>
					</div>
					<small v-if="vuelidate.StartDate.required.$invalid && submitted" class="p-error">
						{{ vuelidate.StartDate.required.$message.replace('Value', 'Start Date') }}
					</small>
					<small v-else-if="vuelidate.StartDate.minValue.$invalid && submitted" class="p-error">
						{{ vuelidate.StartDate.minValue.$message }}
					</small>
				</div>

				<!-- End Date -->
				<div class="field">
					<div class="p-float-label">
						<Calendar
							id="project-end-date"
							v-model.lazy="vuelidate.EndDate.$model"
							dateFormat="dd/mm/yy"
							:min-date="vuelidate.StartDate.$model"
							:class="{ 'p-invalid': vuelidate.EndDate.$invalid && submitted }"
						/>
						<label
							for="project-end-date"
							:class="{ 'p-error': vuelidate.EndDate.$invalid && submitted }"
						>
							End Date*
						</label>
					</div>
					<small v-if="vuelidate.EndDate.required.$invalid && submitted" class="p-error">
						{{ vuelidate.EndDate.required.$message.replace('Value', 'End Date') }}
					</small>
					<small v-else-if="vuelidate.EndDate.minValue.$invalid && submitted" class="p-error">
						{{ vuelidate.EndDate.minValue.$message }}
					</small>
				</div>
			</div>

			<div class="button-container">
				<PrimaryButton square label="Submit" type="submit" />
			</div>
		</form>
	</Dialog>
</template>

<style scoped lang="scss">
.project-form-inputs {
	margin-top: 15px;
}

.field {
	padding: 5px 0;

	::v-deep(input),
	::v-deep(span.p-calendar) {
		width: 100%;
	}
}

.button-container {
	margin-top: 25px;
}
</style>
