import { createRouter, createWebHistory, type RouteRecordRaw } from 'vue-router';
import Home from './components/Home.vue';

export function routes() {
  const routes = [
    {
      path: '/',
      name: 'Home',
      component: Home,
    } satisfies RouteRecordRaw,
  ];

  return createRouter({
    history: createWebHistory(import.meta.env.BASE_URL),
    routes,
  });
}
