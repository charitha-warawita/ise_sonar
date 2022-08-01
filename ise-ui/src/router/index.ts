import { createRouter, createWebHistory } from 'vue-router'
import Home from '@/pages/Home.vue'
import Project from '@/pages/Project.vue'
import ProjectService from '@/services/ProjectService'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/project/:id',
      name: 'project',
      component: Project,
    }
  ]
})

export default router
