import { createPinia } from "pinia";
import { createApp } from "vue";
import VueAwesomePaginate from "vue-awesome-paginate";
import "vue-awesome-paginate/dist/style.css";
import { VueQueryPlugin, type VueQueryPluginOptions } from "vue-query";

import "bootstrap";
import "bootstrap/dist/css/bootstrap.min.css";

import App from "./App.vue";
import router from "./router";

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(VueQueryPlugin, {
	queryClientConfig: {
		defaultOptions: {
			queries: {
				refetchOnWindowFocus: false,
				//cacheTime: 30000	// NOTE: Will set cacheTime at the global level
			},
		},
	},
} as VueQueryPluginOptions);
app.use(VueAwesomePaginate);

app.mount("#app");
