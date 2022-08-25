import CreateView from "@/views/CreateView.vue";
import HomeView from "@/views/HomeView.vue";
import ProjectView from "@/views/ProjectView.vue";

import { createRouter, createWebHistory } from "vue-router";

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: "/",
			name: "home",
			component: HomeView,
		},
		{
			path: "/create",
			name: "createProject",
			component: CreateView,
		},
		{
			path: "/project/:id",
			name: "project",
			component: ProjectView,
		},
		{
			path: "/about",
			name: "about",
			// route level code-splitting
			// this generates a separate chunk (About.[hash].js) for this route
			// which is lazy-loaded when the route is visited.
			component: () => import("@/views/AboutView.vue"),
		},
		{
			path: "/confirm",
			name: "confirm",
			// route level code-splitting
			// this generates a separate chunk (About.[hash].js) for this route
			// which is lazy-loaded when the route is visited.
			component: () => import("@/views/ConfirmView.vue"),
		},
	],
});

export default router;
