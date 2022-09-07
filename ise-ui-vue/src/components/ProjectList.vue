<template>
	<div>
		<ul class="nav nav-pills nav-fill border-bottom">
			<li class="nav-item">
				<a
					@click="applyStatusFilter(7)"
					:class="[currentStatus === 7 ? 'active' : '']"
					class="nav-link"
					aria-current="page"
					href="#"
					>All</a
				>
			</li>
			<li class="nav-item">
				<a
					@click="applyStatusFilter(2)"
					:class="[currentStatus === 2 ? 'active' : '']"
					class="nav-link"
					href="#"
					>Active</a
				>
			</li>
			<li class="nav-item">
				<a
					@click="applyStatusFilter(1)"
					:class="[currentStatus === 1 ? 'active' : '']"
					class="nav-link"
					href="#"
					>Created</a
				>
			</li>
			<li class="nav-item">
				<a
					@click="applyStatusFilter(0)"
					:class="[currentStatus === 0 ? 'active' : '']"
					class="nav-link"
					href="#"
					>Draft</a
				>
			</li>
			<li class="nav-item">
				<a
					@click="applyStatusFilter(5)"
					:class="[currentStatus === 5 ? 'active' : '']"
					class="nav-link"
					href="#"
					>Closed</a
				>
			</li>
		</ul>
		<br /><br />
		<div class="d-flex">
			<input
				class="form-control me-2"
				type="name"
				placeholder="Type name here"
				v-model="searchByName"
			/>
			<!--<input class="form-control me-2" type="date" placeholder="Start date  ." aria-label="Date" v-model="searchByStartDate">-->
			<a
				class="btn btn-outline-success searchButton me-2"
				@click="
					useProjsStore.getProjectsBySearchNameAndStartDate(
						useProjsStore.currentStatus
					)
				"
				>Filter</a
			>
			<a
				class="btn btn-outline-success searchButton me-2"
				@click="useProjsStore.setDefaultProjectList"
				>Clear</a
			>
		</div>
		<br /><br />
		<ProjectListTable
			class="projListTable"
			:fields="displayfields"
			:projects="useProjsStore.currentProjects"
			:field-titles="displayFieldFormatted"
			selectable
			@row-selected="RowSelectedHandler"
		></ProjectListTable>
		<div v-if="currentProjects" style="width: 100%; text-align: center">
			<!-- <div style="float: left">Page: {{ currentPageNumber }}</div> -->
			<vue-awesome-paginate
				:total-items="totalItems"
				:items-per-page="currentPageRowCount"
				:max-pages-shown="4"
				:current-page="currentPageNumber"
				:on-click="PageSelectHandler"
				hide-prev-next
			/>
		</div>

		<p v-if="projectListLoading">Loading project list..</p>
		<p v-if="projectListError">{{ projectListError }}</p>
	</div>
</template>
<script setup>
import ProjectListTable from "@/components/ProjectListTable.vue";
import ProjectModel from "@/models/projectModel.js";
import { useProjectsStore } from "@/stores/projectsStore";
import { storeToRefs } from "pinia";
import { onMounted } from "vue";
import { useRouter } from "vue-router";

const router = useRouter();

// console.log(storeToRefs(useProjectStore()).projects);
const useProjsStore = useProjectsStore();
const {
	searchByName,
	searchByStartDate,
	currentProjects,
	currentStatus,
	projectListLoading,
	projectListError,
	currentPageRowCount,
	currentPageNumber,
	totalItems,
} = storeToRefs(useProjsStore);
const displayfields = ProjectModel.displayfields;
const displayFieldFormatted = ProjectModel.displayFieldFormatted;

const PageSelectHandler = async (page) => {
	useProjsStore.currentPageNumber = page;
	useProjsStore.currentProjects = await useProjsStore.GetProjectsList();
};

const RowSelectedHandler = (project) => {
	router.push({
		name: "project",
		params: {
			id: project.id,
		},
	});
};

function applyStatusFilter(status) {
	useProjsStore.getProjectsBySearchNameAndStartDate(status);
	document.querySelectorAll(".paginate-buttons").forEach((button) => {
		button.classList.remove("active-page");

		if (button.innerText === "1") {
			button.click();
		}
	});
}
onMounted(() => {
	useProjsStore.setDefaultProjectList();
});
</script>
<style scoped>
input[type="date"]:not(.has-value):before {
	color: gray;
	content: attr(placeholder);
}

.projListTable {
	box-shadow: rgba(0, 0, 0, 0.16) 0px 1px 4px;
}

::v-deep(.pagination-container) {
	display: flex;
	column-gap: 10px;
}

::v-deep(.paginate-buttons) {
	height: 40px;
	width: 40px;
	border-radius: 20px;
	cursor: pointer;
	background-color: rgb(242, 242, 242);
	border: 1px solid rgb(217, 217, 217);
	color: black;
}

::v-deep(.paginate-buttons:hover) {
	background-color: #d8d8d8;
}

::v-deep(.active-page) {
	background-color: #0d6efd;
	border: 1px solid #0d6efd;
	color: white;
}

::v-deep(.active-page:hover) {
	background-color: #0d6efd;
}
</style>
