<script setup lang="ts">
import { ProjectKeys } from "@/api/project/keys";
import { useProjectQueries } from "@/api/project/queries";
import TargetAudienceList from "@/components/target-audiences/TargetAudienceList.vue";
import { format } from "date-fns";
import { ref } from "vue";
import { useQuery } from "vue-query";
import { useRoute } from "vue-router";
import SurveyList from "../components/surveys/SurveyList.vue";
import TopHeader from '../components/TopHeader.vue';

const route = useRoute();
const projectQueries = useProjectQueries();

const id = parseInt(route.params.id as string);

const { isLoading, isError, data } = useQuery(ProjectKeys.project(id), () =>
	projectQueries.GetProject(id)
);

const ManageProject = () => {
	console.log("manage button clicked");
};

const expanded = ref(false);
const SurveysClicked = () => {
	expanded.value = !expanded.value;
};
</script>

<template>
	<TopHeader/>
	<div
		v-if="isLoading"
		class="h-100 d-flex align-items-center justify-content-center my-3"
	>
		<div>
			<div class="spinner-border" role="status">
				<span class="visually-hidden">Loading...</span>
			</div>
		</div>
	</div>
	<div v-else-if="isError">An error has occurred</div>
	<div v-else-if="data">
		<div class="mt-3 card">
			<div class="d-flex flex-row card-header">
				<h5 class="flex-grow-1" style="margin: auto 0">
					{{ data.name }}
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

			<div class="container card-body">
				<div class="row gx-5">
					<div class="col">
						<b>ID: </b><span>{{ data.id }}</span>
					</div>
					<div class="col">
						<b>Maconomy Number: </b
						><span>{{ data.reference }}</span>
					</div>
				</div>
				<div class="row gx-5">
					<div class="col">
						<b>User: </b>
						<span>{{ data.user ?? "Unknown" }}</span>
					</div>

					<div class="col">
						<b>Fielding Period: </b>
						<span>{{ data.fieldingPeriod }}</span>
					</div>
				</div>
				<div class="row gx-5">
					<div class="col">
						<b>Start Date: </b>
						<span v-if="data.startDate">{{
							format(data.startDate, "yyyy-MM-dd hh:mm aa")
						}}</span>
					</div>

					<div class="col">
						<b>Last Update: </b>
						<span v-if="data.lastUpdate">
							{{ format(data.lastUpdate, "yyyy-MM-dd hh:mm aa") }}
						</span>
					</div>
				</div>
				<div class="row">
					<div class="col">
						<a
							id="link-button"
							role="button"
							data-bs-toggle="collapse"
							href="#surveys"
							aria-expanded="false"
							aria-controls="links"
							@click="SurveysClicked"
						>
							{{ expanded ? "Hide" : "Show" }} Surveys
						</a>

						<div class="collapse" id="surveys">
							<SurveyList v-if="expanded" :project-id="id" />
						</div>
					</div>
				</div>
			</div>
		</div>

		<div class="card mt-3">
			<div class="card-header">
				<h5>Target Audiences</h5>
			</div>

			<div class="container-fluid px-0" v-if="data">
				<TargetAudienceList :project-id="data.id" />
			</div>
			<div class="container" v-else>
				<p>No Target Audiences</p>
			</div>
		</div>
	</div>
</template>

<style scoped>
#link-button {
	text-decoration: none;
}
</style>