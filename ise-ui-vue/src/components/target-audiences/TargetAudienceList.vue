<script setup lang="ts">
import { useProjectQueries } from "@/api/project/queries";
import BsAccordion from "@/components/bootstrap/BsAccordion.vue";
import StyledPaginator from "@/components/general/StyledPaginator.vue";
// eslint-disable-next-line @typescript-eslint/no-unused-vars
import type { TargetAudience } from "@/models/targetAudience";
import { computed, ref } from "vue";

const projectQueries = useProjectQueries();

const props = defineProps<{ projectId: number }>();

const pagesize = 5;
const page = ref(1);

const { isLoading, isError, data } = projectQueries.GetProjectTargetAudiences(
	props.projectId,
	page,
	pagesize
);

const total = computed(() => data.value?.totalResults ?? 0);

const PageSelectHandler = (p: number) => {
	page.value = p;

	console.log(page.value);
};
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
			<div v-if="data.result.length">
				<bs-accordion flush :items="data.result">
					<template #header="{ item }: { item: TargetAudience }">
						{{ item.name }}
					</template>

					<template #body="{ item }: { item: TargetAudience }">
						<div class="container">
							<div>Id: {{ item.id }}</div>
							<div>Name: {{ item.name }}</div>
							<div>
								Audience Number: {{ item.audienceNumber }}
							</div>
							<div>Esitimated IR: {{ item.estimatedIR }}</div>
							<div>Esimated LOI: {{ item.estimatedLOI }}</div>
							<div>Limit: {{ item.limit }}</div>
						</div>
					</template>
				</bs-accordion>
			</div>
			<div v-else class="no-audiences-message">
				<span>No Target Audiences To Display.</span>
			</div>
		</div>

		<div>
			<div class="pager-container">
				<styled-paginator
					:total-items="total"
					:items-per-page="pagesize"
					:max-pages-shown="4"
					:current-page="page"
					:on-click="PageSelectHandler"
					hide-prev-next-when-ends
				></styled-paginator>
			</div>
		</div>
	</div>
</template>

<style scoped>
.spinner-container {
	min-height: 10vh;
}

.pager-container {
	display: flex;
	justify-content: center;
	padding: 10px 0;
	border-top: 1px solid rgb(217, 217, 217);
}

.no-audiences-message {
	text-align: center;
	margin: 30px 0;
}
</style>
