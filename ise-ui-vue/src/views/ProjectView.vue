<script setup>
import TargetAudienceList from "@/components/target-audiences/TargetAudienceList.vue";
import { useAxios } from "@/composables/axios";
import { onMounted, ref } from "vue";
import { useRoute } from "vue-router";

const route = useRoute();
const axios = useAxios();

const project = ref();

onMounted(async () => {
	const id = route.params.id;

	await axios
		.get(`/Project/${id}`)
		.then((response) => (project.value = response.data))
		.catch((err) => console.error(err.toJSON()));
});

const ManageProject = () => {
	console.log("manage button clicked");
};
</script>

<template>
	<div v-if="project">
		<div class="mt-3">
			<div class="d-flex flex-row">
				<h5 class="flex-grow-1" style="margin: auto 0">
					{{ project.name }}
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

			<div class="container">
				<div class="row gx-5">
					<div class="col">
						<b>ID: </b><span>{{ project.id }}</span>
					</div>
					<div class="col">
						<b>Maconomy Number: </b
						><span>{{ project.reference }}</span>
					</div>
				</div>
				<div class="row gx-5">
					<div class="col">
						<b>User: </b>
						<span>{{ project.user ?? "Unknown" }}</span>
					</div>

					<div class="col">
						<b>Fielding Period: </b>
						<span>{{ project.fieldingPeriod }}</span>
					</div>
				</div>
				<div class="row gx-5">
					<div class="col">
						<b>Start Date: </b>
						<span>{{ project.startDate }}</span>
					</div>

					<div class="col">
						<b>Last Update: </b>
						<span>{{ project.lastUpdate }}</span>
					</div>
				</div>
			</div>
		</div>

		<div class="card mt-3">
			<div class="card-header">
				<h5>Target Audiences</h5>
			</div>

			<div class="container-fluid px-0" v-if="project">
				<TargetAudienceList :project-id="project.id" />
			</div>
			<div class="container" v-else>
				<p>No Target Audiences</p>
			</div>
		</div>
	</div>
	<div
		v-else
		class="h-100 d-flex align-items-center justify-content-center my-3"
	>
		<div>
			<div class="spinner-border" role="status">
				<span class="visually-hidden">Loading...</span>
			</div>
		</div>
	</div>
</template>

<style scoped></style>
