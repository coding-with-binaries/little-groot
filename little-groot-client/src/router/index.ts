import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Home from '../views/Home.vue';
import GrootLogin from '../components/groot-login/GrootLogin.vue';
import GrootRegister from '../components/groot-register/GrootRegister.vue';

Vue.use(VueRouter);

const routes: Array<RouteConfig> = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/login',
    name: 'Login',
    component: GrootLogin
  },
  {
    path: '/register',
    name: 'Register',
    component: GrootRegister
  }
];

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
});

export default router;
