<script setup lang="ts">
import PrimaryButton from '@/components/buttons/PrimaryButton.vue';
import CreateTargetAudienceModal from '@/components/modals/CreateTargetAudienceModal.vue';
import type TargetAudience from '@/model/TargetAudience.js';
import { useTargetAudienceService } from '@/services/TargetAudienceService';
import DataView from 'primevue/dataview';
import { ref } from 'vue';
import { useRouter } from 'vue-router';

const router = useRouter();
const targetAudienceService = useTargetAudienceService();

const props = defineProps({
	targetAudiences: {
		type: Array<TargetAudience>,
		required: true,
	},
	projectId: {
		type: Number,
		required: true,
	},
});
const emits = defineEmits<{
	(event: 'created', audience: TargetAudience): void;
}>();

const isModalVisible = ref(false);
const OpenTargetAudienceModal = () => {
	isModalVisible.value = true;
};

const AddTargetAudience = async (ta: TargetAudience) => {
	ta.Id = await targetAudienceService.CreateAsync(ta);

	emits('created', ta);
};

const EditTargetAudience = (ta: TargetAudience) => {
	// Or should we emit 'edit' event and let parent handle routing?
	router.push({
		name: 'target-audience',
		params: {
			project: props.projectId,
			ta: ta.Id,
		},
	});
};
</script>

<template>
	<DataView
		layout="list"
		:value="props.targetAudiences"
		data-key="Id"
		paginator
		:always-show-paginator="false"
		:rows="5"
		class="dataview-container shadowed"
	>
		<template #header>
			<div class="target-audience-list-header">
				<div class="target-audience-list-title">Target Audiences</div>
				<div v-if="props.targetAudiences && props.targetAudiences.length">
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
	<CreateTargetAudienceModal
		v-model:visible="isModalVisible"
		:project-id="props.projectId"
		@created="AddTargetAudience"
	/>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

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
	width: 100%;
	display: flex;
	padding: 10px;
}

.target-audience-content {
	flex-grow: 1;
}

.target-audience-details {
	flex-grow: 1;
	margin-top: 10px;
	display: grid;
	grid-template-columns: repeat(3, 1fr);

	@media screen and (max-width: $sm) {
		grid-template-columns: 1fr;
		grid-gap: 10px 0;
	}
}

.target-audience-buttons {
	align-self: center;
	padding-right: 10px;

	:hover {
		cursor: pointer;
		color: grey;
	}
}
</style>
