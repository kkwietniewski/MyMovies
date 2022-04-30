import Vue from "vue";
import App from "@/App.vue";
import router from "@/router";
import store from "@/store";
import vuetify from "@/plugins/vuetify";
import axios from "axios";
import ApiService from "@/services/apiServices";
import VueCompositionApi from "@vue/composition-api";
import Vuelidate from "vuelidate";

Vue.config.productionTip = false;
ApiService.init(store.state.serverUrl, `${store.state.serverUrl}/api`);
Vue.prototype.$http = axios;

Vue.use(VueCompositionApi);
Vue.use(Vuelidate);

new Vue({
  router,
  store,
  vuetify,
  render: (h) => h(App),
}).$mount("#app");
