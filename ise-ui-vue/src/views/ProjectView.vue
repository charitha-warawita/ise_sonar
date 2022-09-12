<script setup lang="ts">
import { ProjectKeys } from "@/api/project/keys";
import { useProjectQueries } from "@/api/project/queries";
import TargetAudienceList from "@/components/target-audiences/TargetAudienceList.vue";
import { format } from "date-fns";
import { reactive } from "vue";
import { useQuery } from "vue-query";
import { useRoute } from "vue-router";
import BsCard from "../components/bootstrap/BsCard.vue";
import CollapsibleCard from "../components/general/CollapsibleCard.vue";
import SurveyList from "../components/surveys/SurveyList.vue";

const route = useRoute();
const projectQueries = useProjectQueries();

const id = parseInt(route.params.id as string);
const sections = reactive({
	ta: true,
	survey: false,
});

const project = useQuery(ProjectKeys.project(id), () =>
	projectQueries.GetProject(id)
);

const prices = useQuery(ProjectKeys.currentCost(id), () =>
	projectQueries.GetCurrentCost(id)
);

const ManageProject = () => {
	console.log("manage button clicked");
};
</script>

<template>
	<div
		v-if="project.isLoading.value"
		class="h-100 d-flex align-items-center justify-content-center my-3"
	>
		<div>
			<div class="spinner-border" role="status">
				<span class="visually-hidden">Loading...</span>
			</div>
		</div>
	</div>
	<div v-else-if="project.isError.value">An error has occurred</div>
	<div v-else-if="project.data.value" class="mb-3">
		<div class="container-flush mt-3">
			<bs-card>
				<template #header>
					<div class="d-flex flex-row">
						<h5 class="flex-grow-1" style="margin: auto 0">
							{{ project.data.value.name }}
						</h5>

						<div>
							<button
								type="button"
								class="btn btn-outline-success"
								@click="ManageProject"
							>
								Manage
							</button>
						</div>
					</div>
				</template>

				<template #body>
					<div class="container">
						<h6>Details:</h6>

						<div class="row gx-5">
							<div class="col">
								<b>ID: </b
								><span>{{ project.data.value.id }}</span>
							</div>
							<div class="col">
								<b>Maconomy Number: </b
								><span>{{ project.data.value.reference }}</span>
							</div>
						</div>
						<div class="row gx-5">
							<div class="col">
								<b>User: </b>
								<span>{{
									project.data.value.user ?? "Unknown"
								}}</span>
							</div>

							<div class="col">
								<b>Fielding Period: </b>
								<span>{{
									project.data.value.fieldingPeriod
								}}</span>
							</div>
						</div>
						<div class="row gx-5">
							<div class="col">
								<b>Start Date: </b>
								<span v-if="project.data.value.startDate">{{
									format(
										project.data.value.startDate,
										"yyyy-MM-dd hh:mm aa"
									)
								}}</span>
							</div>

							<div class="col">
								<b>Last Update: </b>
								<span v-if="project.data.value.lastUpdate">
									{{
										format(
											project.data.value.lastUpdate,
											"yyyy-MM-dd hh:mm aa"
										)
									}}
								</span>
							</div>
						</div>
					</div>

					<div class="container">
						<hr />
					</div>

					<div class="container">
						<h6>Pricing:</h6>

						<div class="row">
							<div
								v-if="prices.isLoading.value"
								class="placeholder-glow"
							>
								<span class="placeholder col-1"></span>
							</div>
							<div v-else-if="prices.isError.value">
								<p>
									An error occurred whilst retrieving price
									data.
								</p>
							</div>
							<div v-else-if="prices.data.value">
								<div v-if="prices.data.value.length === 0">
									No Pricing data available.
								</div>
								<div
									v-else
									v-for="price in prices.data.value"
									:key="price.currency"
									class="col"
								>
									<b>{{ price.currency }}:</b>
									{{
										new Intl.NumberFormat(undefined, {
											style: "currency",
											currency: price.currency,
										}).format(price.amount)
									}}
								</div>
							</div>
						</div>
					</div>
				</template>
			</bs-card>
		</div>

		<CollapsibleCard
			name="ta-list"
			title="Target Audiences"
			class="mt-3"
			v-model:expanded="sections.ta"
		>
			<template #body>
				<div class="container-fluid px-0" v-if="project.data.value">
					<TargetAudienceList :project-id="project.data.value.id" />
				</div>
				<div class="container" v-else>
					<p>No Target Audiences</p>
				</div>
			</template>
		</CollapsibleCard>

		<CollapsibleCard
			name="survey-list"
			title="Surveys"
			class="mt-3"
			v-model:expanded="sections.survey"
		>
			<template #body>
				<SurveyList
					:project-id="id"
					:enabled="sections.survey"
				></SurveyList>
			</template>
		</CollapsibleCard>
	</div>
</template>

<style scoped>
#link-button {
	text-decoration: none;
}
</style>
