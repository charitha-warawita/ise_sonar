/* eslint-disable @typescript-eslint/no-explicit-any */
import axios from "axios";
import { parseISO } from "date-fns";

const base = import.meta.env.VITE_ISE_API_URL;

function isIsoDateString(value: any): boolean {
	const isoDateFormat =
		/^\d{4}-\d{2}-\d{2}T\d{2}:\d{2}:\d{2}(?:\.\d*)?(?:[-+]\d{2}:?\d{2}|Z)?$/;

	return value && typeof value === "string" && isoDateFormat.test(value);
}

// Parses the response body, looking for date strings in the response, converting as necessary.
const transformDates = (body: any) => {
	if (body === null || body === undefined || typeof body !== "object")
		return body;

	for (const key of Object.keys(body)) {
		const value = body[key];

		if (isIsoDateString(value)) body[key] = parseISO(value);
		else if (typeof value === "object") transformDates(value);
	}
};

export function useAxios() {
	// Can use any authentication stores that we may need here.

	const instance = axios.create({
		baseURL: `${base}/api`,
	});

	instance.interceptors.response.use((response) => {
		transformDates(response.data);

		return response;
	});

	return instance;
}
