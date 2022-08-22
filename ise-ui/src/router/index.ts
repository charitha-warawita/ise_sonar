import { createRouter, createWebHistory } from 'vue-router';
import HomePage from '@/pages/HomePage.vue';
import ProjectPage from '@/pages/ProjectPage.vue';
import TargetAudiencePage from '@/pages/TargetAudiencePage.vue';

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: '/',
			name: 'home',
			component: HomePage,
		},
		{
			path: '/project/:id',
			name: 'project',
			component: ProjectPage,
		},
		{
			path: '/project/:project/target-audience/:ta',
			name: 'target-audience',
			component: TargetAudiencePage,
		},
	],
});

export default router;
