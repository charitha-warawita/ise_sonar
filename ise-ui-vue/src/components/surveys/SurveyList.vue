<script setup lang="ts">
import { ProjectKeys } from "@/api/project/keys";
import { useProjectQueries } from "@/api/project/queries";
import { LinkKeySchema } from "@/models/enums/linkKeyEnum";
import type { Link } from "@/models/link";
import { computed } from "vue";
import { useQuery } from "vue-query";

const projectQueries = useProjectQueries();

const props = defineProps<{
	projectId: number;
	enabled?: boolean;
}>();

const enabled = computed(() =>
	props.enabled == undefined ? true : props.enabled
);

const { isLoading, data } = useQuery(
	ProjectKeys.surveys(props.projectId),
	() => projectQueries.GetSurveys(props.projectId),
	{
		enabled,
	}
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
	<div>
		<div
			v-if="isLoading"
			style="min-height: 88px"
			class="d-flex align-items-center justify-content-center my-auto"
		>
			<div>
				<div class="spinner-border" role="status">
					<span class="visually-hidden">Loading...</span>
				</div>
			</div>
		</div>
		<ul v-else-if="data != undefined" class="list-group list-group-flush">
			<div v-if="data.length == 0">
				No Surveys exist for this Project.
			</div>
			<li
				v-else
				v-for="(survey, i) in data"
				:key="i"
				class="list-group-item"
			>
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
			</li>
		</ul>
	</div>
</template>

<style scoped></style>
