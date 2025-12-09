import moment from "moment";

export function formatDateToString(value, format) {
    return moment(value).format(format);
}

export function formatDateYYYYMMDD(value) {
    return moment(value).format("YYYY-MM-DD");
}

export function addItemToArray(itemArr, item, key){
    if (itemArr != null){
        var index = itemArr.findIndex(li => li[key] == item[key]);
        var newItems = [...itemArr];
        console.log("index: ", index);
        if (index != -1){
            newItems[index] = item;
            return newItems;
        }
        return [...itemArr, item];
    }
}

export function deleteItemToArray(itemArr, id, key){
    return itemArr.filter((item) => item[key] !== id);
}

export function pad(n) {
    return (n<10 ? '0'+n : n);
}

export function validEmail(email){
    const regex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    return regex.test(email);
}