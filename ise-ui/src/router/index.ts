import DashboardPage from '@/pages/DashboardPage.vue';
import ProjectDetailsPage from '@/pages/ProjectDetailsPage.vue';
import ProjectsPage from '@/pages/ProjectsPage.vue';
import TargetAudiencePage from '@/pages/TargetAudiencePage.vue';
import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: '/',
			name: 'dashboard',
			component: DashboardPage,
		},
		{
			path: '/projects',
			name: 'projects',
			component: ProjectsPage,
		},
		{
			path: '/project/:id',
			name: 'project',
			component: ProjectDetailsPage,
		},
		{
			path: '/project/:project/target-audience/:ta',
			name: 'target-audience',
			component: TargetAudiencePage,
		},
	],
});

export default router;
