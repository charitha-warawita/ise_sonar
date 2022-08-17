<script setup lang="ts">
import MarketSection from '@/components/pages/project/target-audience/features/MarketsSection.vue';
import ProfilingSection from '@/components/pages/project/target-audience/features/ProfilingSection.vue';
import QualificationSection from '@/components/pages/project/target-audience/features/QualificationSection.vue';
import QuotaSection from '@/components/pages/project/target-audience/features/QuotaSection.vue';
import SourcingSection from '@/components/pages/project/target-audience/features/SourcingSection.vue';
import Dialog from 'primevue/dialog';
import ListBox from 'primevue/listbox';
import ScrollPanel from 'primevue/scrollpanel';
import { markRaw, ref, type Component, type Ref } from 'vue';

type SelectItem = {
	label: string;
	value: Component;
};

const selected: Ref<Component | null> = ref(null);
const features: SelectItem[] = [
	{
		label: 'Qualifications',
		value: markRaw(QualificationSection), // using markRaw here to ensure components are not made reactive.
	},
	{
		label: 'Quotas',
		value: markRaw(QuotaSection),
	},
	{
		label: 'Markets',
		value: markRaw(MarketSection),
	},
	{
		label: 'Profiling',
		value: markRaw(ProfilingSection),
	},
	{
		label: 'Sourcing',
		value: markRaw(SourcingSection),
	},
];
</script>

<template>
	<Dialog
		modal
		:breakpoints="{ '1200px': '60vw', '992px': '70vw', '576px': '90vw' }"
		:style="{ width: '60vw', 'max-width': '1080px' }"
	>
		<div class="container">
			<div class="feature-list-container">
				<div class="feature-list-search"></div>
				<div class="feature-list">
					<ScrollPanel style="width: 100%; height: 30vh">
						<ListBox
							:style="{ width: '100%' }"
							:options="features"
							v-model="selected"
							option-label="label"
							option-value="value"
						></ListBox>
					</ScrollPanel>
				</div>
			</div>
			<div class="list-selection">
				<component v-if="selected" :is="selected"></component>
				<div v-else>
					<p>Please select a feature from the list.</p>
				</div>
			</div>
		</div>
	</Dialog>
</template>

<style scoped lang="scss">
.container {
	display: flex;
	height: 30vh;

	.feature-list-container {
		flex-grow: 1;
		max-width: 250px;
	}
	.list-selection {
		flex-grow: 1;
	}
}

::v-deep(.p-scrollpanel) {
	.p-scrollpanel-wrapper {
		border-right: 4px solid var(--surface-ground);
	}

	.p-scrollpanel-bar {
		opacity: 1;
		width: 4px;
		background-color: var(--primary-color);
	}
}

::v-deep(.p-listbox) {
	border: none;
}

::v-deep(.p-dialog-content) {
	padding: 1rem !important;
}
</style>
