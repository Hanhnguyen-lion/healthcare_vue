import { createApp} from 'vue';
import { createPinia} from 'pinia';
import App from './App.vue';
import "./styles.css";
import 'bootstrap/dist/css/bootstrap.min.css';
import router from './router/router';
import Vue3ConfirmDialog from 'vue3-confirm-dialog';
import 'vue3-confirm-dialog/style';
import 'bootstrap/dist/js/bootstrap.bundle.min.js';
import { useAuthStore } from './store/auth.module';

const app = createApp(App);

const timeoutInMS = 900000; // 15 minutes -> 15 * 60s * 1000ms
let timeoutId;

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

// const {idle} = useIdle(900000);

// watch(idle, (isIdle)=>{
//     if (isIdle.value){
//         const auth = useAuthStore();
//         console.log('User is idle, logging out...');
//         auth.logout();
//     }
// });
function handleInactive() {
    const auth = useAuthStore();
    console.log('User is idle, logging out...');
    auth.logout();
}

function startTimer() { 
    // setTimeout returns an ID (can be used to start or clear a timer)
    timeoutId = setTimeout(handleInactive, timeoutInMS);
}

function resetTimer() { 
    clearTimeout(timeoutId);
    startTimer();
}
 
function setupTimers () {
    document.addEventListener("keypress", resetTimer, false);
    document.addEventListener("mousemove", resetTimer, false);
    document.addEventListener("mousedown", resetTimer, false);
    document.addEventListener("touchmove", resetTimer, false);
     
    startTimer();
}

app.use(createPinia());
app.use(router);
app.use(Vue3ConfirmDialog);
app.use(setupTimers());
app.component("vue3-confirm-dialog", Vue3ConfirmDialog.default);
app.mount('#app');
