<script setup lang="ts">
import { computed, ref } from "vue";

const props = defineProps<{
	name: string;
	title: string;
	expanded: boolean;
}>();

const emits = defineEmits<{
	(e: "update:expanded", value: boolean): void;
}>();

const expanded = computed({
	get() {
		return props.expanded;
	},
	set(value: boolean) {
		emits("update:expanded", value);
	},
});

// If expanded is true onMount we need to apply 'show' class.
const initialClasses = ref<{ show?: boolean }>({
	show: expanded.value,
});

const handleToggle = () => {
	// If expanded, and its the first time the button has been clicked.
	if (expanded.value && initialClasses.value.show !== undefined) {
		// Allow time for bootstrap animation to hide the element. Then remove the 'show' class.
		// Bootstrap will then apply 'show' as needed.
		setTimeout(() => {
			initialClasses.value = {};
		}, 400);
	}

	expanded.value = !expanded.value;
};
</script>

<template>
	<div class="accordion" :id="props.name">
		<div class="accordion-item">
			<div class="accordion-header">
				<button
					class="accordion-button"
					type="button"
					data-bs-toggle="collapse"
					:data-bs-target="`#${props.name}-body`"
					@click="handleToggle"
				>
					<h5 class="mb-0">{{ props.title }}</h5>
				</button>
			</div>

			<div
				:id="`${props.name}-body`"
				class="accordion-collapse collapse"
				:class="initialClasses"
				:data-bs-parent="`#${props.name}`"
			>
				<div class="accordion-body">
					<slot name="body"></slot>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped>
.accordion-header {
	background-color: rgba(0, 0, 0, 0.03) !important;
}

.accordion-button {
	color: unset;
	background-color: unset;
}

.accordion-button:not(.collapsed)::after {
	background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' viewBox='0 0 16 16' fill='%23212529'%3e%3cpath fill-rule='evenodd' d='M1.646 4.646a.5.5 0 0 1 .708 0L8 10.293l5.646-5.647a.5.5 0 0 1 .708.708l-6 6a.5.5 0 0 1-.708 0l-6-6a.5.5 0 0 1 0-.708z'/%3e%3c/svg%3e");
}

.accordion-body {
	padding: 0;
}
</style>
