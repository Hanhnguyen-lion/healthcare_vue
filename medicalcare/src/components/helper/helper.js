import { useDateFormat } from "@vueuse/core";

export function formatDateToString(value, format) {
    return useDateFormat(value, format);
}