import DashboardPage from '@/pages/DashboardPage.vue';
import LoginPage from '@/pages/LoginPage.vue';
import ProjectDetailsPage from '@/pages/ProjectDetailsPage.vue';
import ProjectsPage from '@/pages/ProjectsPage.vue';
import TargetAudiencePage from '@/pages/TargetAudiencePage.vue';
import { useAuthentication } from '@/services/AuthenticationService';
import { createRouter, createWebHistory } from 'vue-router';

const router = createRouter({
	history: createWebHistory(import.meta.env.BASE_URL),
	routes: [
		{
			path: '/login',
			name: 'login',
			component: LoginPage,
		},
		{
			path: '/',
			redirect: {
				name: 'dashboard',
			},
		},
		{
			path: '/dashboard',
			name: 'dashboard',
			component: DashboardPage,
			meta: {
				protected: true,
			},
		},
		{
			path: '/projects',
			name: 'projects',
			component: ProjectsPage,
			meta: {
				protected: true,
			},
		},
		{
			path: '/project/:id',
			name: 'project',
			component: ProjectDetailsPage,
			meta: {
				protected: true,
			},
		},
		{
			path: '/project/:project/target-audience/:ta',
			name: 'target-audience',
			component: TargetAudiencePage,
			meta: {
				protected: true,
			},
		},
	],
});

router.beforeEach((to) => {
	const { isLoggedIn } = useAuthentication();

	// If user is not logged in, redirect to login page.
	if (to.name !== 'login' && !isLoggedIn.value) return { name: 'login' };

	// If attempting to reach /login or /register and already logged in, redirect to home page.
	if ((to.name === 'login' || to.name === 'register') && isLoggedIn.value) return { path: '/' };
});

export default router;
