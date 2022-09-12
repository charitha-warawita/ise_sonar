import { z } from "zod";

export const CostSchema = z.object({
	amount: z.number(),
	currency: z.string().length(3),
});

export type Cost = z.infer<typeof CostSchema>;
