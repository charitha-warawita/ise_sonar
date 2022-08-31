import { useAxios } from "@/composables/axios";
import { ProjectSchema } from "@/models/project";
import { TargetAudienceSchema } from "@/models/targetAudience";
import type { Ref } from "vue";
import { useQuery } from "vue-query";
import { PagedResponseSchema } from "../responseSchemas";
import { ProjectKeys } from "./keys";

export function useProjectQueries() {
	const GetProject = (id: number) => {
		const axios = useAxios();

		return useQuery(ProjectKeys.project(id), () =>
			axios
				.get(`/Project/${id}`)
				.then((response) => ProjectSchema.parse(response.data))
		);
	};

	const GetProjectTargetAudiences = (
		id: number,
		page: Ref<number>,
		pageSize = 5
	) => {
		const axios = useAxios();
		const schema = PagedResponseSchema(TargetAudienceSchema);

		return useQuery(
			ProjectKeys.targetAudiences(id, page),
			async () =>
				await axios
					.get(`/Project/${id}/TargetAudiences`, {
						params: { page: page.value, pageSize },
					})
					.then((response) => schema.parse(response.data)),
			{
				keepPreviousData: true,
			}
		);
	};

	return {
		GetProject,
		GetProjectTargetAudiences,
	};
}
