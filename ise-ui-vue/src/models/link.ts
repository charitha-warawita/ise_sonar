import { z } from "zod";
import { LinkKeySchema } from "./enums/linkKeyEnum";

export const LinkSchema = z.object({
	surveyId: z.number().int().nonnegative(),
	key: LinkKeySchema,
	value: z.string().url(),
	type: z.string().min(1),
});

export type Link = z.infer<typeof LinkSchema>;
