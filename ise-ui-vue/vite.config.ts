import { fileURLToPath, URL } from "url";

import vue from "@vitejs/plugin-vue";
import dns from "dns";
import { defineConfig } from "vite";

dns.setDefaultResultOrder("verbatim");

// https://vitejs.dev/config/
export default defineConfig({
	plugins: [vue()],
	resolve: {
		alias: {
			"@": fileURLToPath(new URL("./src", import.meta.url)),
		},
	},
	server: {
		host: "localhost",
		port: 3000,
	},
});
