import Vue from 'vue'
import Router from 'vue-router'
import Main from '@/components/main'

Vue.use(Router)

const router = new Router({
  routes: [
    {
      path: '/',
      name: 'default',
      redirect: '/main',
    },
    {
      path: '/main',
      name: 'Main',
      component: Main
    }
  ]
})

export default router

