import { z } from 'zod';

export const TargetAudienceSchema = z
	.object({
		Id: z.number().int().min(-1).optional().default(-1),
		ProjectId: z.number().int().positive(),
		Name: z.string().min(1),
		EstimatedIR: z.number().int().positive(),
		EstimatedLOI: z.number().int().positive(),
		Limit: z.number().int().positive(),
	})
	.strict();

export type TargetAudience = z.infer<typeof TargetAudienceSchema>;
