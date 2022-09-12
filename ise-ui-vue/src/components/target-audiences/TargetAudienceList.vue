<script setup lang="ts">
import { ProjectKeys } from "@/api/project/keys";
import { useProjectQueries } from "@/api/project/queries";
import BsAccordion from "@/components/bootstrap/BsAccordion.vue";
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import type { TargetAudience } from "@/models/targetAudience";
import { useQuery } from "vue-query";

const projectQueries = useProjectQueries();

const props = defineProps<{ projectId: number }>();

const { isLoading, isError, data } = useQuery(
	ProjectKeys.targetAudiences(props.projectId),
	() => projectQueries.GetProjectTargetAudiences(props.projectId)
);
</script>

<template>
	<div>
		<div v-if="isLoading" class="spinner-container">
			<div class="position-absolute top-50 start-50 translate-middle">
				<div class="spinner-border" role="status">
					<span class="visually-hidden">Loading...</span>
				</div>
			</div>
		</div>
		<div v-else-if="isError">
			An error occurred whilst fetching Target Audiences.
		</div>
		<div v-else-if="data">
			<div v-if="data.length">
				<bs-accordion flush :items="data">
					<template #header="{ item }: { item: TargetAudience }">
						{{ item.name }}
					</template>

					<template #body="{ item }: { item: TargetAudience }">
						<div class="container mx-2">
							<div class="row">
								<div class="col">
									<div>
										<b>Id:</b>
										{{ item.id }}
									</div>
								</div>
								<div class="col">
									<div>
										<b>Name:</b>
										{{ item.name }}
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col">
									<div>
										<b>Audience Number:</b>
										{{ item.audienceNumber }}
									</div>
								</div>
								<div class="col">
									<div>
										<b>Esitimated IR:</b>
										{{ item.estimatedIR }}
									</div>
								</div>
							</div>
							<div class="row">
								<div class="col">
									<div>
										<b>Esimated LOI:</b>
										{{ item.estimatedLOI }}
									</div>
								</div>
								<div class="col">
									<div>
										<b>Limit:</b>
										{{ item.limit }}
									</div>
								</div>
							</div>
						</div>
					</template>
				</bs-accordion>
			</div>
			<div v-else class="no-audiences-message">
				<span>No Target Audiences To Display.</span>
			</div>
		</div>
	</div>
</template>

<style scoped>
.spinner-container {
	min-height: 10vh;
}

.no-audiences-message {
	text-align: center;
	margin: 30px 0;
}
</style>
