<script setup>
const props = defineProps({
	flush: {
		type: Boolean,
		required: false,
		default: false,
	},
	items: {
		type: Array,
		required: true,
	},
});
</script>

<template>
	<div
		class="accordion"
		:class="{ 'accordion-flush': props.flush }"
		id="accordion"
	>
		<div
			class="accordion-item"
			v-for="(item, index) in props.items"
			:key="index"
		>
			<div class="accordion-header">
				<button
					class="accordion-button collapsed"
					type="button"
					data-bs-toggle="collapse"
					:data-bs-target="`#collapse-${index}`"
					aria-expanded="true"
					:aria-controls="`collapse-${index}`"
				>
					<slot name="header" :item="item"></slot>
				</button>
			</div>

			<div
				:id="`collapse-${index}`"
				class="accordion-collapse collapse"
				:aria-labelledby="`heading-${index}`"
				data-bs-parent="#accordion"
			>
				<div class="accordion-body">
					<slot name="body" :item="item"></slot>
				</div>
			</div>
		</div>
	</div>
</template>

<style scoped></style>
