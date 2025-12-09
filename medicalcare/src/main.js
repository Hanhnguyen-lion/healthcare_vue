import { createApp} from 'vue';
import { createPinia } from 'pinia';
import App from './App.vue';
import "./styles.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import router from './router/router';
import Vue3ConfirmDialog from 'vue3-confirm-dialog';
import 'vue3-confirm-dialog/style';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { useAuthStore } from './store/auth.module';

const app = createApp(App);

router.beforeEach((to)=>{

    const publicPages = ['/Account/Login', '/Account/Register', '/Account/Forgotpassword'];
    const authRequired = !publicPages.includes(to.path);
    const auth = useAuthStore();

    if (authRequired && !auth.accountLogin) {
        auth.returnUrl = to.fullPath;
        return '/Account/Login';
    }
    else{
        return true;
    }
});

app.use(createPinia());
app.use(router);
app.use(Vue3ConfirmDialog);
app.component("vue3-confirm-dialog", Vue3ConfirmDialog.default);
app.mount('#app');
