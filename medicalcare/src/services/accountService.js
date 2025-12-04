import { enviroment } from "@/enviroments/enviroment";
import axios from "axios";
export default{
    async login(email, password){
        const url = `${enviroment.apiUrl}/Accounts/Authenticate`;
        var param = {
            "email": email,
            "password": password
        };


        return await axios.post(url, param).then((data)=>{
            console.log(data);
            return data.data;
        }).catch((error)=>{
            console.log("login error: ", error)
        });
    }
}
