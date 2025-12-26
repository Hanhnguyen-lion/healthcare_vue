import { useAuthStore } from "./store/auth.module";

const timeoutInMS = 900000; // 15 minutes -> 15 * 60s * 1000ms
let timeoutId;

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

export default setupTimers;
