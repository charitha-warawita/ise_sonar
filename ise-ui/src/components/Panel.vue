<script setup lang="ts">
import Panel from 'primevue/panel';
</script>

<template>
	<Panel>
		<template #header>
			<slot name="header"></slot>
		</template>

		<slot></slot>
	</Panel>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;
@use '@/assets/mixins.scss';

.p-panel :deep(.p-panel-header) {
	@include mixins.shadowed();

	border: none;
	z-index: 1;
	background-color: var(--color-background);
	border-top-left-radius: $border-radius;
	border-top-right-radius: $border-radius;
}

.p-panel :deep(.p-panel-content) {
	@include mixins.shadowed();

	border: none;
	border-bottom-left-radius: $border-radius;
	border-bottom-right-radius: $border-radius;
}
</style>
