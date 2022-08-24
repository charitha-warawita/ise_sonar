import { z } from 'zod';

export const User = z
	.object({
		Id: z.number().int().positive(),
		UserName: z.string().min(1),
		FirstName: z.string().min(1),
		LastName: z.string().min(1),
	})
	.strict();
