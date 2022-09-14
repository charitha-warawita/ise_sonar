export const ProjectKeys = {
	all: ["project"] as const,
	project: (id: number) => [...ProjectKeys.all, id] as const,
	targetAudiences: (id: number) =>
		[...ProjectKeys.project(id), "target-audiences"] as const,
	surveys: (id: number) => [...ProjectKeys.project(id), "surveys"],
	currentCost: (id: number) => [...ProjectKeys.project(id), "current-cost"],
};
