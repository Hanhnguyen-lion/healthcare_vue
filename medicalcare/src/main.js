import { createApp } from 'vue'
import App from './App.vue'
import "./styles.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import router from './router/router';
import Vue3ConfirmDialog from 'vue3-confirm-dialog';
import 'vue3-confirm-dialog/style';

const app = createApp(App);
app.use(router);
app.use(Vue3ConfirmDialog);
app.component("vue3-confirm-dialog", Vue3ConfirmDialog.default);
app.mount('#app');
