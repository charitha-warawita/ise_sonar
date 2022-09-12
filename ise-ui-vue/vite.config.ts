import { fileURLToPath, URL } from "url";

import vue from "@vitejs/plugin-vue";
import dns from "dns";
import { visualizer } from "rollup-plugin-visualizer";
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
	build: {
		rollupOptions: {
			plugins: [visualizer()],
			output: {
				manualChunks: function (id) {
					if (id.includes("@azure")) return "msal"; // ~300KiB

					if (id.includes("node_modules")) return "vendor"; // ~360KiB, split further if necessary.
				},
			},
		},
	},
});
