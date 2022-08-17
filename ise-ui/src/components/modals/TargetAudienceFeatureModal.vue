<script setup lang="ts">
import MarketSection from '@/components/pages/project/target-audience/features/MarketsSection.vue';
import ProfilingSection from '@/components/pages/project/target-audience/features/ProfilingSection.vue';
import QualificationSection from '@/components/pages/project/target-audience/features/QualificationSection.vue';
import QuotaSection from '@/components/pages/project/target-audience/features/QuotaSection.vue';
import SourcingSection from '@/components/pages/project/target-audience/features/SourcingSection.vue';
import Dialog from 'primevue/dialog';
import InputText from 'primevue/inputtext';
import ListBox from 'primevue/listbox';
import ScrollPanel from 'primevue/scrollpanel';
import { computed, markRaw, ref, type Component, type Ref } from 'vue';

const emits = defineEmits<{
	(event: 'selected'): void;
	(event: 'canceled'): void;
}>();

type ComponentConfig = {
	component: Component;
	props: object;
};

type ListItem = {
	label: string;
	value: ComponentConfig;
};

const filter: Ref<string | null> = ref(null);
const selected: Ref<ComponentConfig | null> = ref(null);
const features: ListItem[] = [
	{
		label: 'Qualifications',
		value: {
			// using markRaw here to ensure components are not made reactive. Vue will warn.
			component: markRaw(QualificationSection),
			props: {
				ProjectId: 1,
			},
		},
	},
	{
		label: 'Quotas',
		value: {
			component: markRaw(QuotaSection),
			props: [],
		},
	},
	{
		label: 'Markets',
		value: {
			component: markRaw(MarketSection),
			props: [],
		},
	},
	{
		label: 'Profiling',
		value: {
			component: markRaw(ProfilingSection),
			props: [],
		},
	},
	{
		label: 'Sourcing',
		value: {
			component: markRaw(SourcingSection),
			props: [],
		},
	},
];

const filteredFeatures = computed(() => {
	if (!filter.value) return features;

	return features.filter((f) => f.label.includes(filter.value as string));
});

const Reset = () => {
	filter.value = null;
	selected.value = null;
};
</script>

<template>
	<Dialog
		modal
		:breakpoints="{ '1200px': '60vw', '1081px': '80vw', '576px': '90vw' }"
		:style="{ width: '60vw', 'max-width': '1080px' }"
		@after-hide="Reset"
	>
		<div class="container">
			<div class="feature-list-container">
				<div class="feature-list-search">
					<InputText
						class="feature-list-filter-input"
						id="feature-filter"
						type="text"
						placeholder="Search"
						v-model="filter"
					/>
				</div>
				<div class="feature-list">
					<ScrollPanel style="width: 100%; height: 30vh">
						<ListBox
							:style="{ width: '100%' }"
							:options="filteredFeatures"
							v-model="selected"
							option-label="label"
							option-value="value"
						></ListBox>
					</ScrollPanel>
				</div>
			</div>
			<div class="list-selection">
				<component
					v-if="selected"
					:is="selected.component"
					v-bind="selected.props"
					@selected="emits('selected')"
					@cancel="emits('canceled')"
				></component>
				<div v-else class="empty-message">
					<!-- TODO: Styling  -->
					<p class="centered-v">Please select a feature from the list.</p>
				</div>
			</div>
		</div>
	</Dialog>
</template>

<style scoped lang="scss">
.container {
	display: flex;
	height: 35vh;

	.feature-list-container {
		flex-grow: 1;
		max-width: 250px;

		.feature-list-filter-input {
			margin: 5px 0;
		}
	}
	.list-selection {
		flex-grow: 1;
	}

	.empty-message {
		text-align: center;
		width: 100%;
		height: 100%;

		> p {
			font-weight: 500;
		}
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
