<script setup>
import BsAccordion from "@/components/bootstrap/BsAccordion.vue";
import StyledPaginator from "@/components/general/StyledPaginator.vue";
import { useAxios } from "@/composables/axios";
import { computed, ref, watchEffect } from "vue";

const axios = useAxios();
const props = defineProps({
	projectId: {
		type: Number,
		required: true,
	},
});

const data = ref([]);
const total = ref();
const page = ref(1);
const pagesize = 5;
const loading = ref(false);
const pagedData = computed(() => data.value[page.value]);

watchEffect(async () => {
	if (pagedData.value !== undefined) return;

	loading.value = true;

	await axios
		.get("/TargetAudience", {
			params: {
				page: page.value,
				pageSize: pagesize,
				projectId: props.projectId,
			},
		})
		.then((response) => {
			data.value[page.value] = response.data.result;
			total.value = response.data.totalResults;
		})
		.catch((err) => console.error(err))
		.finally(() => (loading.value = false));
});

const PageSelectHandler = (p) => {
	page.value = p;
};
</script>

<template>
	<div>
		<div v-if="!loading">
			<div v-if="pagedData.length">
				<bs-accordion flush :items="pagedData">
					<template #header="{ item }">
						{{ item.name }}
					</template>

					<template #body="{ item }">
						<div class="container">
							<div>Name: {{ item.name }}</div>
							<div>
								Audience Number: {{ item.audienceNumber }}
							</div>
							<div>Esitimated IR: {{ item.estimatedIR }}</div>
							<div>Esimated LOI: {{ item.esimatedLOI }}</div>
							<div>Limit: {{ item.limit }}</div>
						</div>
					</template>
				</bs-accordion>
			</div>
			<div v-else class="no-audiences-message">
				<span>No Target Audiences To Display.</span>
			</div>
		</div>
		<div v-else class="spinner-container">
			<div class="position-absolute top-50 start-50 translate-middle">
				<div class="spinner-border" role="status">
					<span class="visually-hidden">Loading...</span>
				</div>
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
