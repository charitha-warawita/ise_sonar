import { z } from "zod";
import { LinkSchema } from "./link";

export const SurveySchema = z.object({
	name: z.string().min(1),
	links: LinkSchema.array(),
});

export type Survey = z.infer<typeof SurveySchema>;
