import { z } from "zod";
import { StatusEnum } from "./enums/statusEnum";
import { TargetAudienceSchema } from "./targetAudience";

export const ProjectSchema = z
	.object({
		id: z.number().int().positive(),
		name: z.string().min(1),
		reference: z.string().nullable(),
		user: z.object({}).nullable(),
		lastUpdate: z.date().nullable(),
		startDate: z.date().nullable(),
		fieldingPeriod: z.number().int().positive().nullable(),
		status: StatusEnum,
		categories: z.object({}).array().nullable(), // Placeholder
		targetAudiences: TargetAudienceSchema.array().nullable(),
	})
	.strict();

export type Project = z.infer<typeof ProjectSchema>;
