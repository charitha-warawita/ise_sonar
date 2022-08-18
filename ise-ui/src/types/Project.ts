import { z } from 'zod';
import { TargetAudienceSchema } from './TargetAudience';

export const ProjectSchema = z
	.object({
		Id: z.number().int().min(-1).optional().default(-1),
		Name: z.string().min(1),
		MaconomyNumber: z.string().min(1),
		Owner: z.string().min(1),
		CreationDate: z.date(),
		LastActivity: z.date(),
		StartDate: z.date(),
		EndDate: z.date(),
		TargetAudiences: z.array(TargetAudienceSchema).optional().default([]),
	})
	.strict();

export type Project = z.infer<typeof ProjectSchema>;
