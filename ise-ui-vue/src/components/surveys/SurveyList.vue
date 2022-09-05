<script setup lang="ts">
import { ProjectKeys } from "@/api/project/keys";
import { useProjectQueries } from "@/api/project/queries";
import { LinkKeySchema } from "@/models/enums/linkKeyEnum";
import type { Link } from "@/models/link";
import { useQuery } from "vue-query";

const projectQueries = useProjectQueries();

const props = defineProps<{
	projectId: number;
}>();

const { isLoading, data } = useQuery(ProjectKeys.surveys(props.projectId), () =>
	projectQueries.GetSurveys(props.projectId)
);

const linkKeys = [
	LinkKeySchema.enum.EntryUrl,
	LinkKeySchema.enum.TestCompleteSurvey,
];
const FilterLinks = (links: Link[]) => {
	return links.filter((l) => linkKeys.includes(l.key));
};
</script>

<template>
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
	<div v-else-if="data != undefined" class="card card-body">
		<div v-if="data.length == 0">No Surveys exist for this Project.</div>
		<div v-else v-for="(survey, i) in data" :key="i">
			<div>
				<b>{{ survey.name }}</b>
			</div>

			<div>Links:</div>
			<div class="row">
				<div
					v-for="link in FilterLinks(survey.links)"
					:key="link.key"
					class="col col-md-3"
				>
					<a :href="link.value">{{ link.key }}</a>
				</div>
			</div>
			<div v-if="i != data.length - 1">
				<hr />
			</div>
		</div>
	</div>
</template>

<style scoped></style>
