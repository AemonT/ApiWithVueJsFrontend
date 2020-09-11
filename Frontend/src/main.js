import Vue from 'vue';
import App from './App.vue';
import axios from 'axios';
import VueAxios from 'vue-axios';
Vue.use(VueAxios, axios);
Vue.config.productionTip = true;

new Vue({
    render: h => h(App)
}).$mount('#app');

//new Vue({
//    el: '#body',
//    data() {
//        return {
//            info : null
//        }
//    },
//    mounted() {
//        axios
//            .get('http://localhost:3816/api/vehicle')
//            .then(response => (this.info = response));
//    }
//})
