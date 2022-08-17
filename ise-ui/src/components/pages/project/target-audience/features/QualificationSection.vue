<script setup lang="ts">
import FeatureRoot from './FeatureRoot.vue';

const props = defineProps({
	ProjectId: Number,
});

const Save = () => {
	console.log('saving qualifications for project');
};
</script>

<template>
	<FeatureRoot @selected="Save">
		<div>Qualifications</div>
	</FeatureRoot>
</template>

<style scoped lang="scss"></style>
