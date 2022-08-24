import { createPinia } from 'pinia';
import { createApp } from 'vue';

import App from './App.vue';
import router from './router';

import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';
import Tooltip from 'primevue/tooltip';

import './assets/main.css';

import 'primeflex/primeflex.css';
import 'primeicons/primeicons.css'; //icons
import 'primevue/resources/primevue.min.css'; //core css
import 'primevue/resources/themes/saga-blue/theme.css'; //theme - this is a default

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(PrimeVue);
app.use(ToastService);

app.directive('tooltip', Tooltip);

app.mount('#app');
