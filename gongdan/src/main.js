// The Vue build version to load with the `import` command
// (runtime-only or standalone) has been set in webpack.base.conf with an alias.
import Vue from 'vue'
import Vuex from 'vuex'
import store from './store'
import App from './App'
import axios from 'axios'
import ElementUI from 'element-ui'
import NProgress from 'nprogress'
import {Message} from 'element-ui'
//import VueResource from 'vue-resource'
import "babel-polyfill"
import moment from 'moment'

import './assets/css/reset.css'// 导入全局样式
import './assets/css/index.css'// 导入全局样式
import './assets/iconFont/iconfont.css'// 导入图标
import 'element-ui/lib/theme-chalk/index.css';
import router from './router'//router组件引入在css之后！！！！！！

Vue.config.productionTip = false
Vue.use(Vuex)

//Vue.use(VueResource)

Vue.use(ElementUI);
Vue.prototype.$moment = moment;
//Vue.use(Message);
Vue.prototype.$message = Message;
axios.defaults.baseURL = "http://192.168.1.195/gongdan"
//localhost:2255
//120.26.15.132:8080
//192.168.1.200
//192.168.1.92
//192.168.1.195

axios.interceptors.request.use(config => {
  NProgress.start();
  config.headers.Authorization = window.sessionStorage.getItem('token');
  return config;
  //这里拿出token，做了一次处理，然后请求服务器的时候就会使用这个处理过的请求头。
  //原本在header中是没有Authorization这个属性的，用了上面语句后，在请求头中就有了这个值。
  //有了这个值后，后台就可以取这个值进行安全验证了
})

axios.interceptors.request.use(config => {
  if(config.url.indexOf('?')>-1){
    config.url=config.url+`&n=${encodeURIComponent(Math.random())}`
  }
  else{
    config.url=config.url+`?n=${encodeURIComponent(Math.random())}`
  }
  return config;
}, error => {
  return Promise.reject(error);
});

axios.interceptors.response.use(config => {
  NProgress.done();
  return config;
})

//将axios挂载到Vue的实例上，这样Vue中的每一个组件都可以通过this直接访问到$http从而来发起ajax请求
Vue.prototype.$http = axios;
/* eslint-disable no-new */

Vue.prototype.$store = store
/* eslint-disable no-new */
new Vue({
  el: '#app',
  router,
  store,
  components: { App },
  template: '<App/>'
})
