import { createRouter, createWebHistory } from 'vue-router'
import HomeView from '@/views/HomeView.vue'
import CreateView from '@/views/CreateView.vue'
import LoginView from '@/views/LoginView.vue'
import auth from '@/services/auth'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'home',
      component: HomeView,
      meta: { requiresAuthentication: true }
    },
    {
      path: '/create',
      name: 'createProject',
      component: CreateView,
      meta: { requiresAuthentication: true }
    },
    {
      path: '/about',
      name: 'about',
      // route level code-splitting
      // this generates a separate chunk (About.[hash].js) for this route
      // which is lazy-loaded when the route is visited.
      component: () => import('@/views/AboutView.vue'),
      meta: { requiresAuthentication: true }
    },
    {
      path: '/confirm',
      name: 'confirm',
      component: () => import('@/views/ConfirmView.vue'),
      meta: { requiresAuthentication: true }
    },
    {
      path: '/user',
      name: 'user',
      component: () => import('@/views/UserView.vue'),
      meta: { requiresAuthentication: true }
    },
    {
      path: '/login',
      name: 'login',
      component: () => import('@/views/LoginView.vue'),
      meta: { requiresAuthentication: false }
    },
  ]
})

router.beforeEach((to, from, next) => {
  if(to.matched.some(record => record.meta.requiresAuthentication)) {
    console.log("entered before route");
    // this route requires authentication, check if logged in
    var isAuth = false;
    try {
      var user = auth.user();
      if(user)
        isAuth = true;
    }
    catch(error) {
    }
    
    if(isAuth) {
      next()
      return
    }
    else {
      console.log("routing to login page");
      next('/login')
      console.log("routed to login page");
      return
    }
  }
  else {
    next()
    return
  }
});

export default router
