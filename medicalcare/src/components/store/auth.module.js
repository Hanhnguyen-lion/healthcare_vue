import { enviroment } from "@/enviroments/enviroment";
import router from "@/router/router";
import { post } from "@/services/baseServices";
import { defineStore } from "pinia";

const apiUrl = `${enviroment.apiUrl}/Accounts/Authenticate`;

export const useAuthStore = defineStore("auth", 
    {
    state: () => ({
        // initialize state from local storage to enable user to stay logged in
        accountLogin: JSON.parse(sessionStorage.getItem("AccountLogin")),
        returnUrl: null
    }),
    actions:{
        async login(email, password){
            var param = {
                "email": email,
                "password": password
            };
            const accountItem = await post(apiUrl, param).then(response=>{
                if (response.valid){
                    const user = response.data.item;
                    this.accountLogin = user;
                    sessionStorage.setItem("AccountLogin", JSON.stringify(user));
                    return {valid: true, user: user};
                }
                else{
                    return response;
                }
            });
            return accountItem;
        },
        logout(){
            this.accountLogin = null,
            sessionStorage.removeItem("AccountLogin");
            router.push("/Account/Login");
        }
    }
});

