import { z } from 'zod';

export const ProgressCardData = z.object({
	icon: z.string().optional(),
	type: z.enum(['Goal', 'Expected']),
	operator: z.enum(['gt', 'lt']).optional(),
	title: z.string().min(1),
	value: z.number().nonnegative(),
	target: z.number().int().nonnegative().optional(),
	description: z.string().min(1).optional(),
});

export type ProgressCardData = z.infer<typeof ProgressCardData>;
