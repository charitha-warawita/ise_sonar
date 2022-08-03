<script setup lang="ts">
import type TargetAudience from '@/model/TargetAudience.js';
import DataView from 'primevue/dataview';
import PrimaryButton from '../../buttons/PrimaryButton.vue';

const props = defineProps({
	targetAudience: {
		type: Array<TargetAudience>,
		required: true,
	},
});

const emits = defineEmits<{
	(event: 'add'): void;
	(event: 'edit', target: TargetAudience): void;
}>();
</script>

<template>
	<!-- This Data View needs styling, consistent with figma UI.  -->
	<DataView
		layout="list"
		:value="props.targetAudience"
		data-key="Id"
		paginator
		:always-show-paginator="false"
		:rows="5"
		class="dataview-container shadowed"
	>
		<template #header>
			<div class="target-audience-list-header">
				<div class="target-audience-list-title">Target Audiences</div>
				<div v-if="props.targetAudience?.length">
					<PrimaryButton icon="pi pi-plus" @click="emits('add')" />
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
					<i class="pi pi-pencil" @click="emits('edit', slot.data)"></i>
				</div>
			</div>
		</template>
		<template #empty>
			<div class="target-audience-empty-container">
				<div class="">
					<p style="font-weight: bold">There are No Target Audiences</p>
					<p>Please create at least on target group to create the project</p>

					<div style="max-width: 250px; margin: 20px auto">
						<PrimaryButton square label="Create Target Audience" @click="emits('add')" />
					</div>
				</div>
			</div>
		</template>
	</DataView>
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
