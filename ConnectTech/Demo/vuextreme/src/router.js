import Vue from 'vue'
import Router from 'vue-router'
import Home from './views/Home.vue'
import Trip from './views/Trip.vue';
import Region from './views/Region.vue';
Vue.use(Router)

export default new Router({
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home
    },
    {
      path: '/region',
      name: 'region',
      component: Region,
      props: true 
    },
    {
      path: '/trip',
      name: 'trip',
      component: Trip
    }
  ]
})
