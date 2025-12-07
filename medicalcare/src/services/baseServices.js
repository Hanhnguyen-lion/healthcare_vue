import axios from "axios";

export async function getItems(apiUrl){

    const response = await axios.get(apiUrl)

    return response.data;
}

export async function deleteItem(apiUrl, id){
    try{
        var url = `${apiUrl}/Delete/${id}`;
        await axios.delete(url);
        return true;
    }
    catch(error){
        console.log("deleteItem error:", error);
        return false;
    }
}

export async function addItem(apiUrl, item){
    try{
        var url = `${apiUrl}/Add`;
        return await axios.post(url, item);
    }
    catch(error){
        console.log("addItem error:", error);
        return null;
    }
}

export async function updateItem(apiUrl, item, id){
    try{
        var url = `${apiUrl}/Edit/${id}`;
        return await axios.put(url, item);
    }
    catch(error){
        console.log("updateItem error:", error);
        return null;
    }
}

export async function getItemById(apiUrl, id){
    try{
        var url = `${apiUrl}/${id}`;
        const item =  await axios.get(url);
        return item.data;
    }
    catch(error){
        console.log("getItemById error:", error);
        return null;
    }
}
