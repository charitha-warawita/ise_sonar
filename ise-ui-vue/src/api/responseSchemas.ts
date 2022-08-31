import { z, ZodType } from "zod";

/**
 * Genereates a PagedResponse schema, with the result[] of the given type.
 *
 * @param type - The ZodType schema of the result.
 */
export const PagedResponseSchema = (type: ZodType) => {
	const schema = z.object({
		result: z.array(type),
		totalResults: z.number().int().positive(),
	});

	return schema;
};
