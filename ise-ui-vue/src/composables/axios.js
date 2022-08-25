import axios from "axios";

const base = import.meta.env.VITE_ISE_API_URL;

export function useAxios() {
	// Can use any authentication stores that we may need here.

	const instance = axios.create({
		baseURL: `${base}/api`,
	});

	return instance;
}
