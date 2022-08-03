<script setup lang="ts">
import { type Ref, ref, onMounted } from 'vue';
import { useRoute } from 'vue-router';
import Pricing from '../components/pages/project/Pricing.vue';
import ProjectOverview from '../components/pages/project/ProjectOverview.vue';
import Project from '@/model/Project';
import TargetAudienceList from '../components/pages/project/TargetAudienceList.vue';

const route = useRoute();
const project: Ref<Project | null> = ref(null);

onMounted(async () => {
	const id = route.params.id as string;

	// TODO: Move to project service.
	const p = new Project();
	p.Id = Number.parseInt(id);
	p.Name = 'My Project';

	project.value = p;
});
</script>

<template>
	<div class="project-container">
		<div class="overview">
			<ProjectOverview :project="project" />
		</div>

		<div class="project-details">
			<div class="configuration-container">
				<div>
					<TargetAudienceList v-if="project" v-model:target-audiences="project.TargetAudiences" />
				</div>
			</div>

			<div class="pricing-container">
				<Pricing />
			</div>
		</div>
	</div>
</template>

<style scoped>
.project-container {
	flex-direction: column;
}

.overview {
	flex-grow: 1;
	margin-bottom: 50px;
}

.project-details {
	flex-grow: 1;
	display: grid;
	grid-template-columns: 3.5fr 1fr;
}

.configuration-container {
	flex-grow: 0.85;
	display: flex;
	flex-direction: column;
	margin-right: 10px;
}

.pricing-container {
	margin-left: 10px;
}

@media screen and (min-width: 992px) {
	.configuration-container {
		flex-grow: 0.65;
	}

	.pricing-container {
		flex-grow: 0.35;
		height: 20vh;
	}
}

@media screen and (min-width: 1200px) {
	.configuration-container {
		flex-grow: 0.85;
	}

	.pricing-container {
		flex-grow: 0.15;
		height: 25vh;
	}
}
</style>
