/* eslint-disable @typescript-eslint/no-explicit-any */
import auth from "@/services/auth";
import axios from "axios";
import { parseISO } from "date-fns";

const CORE_API_BASE = import.meta.env.VITE_ISE_API_URL;
const CORE_API_SCOPES = [import.meta.env.VITE_CORE_API_SCOPE];

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

export async function useAxiosAsync() {
	// Can use any authentication stores that we may need here.
	const token = await auth.acquireToken(CORE_API_SCOPES);

	const instance = axios.create({
		baseURL: `${CORE_API_BASE}/api`,
		headers: {
			Authorization: `bearer ${token}`,
		},
	});

	instance.interceptors.response.use((response) => {
		transformDates(response.data);

		return response;
	});

	return instance;
}
