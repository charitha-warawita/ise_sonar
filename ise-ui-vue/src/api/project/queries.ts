import { useAxiosAsync } from "@/composables/axios";
import { ProjectSchema } from "@/models/project";
import { SurveySchema } from "@/models/survey";
import { TargetAudienceSchema } from "@/models/targetAudience";
import { z } from "zod";
import { PagedResponseSchema } from "../responseSchemas";

export function useProjectQueries() {
	const GetProject = async (id: number) => {
		const axios = await useAxiosAsync();

		return await axios
			.get(`/Project/${id}`)
			.then((response) => ProjectSchema.parse(response.data));
	};

	const GetProjectTargetAudiences = async (
		id: number,
		page: number,
		pageSize = 5
	) => {
		const axios = await useAxiosAsync();
		const schema = PagedResponseSchema(TargetAudienceSchema);

		return await axios
			.get(`/Project/${id}/TargetAudiences`, {
				params: { page, pageSize },
			})
			.then((response) => schema.parse(response.data));
	};

	const GetSurveys = async (id: number) => {
		const axios = await useAxiosAsync();

		return await axios
			.get(`/Project/${id}/Surveys`)
			.then((response) => z.array(SurveySchema).parse(response.data));
	};

	return {
		GetProject,
		GetProjectTargetAudiences,
		GetSurveys,
	};
}