import { enviroment } from "@/enviroments/enviroment";
import router from "@/router/router";
import { addItem } from "@/services/baseServices";
// import { BehaviorSubject } from "rxjs";

//const accountSubject = new BehaviorSubject(null);
export default {

    // setAccount(account){
    //     accountSubject.next(account);
    // },
    // clearAccount(){
    //     accountSubject.next(null);
    // },
    // getAccountValue(){
    //     return (accountSubject.asObservable()) ? accountSubject.asObservable() : null;
    // },

    login(email, password) {
        const url = `${enviroment.apiUrl}/Accounts/Authenticate`;
        var param = {
            "email": email,
            "password": password
        };

        return addItem(url, param).then(response=>{
            if (response.valid){
                var user = response.data.item;
                sessionStorage.setItem("user", JSON.stringify(user));
                //this.setAccount(user);
                return {message: response.data.message, valid: true, user: user};
            }
            else{
                sessionStorage.removeItem("user");
                return response;
            }
        });
    },
    logout() {
        //console.log("1111");
        //this.clearAccount();
        sessionStorage.removeItem("user");
        router.push('/Account/Login');
    },
    isLoggedIn(){
        if (sessionStorage.getItem("user"))
            return true;            
        else
            return false;
    }
}