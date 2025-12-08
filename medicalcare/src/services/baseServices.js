import axios from "axios";

export async function getItemsWithParam(apiUrl, params){
    return await axios.get(apiUrl, {params: params})
        .then((response)=>{
            return {valid: true, message: "Get Items Succcessfull", data: response.data};
    }).catch((error)=>{
        var message = "";
        if (error.response) {
            message = error.response;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('getItems finish:');
    });
}

export async function getItems(apiUrl){
    return await axios.get(apiUrl)
        .then((response)=>{
            return {valid: true, message: "Get Items Succcessfull", data: response.data};
    }).catch((error)=>{
        var message = "";
        if (error.response) {
            message = error.response.data.message;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('getItems finish:');
    });
}

export async function deleteItem(apiUrl){
    return await axios.delete(apiUrl)
        .then((response)=>{
            return {valid: true, data: response.data, message: "Delete success"};
    }).catch((error)=>{
        var message = "";
        if (error.response) {
            message = error.response.data;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('deleteItem finish:');
    });
}

export async function addItem(apiUrl, item){
    return await axios.post(apiUrl, item)
        .then((response)=>{
            return {valid: true, data:response.data, message: "Add Item success"};
    }).catch((error)=>{
        var message = "";
        if (error.response) {
            message = error.response.data.message;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('addItem finish:');
    });
}

export async function updateItem(apiUrl, item){
    return await axios.put(apiUrl, item).then((response)=>{
        return {valid: true, item: response.data, message: "Update Successfull"};
    }).catch(error =>{
        var message = "";
        if (error.response) {
            message = error.response.data.message;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('updateItem finish:');
    });
}

export async function getItemById(apiUrl){
    return await axios.get(apiUrl)
        .then((response)=>{
            return {valid: true, message: "Get Item Succcessfull", data: response.data};
    }).catch((error)=>{
        var message = "";
        if (error.response) {
            message = error.response.data.message;
        } else if (error.request) {
            message = error.request;
        } else if ( error.config) {
            message =  error.config;
        } else {
            message = error.message;
        }
        console.log('Error:', message);
        return {valid: false, message};
    }).finally(()=>{
        console.log('getItemById finish:');
    });
}
