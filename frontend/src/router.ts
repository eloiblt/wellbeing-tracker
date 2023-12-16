import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import Home from './components/Home.vue';
import Login from './components/Login.vue';
import Metrics from './components/Metrics.vue';
import Signup from './components/Signup.vue';

export function routes() {
  const routes = [
    {
      path: '/',
      name: 'Home',
      component: Home,
    } satisfies RouteRecordRaw,
    {
      path: '/login',
      name: 'Login',
      component: Login,
    } satisfies RouteRecordRaw,
    {
      path: '/signup',
      name: 'Signup',
      component: Signup,
    } satisfies RouteRecordRaw,
    {
      path: '/metrics',
      name: 'Metrics',
      component: Metrics,
    } satisfies RouteRecordRaw,
    {
      path: '/:catchAll(.*)*',
      name: 'PageNotFound',
      redirect: '/',
    } satisfies RouteRecordRaw,
  ];

  return createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
  });
}
