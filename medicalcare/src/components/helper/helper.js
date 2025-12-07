import { useDateFormat } from "@vueuse/core";

export function formatDateToString(value, format) {
    return useDateFormat(value, format);
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