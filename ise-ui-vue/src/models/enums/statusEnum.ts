import { z } from "zod";

export enum Status {
	Draft = 0,
	Created = 1,
	Live = 2,
	Paused = 3,
	Complete = 4,
	Closed = 5,
	Halted = 6,
}

export const StatusEnum = z.nativeEnum(Status);
