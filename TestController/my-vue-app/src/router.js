import { createRouter, createWebHistory } from 'vue-router';

const routes = [
    { path: '/game', component: Game }
  ];

  const router = createRouter({
    history: createWebHistory(),
    routes
  });

  export default router;