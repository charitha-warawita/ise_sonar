import { z } from 'zod';

export const ProjectStatusEnum = z.enum(['Draft', 'Created', 'Started', 'Completed', 'Cancelled']);
export type ProjectStatusEnum = z.infer<typeof ProjectStatusEnum>;
