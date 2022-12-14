import { z } from "zod";

export const TargetAudienceSchema = z
	.object({
		id: z.number().int().positive(),
		name: z.string().min(1),
		audienceNumber: z.number().int().positive(),
		estimatedIR: z.number().int().nonnegative(),
		estimatedLOI: z.number().int().positive(),
		limit: z.number().int().nonnegative(),
		limitType: z.number().int().positive().nullable().optional(),
		testingUrl: z.string().min(1).optional().nullable(),
		liveUrl: z.string().min(1).optional().nullable(),
		qualifications: z.object({}).array().nullable(), // Placeholder
		quotas: z.object({}).array().nullable(), // Placeholder
	})
	.strict();

export type TargetAudience = z.infer<typeof TargetAudienceSchema>;
