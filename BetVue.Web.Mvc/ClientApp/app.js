const moment = require('moment');
require('moment/locale/ru');
import Vue from 'vue';
import VueWait from 'vue-wait';
import BootstrapVue from 'bootstrap-vue';
import axios from 'axios';
import router from './router';
import store from './store';
import { sync } from 'vuex-router-sync';
import App from 'components/app-root';

Vue.prototype.$http = axios;
Vue.use(BootstrapVue);
Vue.use(VueWait);
Vue.use(require('vue-moment'), {
    moment
});

sync(store, router)

const app = new Vue({
    wait: new VueWait(),
    store,
    router,
    ...App
})

export {
    app,
    router,
    store
}
