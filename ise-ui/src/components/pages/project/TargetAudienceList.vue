<script setup lang="ts">
import type TargetAudience from '@/model/TargetAudience.js';
import DataView from 'primevue/dataview';
import { computed, ref } from 'vue';
import PrimaryButton from '../../buttons/PrimaryButton.vue';
import CreateTargetAudienceModal from '@/components/modals/CreateTargetAudienceModal.vue';

const props = defineProps({
	targetAudiences: {
		type: Array<TargetAudience>,
		required: true,
	},
});

const emits = defineEmits<{
	(event: 'update:targetAudience', data: Array<TargetAudience>): void;
}>();

const audiences = computed({
	get: () => props.targetAudiences,
	set: (value) => emits('update:targetAudience', value),
});

const isModalVisible = ref(false);
const OpenTargetAudienceModal = () => {
	isModalVisible.value = true;
};

const AddTargetAudience = (ta: TargetAudience) => {
	audiences.value.push(ta);
};

const EditTargetAudience = (ta: TargetAudience) => {
	console.log(ta.Name);
};
</script>

<template>
	<DataView
		layout="list"
		:value="audiences"
		data-key="Id"
		paginator
		:always-show-paginator="false"
		:rows="5"
		class="dataview-container shadowed"
	>
		<template #header>
			<div class="target-audience-list-header">
				<div class="target-audience-list-title">Target Audiences</div>
				<div v-if="audiences.length">
					<PrimaryButton icon="pi pi-plus" @click="OpenTargetAudienceModal" />
				</div>
			</div>
		</template>
		<template #list="slot">
			<div class="target-audience-datarow">
				<div class="target-audience-content">
					<div>
						<h4 class="bold">{{ slot.data.Name }}</h4>
					</div>
					<div>
						<div class="target-audience-details">
							<div>Limit: {{ slot.data.Limit ?? 'N/A' }}</div>
							<div>Estimated IR: {{ slot.data.EstimatedIR }}</div>
							<div>Estimated LOI: {{ slot.data.EstimatedLOI }}</div>
						</div>
					</div>
				</div>

				<div class="target-audience-buttons">
					<i class="pi pi-pencil" @click="EditTargetAudience(slot.data)"></i>
				</div>
			</div>
		</template>
		<template #empty>
			<div class="target-audience-empty-container">
				<div class="">
					<p style="font-weight: bold">There are No Target Audiences</p>
					<p>Please create at least on target group to create the project</p>

					<div style="max-width: 250px; margin: 20px auto">
						<PrimaryButton square label="Create Target Audience" @click="OpenTargetAudienceModal" />
					</div>
				</div>
			</div>
		</template>
	</DataView>
	<CreateTargetAudienceModal v-model:visible="isModalVisible" @created="AddTargetAudience" />
</template>

<style scoped>
.target-audience-empty-container {
	padding: 20px;
	text-align: center;
}

.dataview-container :deep(.p-dataview-header) {
	border: none;
}

.target-audience-list-header {
	display: flex;
}

.target-audience-list-title {
	flex-grow: 1;
	font-weight: 600;
	align-self: center;
}

.target-audience-datarow {
	flex-grow: 1;
	display: flex;
	padding: 10px;
}

.target-audience-content {
	flex-grow: 1;
}

.target-audience-details {
	flex-grow: 1;
	display: grid;
	grid-template-columns: repeat(3, 1fr);
}

.target-audience-buttons {
	align-self: center;
	padding-right: 10px;
}

.target-audience-buttons :hover {
	cursor: pointer;
	color: grey;
}
</style>
