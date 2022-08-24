<script setup lang="ts">
import type { ProgressCardData } from '@/types/visuals/ProgressCardData';
import Card from 'primevue/card';
import Chip from 'primevue/chip';
import ProgressBar from 'primevue/progressbar';
import { computed } from 'vue';

const props = defineProps<{
	data: ProgressCardData;
}>();

const difference = computed(() => {
	if (!props.data.target) return null;

	const diff = props.data.target * -1 + props.data.value;
	return Math.round(diff);
});

const points = computed(() => {
	if (difference.value === null) return null;

	return (difference.value > 0 ? `+ ${difference.value}` : difference.value) + ' pts';
});

const achieved = computed(() => {
	if (difference.value === null || props.data.target === undefined || !props.data.operator)
		return true;

	switch (props.data.operator) {
		case 'lt':
			return props.data.value < props.data.target;

		case 'gt':
			return props.data.value > props.data.target;

		default:
			return true;
	}
});
</script>

<template>
	<Card>
		<template #header>
			<div class="statistic-header">
				<i class="pi pi-user"></i>
				<span>{{ props.data.title }}</span>
			</div>
		</template>

		<template #content>
			<div class="statistic-row">
				<span>
					{{ props.data.value }}%
					<span v-if="!achieved" class="warn"><i class="pi pi-exclamation-triangle"></i></span>
				</span>

				<span class="chip-container" v-if="points !== null">
					<Chip class="points-chip">
						{{ points }}
					</Chip>
				</span>
			</div>

			<div class="progress-container">
				<ProgressBar
					:value="props.data.value"
					:showValue="false"
					class="progressbar"
					:class="{ warn: !achieved }"
				/>
				<div
					v-if="props.data.target !== undefined"
					class="progress-target-mark"
					:style="{ width: `${props.data.target}%` }"
				></div>
			</div>
		</template>

		<template v-if="props.data.target" #footer>
			<div class="statistic-footer">
				<span class="statistic-type-text">{{ props.data.type }}: </span>
				<span v-if="props.data.operator">
					{{ props.data.operator === 'gt' ? 'Above' : 'Below' }}:
				</span>
				<span>{{ props.data.target }}%</span>
				<span class="info" v-if="props.data.description !== undefined">
					<i class="pi pi-exclamation-circle" v-tooltip.bottom="props.data.description"></i>
				</span>
			</div>
		</template>
	</Card>
	<!-- <Tooltip></Tooltip> -->
</template>

<style scoped lang="scss">
.statistic-header {
	padding: 10px 0 0 10px;

	i {
		margin-right: 5px;
	}

	span {
		font-size: small;
	}
}

.statistic-row {
	display: flex;
	margin-bottom: 0.8rem;

	span:first-of-type {
		flex-grow: 1;
		font-weight: 700;
		font-size: 1.5rem;

		.warn > i {
			margin-top: auto;
			margin-bottom: auto;
			font-size: 1.2rem;
		}
	}

	.chip-container {
		margin-top: auto;
		margin-bottom: auto;
		.points-chip {
			font-size: smaller;
			font-weight: 500;
		}
	}
}

.progress-container {
	.p-progressbar {
		border-radius: 15px;
		height: 0.5rem;

		::v-deep(.p-progressbar-value) {
			transition: width 2s ease-out;
		}
	}

	.progress-target-mark {
		border-right: 2px solid black;
		height: 0.5rem;
		position: relative;
		top: -0.5rem;
	}
}

.statistic-footer {
	.statistic-type-text {
		font-weight: bold;
	}

	.info {
		margin-left: 0.5rem;
	}
}

::v-deep(.p-card-content) {
	margin-bottom: 0;
	padding: 0 !important;
}

.warn {
	color: orange;

	::v-deep(.p-progressbar-value) {
		background: orange;
	}
}
</style>
