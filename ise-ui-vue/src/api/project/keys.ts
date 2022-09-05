import type { Ref } from "vue";

export const ProjectKeys = {
	all: ["project"] as const,
	project: (id: number) => [...ProjectKeys.all, id] as const,
	targetAudiences: (id: number, page: Ref<number>) =>
		[...ProjectKeys.project(id), "target-audiences", page] as const,
	surveys: (id: number) => [...ProjectKeys.project(id), "surveys"],
};
