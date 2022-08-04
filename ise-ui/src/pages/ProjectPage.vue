<script setup lang="ts">
import { type Ref, ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import Pricing from '../components/pages/project/Pricing.vue';
import ProjectOverview from '../components/pages/project/ProjectOverview.vue';
import Project from '@/model/Project';
import TargetAudienceList from '../components/pages/project/TargetAudienceList.vue';
import { useBreadcrumbStore } from '@/stores/BreadcrumbStore';
import type { MenuItem } from 'primevue/menuitem';

const route = useRoute();
const breadcrumb = useBreadcrumbStore();
const project: Ref<Project | null> = ref(null);
const id = Number.parseInt(route.params.id as string);

onMounted(async () => {
	// TODO: Move to project service.
	const p = new Project();
	p.Id = id;
	p.Name = 'My Project';
	project.value = p;

	breadcrumb.$patch((state) => {
		const crumbs: MenuItem[] = [
			{
				label: p.Name,
				to: route.path,
			},
		];

		state.items = crumbs;
	});
});
</script>

<template>
	<div class="project-container">
		<div class="overview">
			<ProjectOverview :project="project" />
		</div>

		<!-- <div class="project-details"> -->
		<div class="configuration-container">
			<div>
				<TargetAudienceList
					v-if="project"
					v-model:target-audiences="project.TargetAudiences"
					:project-id="id"
				/>
			</div>
		</div>

		<div class="pricing-container">
			<Pricing />
		</div>
		<!-- </div> -->
	</div>
</template>

<style scoped lang="scss">
@use '@/assets/variables.scss' as *;

.project-container {
	display: grid;
	grid-gap: 20px 20px;
	grid-template-columns: 4fr 1fr;

	@media screen and (max-width: $lg) {
		grid-template-columns: 2fr 1fr;
	}

	@media screen and (max-width: $md) {
		grid-template-columns: 1fr;
	}

	& > div {
		width: 100%;
	}
}

.overview {
	@media screen and (min-width: $md) {
		grid-column: span 2;
	}
}

.configuration-container {
	display: flex;
	flex-direction: column;
}

.pricing-container {
	min-width: 10rem;
	min-height: 18rem;

	@media screen and (min-width: $lg) {
		height: 20vh;
	}

	@media screen and (min-width: $xl) {
		height: 25vh;
	}
}
</style>
