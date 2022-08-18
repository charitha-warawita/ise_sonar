import { createApp } from 'vue';
import { createPinia } from 'pinia';

import App from './App.vue';
import router from './router';

import PrimeVue from 'primevue/config';
import ToastService from 'primevue/toastservice';

import './assets/main.css';

import 'primevue/resources/themes/saga-blue/theme.css'; //theme - this is a default
import 'primevue/resources/primevue.min.css'; //core css
import 'primeicons/primeicons.css'; //icons
import 'primeflex/primeflex.css';

const app = createApp(App);

app.use(createPinia());
app.use(router);
app.use(PrimeVue);
app.use(ToastService);

app.mount('#app');
